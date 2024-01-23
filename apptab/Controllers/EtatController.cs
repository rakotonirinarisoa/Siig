using Microsoft.Build.Framework.XamlTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace apptab.Controllers
{
    public class EtatController : Controller
    {
        private readonly SOFTCONNECTSIIG db = new SOFTCONNECTSIIG();

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        //Financement//
        public ActionResult InfoProCreate()
        {
            ViewBag.Controller = "Informations PROJET";

            return View();
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

                var fina = "";
                if (db.SI_FINANCEMENT.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null) != null)
                {
                    fina = db.SI_FINANCEMENT.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).FINANCEMENT;
                }
                var convention = "";
                if (db.SI_CONVENTION.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null) != null)
                {
                    convention = db.SI_CONVENTION.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).CONVENTION;
                }
                var catego = "";
                if (db.SI_CATEGORIE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null) != null)
                {
                    catego = db.SI_CATEGORIE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).CATEGORIE;
                }
                var enga = "";
                if (db.SI_ENGAGEMENT.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null) != null)
                {
                    enga = db.SI_ENGAGEMENT.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).ENGAGEMENT;
                }
                var proc = "";
                if (db.SI_PROCEDURE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null) != null)
                {
                    proc = db.SI_PROCEDURE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).PROCEDURE;
                }
                var min = "";
                if (db.SI_MINISTERE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null) != null)
                {
                    min = db.SI_MINISTERE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).MINISTERE;
                }
                var mis = "";
                if (db.SI_MISSION.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null) != null)
                {
                    mis = db.SI_MISSION.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).MISSION;
                }
                var prog = "";
                if (db.SI_PROGRAMME.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null) != null)
                {
                    prog = db.SI_PROGRAMME.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).PROGRAMME;
                }
                var act = "";
                if (db.SI_ACTIVITE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null) != null)
                {
                    act = db.SI_ACTIVITE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).ACTIVITE;
                }
                int proj = 0;
                if (db.SI_PROJETS.FirstOrDefault(a => a.ID == crpt && a.DELETIONDATE == null) != null)
                {
                    proj = db.SI_PROJETS.FirstOrDefault(a => a.ID == crpt && a.DELETIONDATE == null).ID;
                }
                var soaA = "";
                if ((from soa in db.SI_SOAS
                     join pro in db.SI_PROSOA on soa.ID equals pro.IDSOA
                     where pro.IDPROJET == crpt && pro.DELETIONDATE == null && soa.DELETIONDATE == null
                     select soa.SOA).FirstOrDefault() != null)
                {
                    soaA = (from soa in db.SI_SOAS
                            join pro in db.SI_PROSOA on soa.ID equals pro.IDSOA
                            where pro.IDPROJET == crpt && pro.DELETIONDATE == null && soa.DELETIONDATE == null
                            select soa.SOA).FirstOrDefault();
                }

                if (proj != 0)
                {
                    return Json(JsonConvert.SerializeObject(new
                    {
                        type = "success",
                        msg = "message",
                        data = new
                        {
                            FIN = fina,
                            CONV = convention,
                            CAT = catego,
                            ENG = enga,
                            PROC = proc,
                            MIN = min,
                            MIS = mis,
                            PROG = prog,
                            ACT = act,
                            PROJ = proj,
                            SOA = soaA
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

        //GET ALL PROJET//
        [HttpPost]
        public ActionResult GetAllPROJET(SI_USERS suser)
        {
            var user = db.SI_PROJETS.Select(a => new
            {
                PROJET = a.PROJET,
                ID = a.ID,
                DELETIONDATE = a.DELETIONDATE,
            }).Where(a => a.DELETIONDATE == null).ToList();

            return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = user }, settings));
        }
    }
}
