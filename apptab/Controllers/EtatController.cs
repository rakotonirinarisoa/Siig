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
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.IDPROJET.Value;
                var info = new
                {
                    fina = db.SI_FINANCEMENT.FirstOrDefault(a => a.IDPROJET == crpt).FINANCEMENT,
                    convention = db.SI_CONVENTION.FirstOrDefault(a => a.IDPROJET == crpt).CONVENTION,
                    catego = db.SI_CATEGORIE.FirstOrDefault(a => a.IDPROJET == crpt).CATEGORIE,
                    enga = db.SI_ENGAGEMENT.FirstOrDefault(a => a.IDPROJET == crpt).ENGAGEMENT,
                    proc = db.SI_PROCEDURE.FirstOrDefault(a => a.IDPROJET == crpt).PROCEDURE,
                    min = db.SI_MINISTERE.FirstOrDefault(a => a.IDPROJET == crpt).MINISTERE,
                    mis = db.SI_MISSION.FirstOrDefault(a => a.IDPROJET == crpt).MISSION,
                    prog = db.SI_PROGRAMME.FirstOrDefault(a => a.IDPROJET == crpt).PROGRAMME,
                    act = db.SI_ACTIVITE.FirstOrDefault(a => a.IDPROJET == crpt).ACTIVITE,
                    proj = db.SI_PROJETS.FirstOrDefault(a => a.ID == crpt).PROJET,
                    //soa = db.SI_SOAS.Join(db.SI_PROSOA).FirstOrDefault(a => a.ID == crpt).SOAS,
                    soa = (from soa in db.SI_SOAS
                           join pro in db.SI_PROSOA on soa.ID equals pro.IDSOA
                           where pro.IDPROJET == crpt
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
    }
}