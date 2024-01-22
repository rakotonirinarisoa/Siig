using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace apptab.Controllers
{
    public class ParametreController : Controller
    {
        private readonly SOFTCONNECTSIIG db = new SOFTCONNECTSIIG();

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        //Financement//
        public ActionResult FinanCreate()
        {
            ViewBag.Controller = "Paramétrage Financement";

            return View();
        }

        [HttpPost]
        public ActionResult DetailsFinan(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.IDPROJET.Value;
                var crpto = db.SI_FINANCEMENT.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null);
                if (crpto != null)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = crpto }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Veuillez créer un nouveau financement. " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public JsonResult UpdateFinan(SI_USERS suser, SI_FINANCEMENT param)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int IdS = exist.IDPROJET.Value;
                var SExist = db.SI_FINANCEMENT.FirstOrDefault(a => a.IDPROJET == IdS && a.DELETIONDATE == null);

                if (SExist != null)
                {
                    if (SExist.CODE != param.CODE || SExist.FINANCEMENT != param.FINANCEMENT)
                    {
                        SExist.DELETIONDATE = DateTime.Now;

                        var newPara = new SI_FINANCEMENT()
                        {
                            FINANCEMENT = param.FINANCEMENT,
                            CODE = param.CODE,
                            IDPROJET = IdS
                        };

                        db.SI_FINANCEMENT.Add(newPara);
                        db.SaveChanges();
                    }

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
                else
                {
                    var newPara = new SI_FINANCEMENT()
                    {
                        FINANCEMENT = param.FINANCEMENT,
                        CODE = param.CODE,
                        IDPROJET = IdS
                    };

                    db.SI_FINANCEMENT.Add(newPara);
                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
            }
            catch (Exception)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Erreur d'enregistrement de l'information. " }, settings));
            }
        }

        //Convention//
        public ActionResult ConvCreate()
        {
            ViewBag.Controller = "Paramétrage Convention";

            return View();
        }

        [HttpPost]
        public ActionResult DetailsConv(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.IDPROJET.Value;
                var crpto = db.SI_CONVENTION.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null);
                if (crpto != null)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = crpto }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Veuillez créer une nouvelle convention. " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public JsonResult UpdateConv(SI_USERS suser, SI_CONVENTION param)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int IdS = exist.IDPROJET.Value;
                var SExist = db.SI_CONVENTION.FirstOrDefault(a => a.IDPROJET == IdS && a.DELETIONDATE == null);

                if (SExist != null)
                {
                    if (SExist.CODE != param.CODE || SExist.CONVENTION != param.CONVENTION)
                    {
                        SExist.DELETIONDATE = DateTime.Now;

                        var newPara = new SI_CONVENTION()
                        {
                            CONVENTION = param.CONVENTION,
                            CODE = param.CODE,
                            IDPROJET = IdS
                        };

                        db.SI_CONVENTION.Add(newPara);
                        db.SaveChanges();
                    }

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
                else
                {
                    var newPara = new SI_CONVENTION()
                    {
                        CONVENTION = param.CONVENTION,
                        CODE = param.CODE,
                        IDPROJET = IdS
                    };

                    db.SI_CONVENTION.Add(newPara);
                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
            }
            catch (Exception)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Erreur d'enregistrement de l'information. " }, settings));
            }
        }

        //Categorie//
        public ActionResult CatCreate()
        {
            ViewBag.Controller = "Paramétrage Catégorie";

            return View();
        }

        [HttpPost]
        public ActionResult DetailsCat(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.IDPROJET.Value;
                var crpto = db.SI_CATEGORIE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null);
                if (crpto != null)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = crpto }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Veuillez créer une nouvelle catégorie. " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public JsonResult UpdateCat(SI_USERS suser, SI_CATEGORIE param)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int IdS = exist.IDPROJET.Value;
                var SExist = db.SI_CATEGORIE.FirstOrDefault(a => a.IDPROJET == IdS && a.DELETIONDATE == null);

                if (SExist != null)
                {
                    if (SExist.CODE != param.CODE || SExist.CATEGORIE != param.CATEGORIE)
                    {
                        SExist.DELETIONDATE = DateTime.Now;

                        var newPara = new SI_CATEGORIE()
                        {
                            CATEGORIE = param.CATEGORIE,
                            CODE = param.CODE,
                            IDPROJET = IdS
                        };

                        db.SI_CATEGORIE.Add(newPara);
                        db.SaveChanges();
                    }

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
                else
                {
                    var newPara = new SI_CATEGORIE()
                    {
                        CATEGORIE = param.CATEGORIE,
                        CODE = param.CODE,
                        IDPROJET = IdS
                    };

                    db.SI_CATEGORIE.Add(newPara);
                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
            }
            catch (Exception)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Erreur d'enregistrement de l'information. " }, settings));
            }
        }

        //Engagement//
        public ActionResult EngaCreate()
        {
            ViewBag.Controller = "Paramétrage Engagement";

            return View();
        }

        [HttpPost]
        public ActionResult DetailsEnga(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.IDPROJET.Value;
                var crpto = db.SI_ENGAGEMENT.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null);
                if (crpto != null)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = crpto }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Veuillez créer un nouvel engagement. " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public JsonResult UpdateEnga(SI_USERS suser, SI_ENGAGEMENT param)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int IdS = exist.IDPROJET.Value;
                var SExist = db.SI_ENGAGEMENT.FirstOrDefault(a => a.IDPROJET == IdS && a.DELETIONDATE == null);

                if (SExist != null)
                {
                    if (SExist.CODE != param.CODE || SExist.ENGAGEMENT != param.ENGAGEMENT)
                    {
                        SExist.DELETIONDATE = DateTime.Now;

                        var newPara = new SI_ENGAGEMENT()
                        {
                            ENGAGEMENT = param.ENGAGEMENT,
                            CODE = param.CODE,
                            IDPROJET = IdS
                        };

                        db.SI_ENGAGEMENT.Add(newPara);
                        db.SaveChanges();
                    }

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
                else
                {
                    var newPara = new SI_ENGAGEMENT()
                    {
                        ENGAGEMENT = param.ENGAGEMENT,
                        CODE = param.CODE,
                        IDPROJET = IdS
                    };

                    db.SI_ENGAGEMENT.Add(newPara);
                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
            }
            catch (Exception)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Erreur d'enregistrement de l'information. " }, settings));
            }
        }

        //Procédure//
        public ActionResult ProcCreate()
        {
            ViewBag.Controller = "Paramétrage Procédure";

            return View();
        }

        [HttpPost]
        public ActionResult DetailsProc(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.IDPROJET.Value;
                var crpto = db.SI_PROCEDURE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null);
                if (crpto != null)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = crpto }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Veuillez créer un nouveau procédure. " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public JsonResult UpdateProc(SI_USERS suser, SI_PROCEDURE param)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int IdS = exist.IDPROJET.Value;
                var SExist = db.SI_PROCEDURE.FirstOrDefault(a => a.IDPROJET == IdS && a.DELETIONDATE == null);

                if (SExist != null)
                {
                    if (SExist.CODE != param.CODE || SExist.PROCEDURE != param.PROCEDURE)
                    {
                        SExist.DELETIONDATE = DateTime.Now;

                        var newPara = new SI_PROCEDURE()
                        {
                            PROCEDURE = param.PROCEDURE,
                            CODE = param.CODE,
                            IDPROJET = IdS
                        };

                        db.SI_PROCEDURE.Add(newPara);
                        db.SaveChanges();
                    }

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
                else
                {
                    var newPara = new SI_PROCEDURE()
                    {
                        PROCEDURE = param.PROCEDURE,
                        CODE = param.CODE,
                        IDPROJET = IdS
                    };

                    db.SI_PROCEDURE.Add(newPara);
                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
            }
            catch (Exception)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Erreur d'enregistrement de l'information. " }, settings));
            }
        }

        //Ministère//
        public ActionResult MinCreate()
        {
            ViewBag.Controller = "Paramétrage Ministère";

            return View();
        }

        [HttpPost]
        public ActionResult DetailsMin(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.IDPROJET.Value;
                var crpto = db.SI_MINISTERE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null);
                if (crpto != null)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = crpto }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Veuillez créer un nouveau ministère. " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public JsonResult UpdateMin(SI_USERS suser, SI_MINISTERE param)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int IdS = exist.IDPROJET.Value;
                var SExist = db.SI_MINISTERE.FirstOrDefault(a => a.IDPROJET == IdS && a.DELETIONDATE == null);

                if (SExist != null)
                {
                    if (SExist.CODE != param.CODE || SExist.MINISTERE != param.MINISTERE)
                    {
                        SExist.DELETIONDATE = DateTime.Now;

                        var newPara = new SI_MINISTERE()
                        {
                            MINISTERE = param.MINISTERE,
                            CODE = param.CODE,
                            IDPROJET = IdS
                        };

                        db.SI_MINISTERE.Add(newPara);
                        db.SaveChanges();
                    }

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
                else
                {
                    var newPara = new SI_MINISTERE()
                    {
                        MINISTERE = param.MINISTERE,
                        CODE = param.CODE,
                        IDPROJET = IdS
                    };

                    db.SI_MINISTERE.Add(newPara);
                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
            }
            catch (Exception)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Erreur d'enregistrement de l'information. " }, settings));
            }
        }

        //Mission//
        public ActionResult MisCreate()
        {
            ViewBag.Controller = "Paramétrage Mission";

            return View();
        }

        [HttpPost]
        public ActionResult DetailsMis(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.IDPROJET.Value;
                var crpto = db.SI_MISSION.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null);
                if (crpto != null)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = crpto }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Veuillez créer une nouvelle mission. " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public JsonResult UpdateMis(SI_USERS suser, SI_MISSION param)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int IdS = exist.IDPROJET.Value;
                var SExist = db.SI_MISSION.FirstOrDefault(a => a.IDPROJET == IdS && a.DELETIONDATE == null);

                if (SExist != null)
                {
                    if (SExist.CODE != param.CODE || SExist.MISSION != param.MISSION)
                    {
                        SExist.DELETIONDATE = DateTime.Now;

                        var newPara = new SI_MISSION()
                        {
                            MISSION = param.MISSION,
                            CODE = param.CODE,
                            IDPROJET = IdS
                        };

                        db.SI_MISSION.Add(newPara);
                        db.SaveChanges();
                    }

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
                else
                {
                    var newPara = new SI_MISSION()
                    {
                        MISSION = param.MISSION,
                        CODE = param.CODE,
                        IDPROJET = IdS
                    };

                    db.SI_MISSION.Add(newPara);
                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
            }
            catch (Exception)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Erreur d'enregistrement de l'information. " }, settings));
            }
        }

        //Programme//
        public ActionResult ProgCreate()
        {
            ViewBag.Controller = "Paramétrage Programme";

            return View();
        }

        [HttpPost]
        public ActionResult DetailsProg(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.IDPROJET.Value;
                var crpto = db.SI_PROGRAMME.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null);
                if (crpto != null)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = crpto }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Veuillez créer un nouveau programme. " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public JsonResult UpdateProg(SI_USERS suser, SI_PROGRAMME param)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int IdS = exist.IDPROJET.Value;
                var SExist = db.SI_PROGRAMME.FirstOrDefault(a => a.IDPROJET == IdS && a.DELETIONDATE == null);

                if (SExist != null)
                {
                    if (SExist.CODE != param.CODE || SExist.PROGRAMME != param.PROGRAMME)
                    {
                        SExist.DELETIONDATE = DateTime.Now;

                        var newPara = new SI_PROGRAMME()
                        {
                            PROGRAMME = param.PROGRAMME,
                            CODE = param.CODE,
                            IDPROJET = IdS
                        };

                        db.SI_PROGRAMME.Add(newPara);
                        db.SaveChanges();
                    }

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
                else
                {
                    var newPara = new SI_PROGRAMME()
                    {
                        PROGRAMME = param.PROGRAMME,
                        CODE = param.CODE,
                        IDPROJET = IdS
                    };

                    db.SI_PROGRAMME.Add(newPara);
                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
            }
            catch (Exception)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Erreur d'enregistrement de l'information. " }, settings));
            }
        }

        //Activité//
        public ActionResult ActCreate()
        {
            ViewBag.Controller = "Paramétrage Activité";

            return View();
        }

        [HttpPost]
        public ActionResult DetailsAct(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.IDPROJET.Value;
                var crpto = db.SI_ACTIVITE.FirstOrDefault(a => a.IDPROJET == crpt && a.DELETIONDATE == null);
                if (crpto != null)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = crpto }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Veuillez créer une nouvelle activité. " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public JsonResult UpdateAct(SI_USERS suser, SI_ACTIVITE param)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDSOCIETE == suser.IDSOCIETE*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int IdS = exist.IDPROJET.Value;
                var SExist = db.SI_ACTIVITE.FirstOrDefault(a => a.IDPROJET == IdS && a.DELETIONDATE == null);

                if (SExist != null)
                {
                    if (SExist.CODE != param.CODE || SExist.ACTIVITE != param.ACTIVITE)
                    {
                        SExist.DELETIONDATE = DateTime.Now;

                        var newPara = new SI_ACTIVITE()
                        {
                            ACTIVITE = param.ACTIVITE,
                            CODE = param.CODE,
                            IDPROJET = IdS
                        };

                        db.SI_ACTIVITE.Add(newPara);
                        db.SaveChanges();
                    }

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
                else
                {
                    var newPara = new SI_ACTIVITE()
                    {
                        ACTIVITE = param.ACTIVITE,
                        CODE = param.CODE,
                        IDPROJET = IdS
                    };

                    db.SI_ACTIVITE.Add(newPara);
                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = param }, settings));
                }
            }
            catch (Exception)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Erreur d'enregistrement de l'information. " }, settings));
            }
        }
    }
}