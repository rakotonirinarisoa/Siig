using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
                int crpt = exist.IDPROJET.Value;
                var info = new
                {
                    fina = db.SI_FINANCEMENT.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).FINANCEMENT,
                    convention = db.SI_CONVENTION.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).CONVENTION,
                    catego = db.SI_CATEGORIE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).CATEGORIE,
                    enga = db.SI_ENGAGEMENT.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).ENGAGEMENT,
                    proc = db.SI_PROCEDURE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).PROCEDURE,
                    min = db.SI_MINISTERE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).MINISTERE,
                    mis = db.SI_MISSION.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).MISSION,
                    prog = db.SI_PROGRAMME.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).PROGRAMME,
                    act = db.SI_ACTIVITE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null).ACTIVITE,
                    proj = db.SI_PROJETS.FirstOrDefault(a => a.ID == crpt && a.DELETIONDATE == null).PROJET,
                    //soa = db.SI_SOAS.Join(db.SI_PROSOA).FirstOrDefault(a => a.ID == crpt).SOAS,
                    soa = (from soa in db.SI_SOAS
                           join pro in db.SI_PROSOA on soa.ID equals pro.IDSOA
                           where pro.IDPROJET == crpt && pro.DELETIONDATE == null && soa.DELETIONDATE == null
                           select soa.SOA).FirstOrDefault()
                };

                if (info != null)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = new { FIN = info.fina, CONV = info.convention, CAT = info.catego, 
                    ENG = info.enga, PROC = info.proc, MIN = info.min, MIS = info.mis, PROG = info.prog, ACT = info.act, PROJ = info.proj, SOA = info.soa } }, settings));
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