using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using apptab.Data;
using apptab.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace apptab.Controllers
{
    public class TraitementController : Controller
    {
        private readonly SOFTCONNECTSIIG db = new SOFTCONNECTSIIG();
        private readonly SOFTCONNECTOM tom = new SOFTCONNECTOM();

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        //Traitement mandats PROJET//
        public ActionResult TraitementPROJET()
        {
            ViewBag.Controller = "Traitement MANDATS";

            return View();
        }

        //GET ALL PROJET//
        [HttpPost]
        public ActionResult GetAllPROJET()
        {
            var user = db.SI_PROJETS.Select(a => new
            {
                PROJET = a.PROJET,
                ID = a.ID,
                DELETIONDATE = a.DELETIONDATE,
            }).Where(a => a.DELETIONDATE == null).ToList();

            return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = user }, settings));
        }

        [HttpPost]
        public ActionResult DetailsInfoPro(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = 0;
                if (suser.IDPROJET == null)
                    crpt = exist.IDPROJET.Value;
                else
                    crpt = suser.IDPROJET.Value;

                int proj = 0;
                if (db.SI_PROJETS.FirstOrDefault(a => a.ID == crpt && a.DELETIONDATE == null) != null)
                {
                    proj = db.SI_PROJETS.FirstOrDefault(a => a.ID == crpt && a.DELETIONDATE == null).ID;
                }

                if (proj != 0)
                {
                    return Json(JsonConvert.SerializeObject(new
                    {
                        type = "success",
                        msg = "message",
                        data = new
                        {
                            PROJ = proj
                        }
                    }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "message" }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public JsonResult Generation(SI_USERS suser, DateTime DateDebut, DateTime DateFin)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.IDPROJET.Value;

                SOFTCONNECTSIIG db = new SOFTCONNECTSIIG();
                SOFTCONNECTOM.connex = new Extension().GetCon(crpt);
                SOFTCONNECTOM tom = new SOFTCONNECTOM();

                List<DATATRPROJET> list = new List<DATATRPROJET>();
                // var Fliq = tom.CPTADMIN_FLIQUIDATION.Where(a=>a.DATELIQUIDATION >= DateDebut && a.DATELIQUIDATION <= DateFin).ToList();
                if (tom.CPTADMIN_FLIQUIDATION.Any(a => a.DATELIQUIDATION >= DateDebut && a.DATELIQUIDATION <= DateFin))
                {
                    foreach (var x in tom.CPTADMIN_FLIQUIDATION.Where(a => a.DATELIQUIDATION >= DateDebut && a.DATELIQUIDATION <= DateFin).ToList())
                    {
                        if (tom.CPTADMIN_FLIQUIDATION.Any(a => a.DATELIQUIDATION >= DateDebut && a.DATELIQUIDATION <= DateFin))
                        {
                            foreach (var y in tom.CPTADMIN_MLIQUIDATION.Where(a => a.IDLIQUIDATION == x.ID).ToList())
                            {
                                if (!db.SI_TRAITPROJET.Any(a => a.No == y.ID) || db.SI_TRAITPROJET.Any(a => a.No == y.ID && a.ETAT == 2))
                                {
                                    //var Coge = y.COGE;
                                    //var Auxi = y.AUXI.ToString();
                                    var titulaire = "";
                                    if (tom.RTIERS.Any(a => a.COGE == y.COGE && a.AUXI == y.AUXI))
                                    {
                                        var isCA = tom.RTIERS.FirstOrDefault(a => a.COGE == y.COGE && a.AUXI == y.AUXI);
                                        titulaire = isCA.COGEAUXI; ;
                                    }

                                    list.Add(new DATATRPROJET { No = y.ID, REF = x.NUMEROFACTURE, OBJ = x.DESCRIPTION, TITUL = titulaire, MONT = Math.Round(y.MONTANTLOCAL.Value, 2).ToString(), COMPTE = y.POSTE, DATE = x.DATELIQUIDATION.Value.Date });
                                }
                            }
                        }
                    }
                }

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Connexion avec succès. ", data = list }, settings));
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        //Traitement mandats ORDSEC//
        public ActionResult TraitementORDSEC()
        {
            ViewBag.Controller = "Traitement MANDATS";

            return View();
        }

        [HttpPost]
        public JsonResult GenerationSIIG(SI_USERS suser, DateTime DateDebut, DateTime DateFin)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.IDPROJET.Value;

                SOFTCONNECTSIIG db = new SOFTCONNECTSIIG();
                SOFTCONNECTOM.connex = new Extension().GetCon(crpt);
                SOFTCONNECTOM tom = new SOFTCONNECTOM();

                List<DATATRPROJET> list = new List<DATATRPROJET>();

                if (db.SI_TRAITPROJET.FirstOrDefault(a => a.IDPROJET == crpt && a.DATE >= DateDebut && a.DATE <= DateFin && a.ETAT == 0) != null)
                {
                    foreach (var x in db.SI_TRAITPROJET.Where(a => a.IDPROJET == crpt && a.DATE >= DateDebut && a.DATE <= DateFin && a.ETAT == 0).ToList())
                    {
                        list.Add(new DATATRPROJET { No = x.No, REF = x.REF, OBJ = x.OBJ, TITUL = x.TITUL, MONT = Math.Round(x.MONT.Value, 2).ToString(), COMPTE = x.COMPTE, DATE = x.DATE.Value.Date });
                    }
                }

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Connexion avec succès. ", data = list }, settings));
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public JsonResult GetCheckedEcritureF(SI_USERS suser, DateTime DateDebut, DateTime DateFin, string listCompte)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            var listCompteS = listCompte.Split(',');
            foreach (var SAV in listCompteS)
            {
                try
                {
                    int crpt = exist.IDPROJET.Value;

                    SOFTCONNECTSIIG db = new SOFTCONNECTSIIG();
                    SOFTCONNECTOM.connex = new Extension().GetCon(crpt);
                    SOFTCONNECTOM tom = new SOFTCONNECTOM();
                    var FSauv = new SI_TRAITPROJET();

                    List<DATATRPROJET> list = new List<DATATRPROJET>();
                    var MLiq = tom.CPTADMIN_MLIQUIDATION.Where(FLiq => FLiq.ID.ToString() == SAV).FirstOrDefault();
                    if (tom.CPTADMIN_FLIQUIDATION.Any(FLiq => FLiq.DATELIQUIDATION >= DateDebut && FLiq.DATELIQUIDATION <= DateFin))
                    {
                        foreach (var x in tom.CPTADMIN_FLIQUIDATION.Where(a => a.DATELIQUIDATION >= DateDebut && a.DATELIQUIDATION <= DateFin && a.ID == MLiq.IDLIQUIDATION).ToList())
                        {
                            if (tom.CPTADMIN_FLIQUIDATION.Any(a => a.DATELIQUIDATION >= DateDebut && a.DATELIQUIDATION <= DateFin && a.ID == MLiq.IDLIQUIDATION))
                            {
                                var SauveF = tom.CPTADMIN_MLIQUIDATION.Where(a => a.ID.ToString() == SAV.ToUpper()).FirstOrDefault();
                                try
                                {
                                    var titulaire = "";
                                    if (tom.RTIERS.Any(a => a.COGE == SauveF.COGE && a.AUXI == SauveF.AUXI))
                                    {
                                        var isCA = tom.RTIERS.FirstOrDefault(a => a.COGE == SauveF.COGE && a.AUXI == SauveF.AUXI);
                                        titulaire = isCA.COGEAUXI;
                                    }

                                    var ss = new SI_TRAITPROJET()
                                    {
                                        No = SauveF.ID,
                                        DATECRE = DateTime.Now,
                                        TITUL = titulaire,
                                        COMPTE = SauveF.POSTE,
                                        REF = x.NUMEROFACTURE,
                                        OBJ = x.DESCRIPTION,
                                        MONT = Math.Round(SauveF.MONTANTLOCAL.Value, 2),
                                        DATE = x.DATELIQUIDATION,
                                        IDPROJET = exist.IDPROJET.Value,
                                        ETAT = 0,
                                    };
                                    db.SI_TRAITPROJET.Add(ss);
                                    db.SaveChanges();
                                }
                                catch (Exception)
                                {

                                    return Json(JsonConvert.SerializeObject(new { type = "Error", msg = "Erreur de traitements. ", data = "" }, settings));
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
                }
            }

            return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Traitements avec succès. ", data = "" }, settings));
        }
    }
}
