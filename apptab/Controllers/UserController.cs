using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using apptab;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using apptab.Data.Entities;
using System.Data.Entity;

namespace SOFTCONNECT.Controllers
{
    public class UserController : Controller
    {
        private readonly SOFTCONNECTSIIG db = new SOFTCONNECTSIIG();

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        //// GET: User
        //[Route("UserList")]
        //[HttpGet]
        public ActionResult List()
        {
            ViewBag.Controller = "Liste des Utilisateurs";
            return View();
        }

        [HttpPost]
        public JsonResult FillTable(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null);
            ViewBag.Role = exist.ROLE;
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                var test = db.SI_USERS.Where(x => x.ROLE == exist.ROLE && x.IDPROJET == exist.IDPROJET && x.DELETIONDATE == null).FirstOrDefault();
                //var test = db.SI_USERS.Where(x => x.ROLE == suser.ROLE && x.IDPROJET == suser.IDPROJET).FirstOrDefault();
                if (test.ROLE == Role.SAdministrateur)
                {
                    var users = db.SI_USERS.Select(a => new
                    {
                        a.LOGIN,
                        a.PWD,
                        ROLE = a.ROLE.ToString(), //db.OPA_ROLES.Where(x => x.ID == a.ROLE).FirstOrDefault().INTITULES,
                        ID = a.ID,
                        PROJET = db.SI_PROJETS.Where(z => z.ID == exist.IDPROJET && z.DELETIONDATE == null).FirstOrDefault().PROJET,
                        DELETONDATE = a.DELETIONDATE
                    }).Where(a => a.DELETONDATE == null).ToList();
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Connexion avec succès. ", data = users }, settings));
                }
                else
                {
                    var users = db.SI_USERS.Where(x => x.ROLE != Role.SAdministrateur && x.IDPROJET == exist.IDPROJET && x.DELETIONDATE == null).Select(a => new
                    {
                        a.LOGIN,
                        a.PWD,
                        ROLE = a.ROLE.ToString(), //db.OPA_ROLES.Where(x => x.ID == a.ROLE).FirstOrDefault().INTITULES,
                        ID = a.ID,
                        PROJET = db.SI_PROJETS.Where(z => z.ID == exist.IDPROJET && z.DELETIONDATE == null).FirstOrDefault().PROJET,
                        DELETONDATE = a.DELETIONDATE
                    }).Where(a => a.DELETONDATE == null).ToList();
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Connexion avec succès. ", data = users }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public async Task<JsonResult> Password(UserPassword userPassword)
        {
            var connectedUser = await db.SI_USERS.FirstOrDefaultAsync(
                a => a.LOGIN == userPassword.LoginName && a.PWD == userPassword.Password && a.DELETIONDATE == null && (a.ROLE == Role.SAdministrateur || a.ROLE == Role.Administrateur)
            );

            if (connectedUser == null)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "" }, settings));
            }

            var res = await db.SI_USERS.FirstOrDefaultAsync(user => user.ID == userPassword.UserId);

            return Json(JsonConvert.SerializeObject(new
            {
                type = "success",
                msg = "Connexion avec succès. ",
                data = new
                {
                    login = res.LOGIN,
                    password = res.PWD
                }
            }, settings));
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GetAllRole()
        {
            //var role = db.SI_ROLES.Where(a => a.INTITULES != "SAdministrateur").Select(a => new
            //{
            //    INTITULE = a.INTITULES,
            //    ID = a.ID,
            //}).ToList();
            var enumlist = Enum.GetValues(typeof(Role));

            var roles = new Dictionary<int, string>();

            foreach (var item in enumlist)
            {
                if (item.ToString() != "SAdministrateur")
                    roles.Add((int)item, Enum.GetName(typeof(Role), item));
            }
            return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = roles }, settings));
        }

        [HttpPost]
        public JsonResult AddUser(SI_USERS suser, SI_USERS user)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDPROJET == suser.IDPROJET*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                var userExist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == user.LOGIN && a.DELETIONDATE == null/* && a.IDPROJET == exist.IDPROJET*/);
                if (userExist == null)
                {
                    var newUser = new SI_USERS()
                    {
                        LOGIN = user.LOGIN,
                        PWD = user.PWD,
                        IDPROJET = exist.IDPROJET,
                        ROLE = user.ROLE,
                    };
                    db.SI_USERS.Add(newUser);

                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = user }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "L'utilisateur existe déjà (pour ce projet ou un autre). " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        [HttpPost]
        public JsonResult UpdateUser(SI_USERS suser, SI_USERS user, string oldPassword, string UserId)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDPROJET == suser.IDPROJET*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int userId = int.Parse(UserId);
                var userExist = db.SI_USERS.FirstOrDefault(a => a.ID == userId && a.DELETIONDATE == null);
                if (userExist != null)
                {
                    if (userExist.PWD != oldPassword)
                    {
                        return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Ancien mot de passe erroné!", data = user }, settings));
                    }

                    userExist.LOGIN = user.LOGIN;
                    userExist.PWD = user.PWD;
                    userExist.IDPROJET = exist.IDPROJET;
                    userExist.ROLE = user.ROLE;

                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = user }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "L'utilisateur existe déjà (pour ce projet ou un autre). " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }
        public ActionResult Param()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Param(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDPROJET == suser.IDPROJET*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.ID;
                var crpto = db.SI_USERS.FirstOrDefault(a => a.ID == crpt && a.DELETIONDATE == null);
                if (crpto != null)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = crpto }, settings));
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
        public JsonResult UpdateMDP(SI_USERS suser, SI_USERS user/*, string MDPA*/)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDPROJET == suser.IDPROJET*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int crpt = exist.ID;
                var SExist = db.SI_USERS.FirstOrDefault(a => a.ID == crpt && a.DELETIONDATE == null);
                if (SExist != null)
                {
                    if (SExist.PWD != user.LOGIN)
                        return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Veuillez vérifier votre ancien mot de passe. ", data = user }, settings));

                    SExist.PWD = user.PWD;
                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = user }, settings));
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
        public ActionResult DetailsUser(string UserId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DetailsUser(SI_USERS suser, string UserId)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDPROJET == suser.IDPROJET*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int useID = int.Parse(UserId);
                var user = db.SI_USERS.FirstOrDefault(a => a.ID == useID && a.DELETIONDATE == null);

                if (user != null)
                {
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = user }, settings));
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
        public ActionResult Login()
        {
            db.SI_USERS.Any();
            return View();
        }

        [HttpPost]
        public ActionResult Login(SI_USERS Users)
        {
            try
            {
                var test = db.SI_USERS.FirstOrDefault(x => x.LOGIN == Users.LOGIN && x.PWD == Users.PWD && x.DELETIONDATE == null);
                if (test == null) return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Vérifiez vos identifiants. " }, settings));

                if (String.IsNullOrEmpty(test.IDPROJET.ToString()) || !db.SI_PROJETS.Any(a => a.ID == test.IDPROJET && a.DELETIONDATE == null))
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Vous n'êtes pas rattaché à un projet actif. " }, settings));

                Session["userSession"] = test;

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", Data = new { test.ROLE, test.IDPROJET } }, settings));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult DeleteUser(SI_USERS suser, string UserId)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDPROJET == suser.IDPROJET*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int useID = int.Parse(UserId);
                var user = db.SI_USERS.FirstOrDefault(a => a.ID == useID && a.DELETIONDATE == null);
                if (user != null)
                {
                    user.DELETIONDATE = DateTime.Now;
                    db.SaveChanges();
                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Suppression avec succès. " }, settings));
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
        public JsonResult GetUR(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD && a.DELETIONDATE == null/* && a.IDPROJET == suser.IDPROJET*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            return Json(JsonConvert.SerializeObject(new { type = "login", msg = "", data = exist.ROLE != Role.SAdministrateur }, settings));
        }
    }
}
