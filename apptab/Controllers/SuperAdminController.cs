using apptab.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime;
using System.Web;
using System.Web.Mvc;

namespace apptab.Controllers
{
    public class SuperAdminController : Controller
    {
        private readonly SOFTCONNECTSIIG db = new SOFTCONNECTSIIG();

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        //PROJETS LISTE//
        public ActionResult ProjetList()
        {
            ViewBag.Controller = "Liste des PROJETS";
            return View();
        }

        public JsonResult FillTable(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD) != null;
            if (!exist) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                var societe = db.SI_PROJETS.Select(a => new
                {
                    PROJET = a.PROJET,
                    ID = a.ID
                }).ToList();

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = societe }, settings));
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        //PROJETS CREATE//
        public ActionResult SuperAdmin()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddSociete(SI_USERS suser, SI_PROJETS societe, SI_USERS user)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD) != null;
            if (!exist) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            var societeExist = db.SI_PROJETS.FirstOrDefault(a => a.PROJET == societe.PROJET);
            if (societeExist == null)
            {
                var newSociete = new SI_PROJETS()
                {
                    PROJET = societe.PROJET
                };
                db.SI_PROJETS.Add(newSociete);
                //var eeee = db.GetValidationErrors();
                db.SaveChanges();

                //First ADMIN//
                int IDSOC = db.SI_PROJETS.FirstOrDefault(a => a.PROJET == societe.PROJET).ID;
                var newFirstAdmin = new SI_USERS()
                {
                    LOGIN = user.LOGIN,
                    PWD = user.PWD,
                    ROLE = Role.Administrateur,// db.OPA_ROLES.Where(a => a.INTITULES == "Administrateur").FirstOrDefault().ID,
                    IDPROJET = IDSOC
                };
                db.SI_USERS.Add(newFirstAdmin);
                //var eeee = db.GetValidationErrors();
                db.SaveChanges();

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = societe }, settings));
            }
            else
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Projet déjà existant. " }, settings));
            }
        }

        //SOA LISTE//
        public ActionResult SOAList()
        {
            ViewBag.Controller = "Liste des PROJETS";
            return View();
        }

        public JsonResult FillTableSOA(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD) != null;
            if (!exist) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                var societe = db.SI_SOAS.Select(a => new
                {
                    SOA = a.SOA,
                    ID = a.ID
                }).ToList();

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = societe }, settings));
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        //SOA CREATE//
        public ActionResult SuperAdminSOA()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddSocieteSOA(SI_USERS suser, SI_SOAS societe)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD) != null;
            if (!exist) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            var societeExist = db.SI_SOAS.FirstOrDefault(a => a.SOA == societe.SOA);
            if (societeExist == null)
            {
                var newSociete = new SI_SOAS()
                {
                    SOA = societe.SOA
                };
                db.SI_SOAS.Add(newSociete);
                //var eeee = db.GetValidationErrors();
                db.SaveChanges();

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = societe }, settings));
            }
            else
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "SOA déjà existant. " }, settings));
            }
        }

        //CORRESPONDANCE PROJET - SOA LISTE//
        public ActionResult PROSOAList()
        {
            ViewBag.Controller = "Correspondance entre PROJETS - SOA";
            return View();
        }

        public JsonResult FillTablePROSOA(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD) != null;
            if (!exist) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                var societe = db.SI_PROSOA.Select(a => new
                {
                    PROJET = db.SI_PROJETS.FirstOrDefault(x => x.ID == a.IDPROJET).PROJET,
                    SOA = db.SI_SOAS.FirstOrDefault(x => x.ID == a.IDSOA).SOA,
                    ID = a.ID
                }).ToList();

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = societe }, settings));
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        //CORRESPONDANCE PROJET - SOA CREATE//
        public ActionResult SuperAdminPROSOA()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddSocietePROSOA(SI_USERS suser, PROSOA societe)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD) != null;
            if (!exist) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            var Projet = db.SI_PROJETS.FirstOrDefault(a => a.ID == societe.PROJET).ID;
            var Soa = db.SI_SOAS.FirstOrDefault(a => a.ID == societe.SOA).ID;

            var societeExist = db.SI_PROSOA.FirstOrDefault(a => a.IDPROJET == Projet/* || a.IDSOA == Soa*/);

            if (societeExist == null)
            {
                var newSociete = new SI_PROSOA()
                {
                    IDPROJET = Projet,
                    IDSOA = Soa
                };
                db.SI_PROSOA.Add(newSociete);
                //var eeee = db.GetValidationErrors();
                db.SaveChanges();

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = societe }, settings));
            }
            else
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Correspondance déjà existante. " }, settings));
            }
        }

        //GET ALL PROJET//
        [HttpPost]
        public ActionResult GetAllPROJET(SI_USERS suser,string IDPROSOA)
        {
            if (IDPROSOA != null)
            {
				int? PROSOAID = int.Parse(IDPROSOA);
				var idPro = db.SI_PROSOA.Where(a => a.ID == PROSOAID).Select(a => a.IDPROJET).FirstOrDefault();

				var FProfet = db.SI_PROJETS.Where(a => a.ID != idPro).Select(a => new
				{
					PROJET = a.PROJET,
					ID = a.ID
				}).ToList();
                var FprojetFirst = db.SI_PROJETS.Where(a=>a.ID == idPro).Select(a => new
				{
					PROJET = a.PROJET,
					ID = a.ID
				}).ToList();
				return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = FprojetFirst,datas = FProfet }, settings));
			}
            else {
				var user = db.SI_PROJETS.Select(a => new
				{
					PROJET = a.PROJET,
					ID = a.ID
				}).ToList();

				return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = user }, settings));
			}
            
        }

        //GET ALL SOA//
        [HttpPost]
        public ActionResult GetAllSOA(SI_USERS suser , string IDPROSOA)
        {
			if (IDPROSOA != null)
            {
				int? PROSOAID = int.Parse(IDPROSOA);
				var idsoa = db.SI_PROSOA.Where(a => a.ID == PROSOAID).Select(a => a.IDSOA).FirstOrDefault();
				var SOA = db.SI_SOAS.Where(x => x.ID != idsoa).Select(a => new
				{
					SOA = a.SOA,
					ID = a.ID
				}).ToList();
				
                var soa1 = db.SI_SOAS.Where(x => x.ID == idsoa).Select(x => new
                {
                    SOA = x.SOA,
                    ID =x.ID
                }).ToList();

                List<SI_SOAS>SOAf = new List<SI_SOAS>();
				return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = soa1, datas = SOA }, settings));
			}
            else
            {
				var SOA = db.SI_SOAS.Select(a => new
				{
					SOA = a.SOA,
					ID = a.ID
				}).ToList();
				return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = SOA }, settings));
			}
           
        }

        //MAPPAGE LISTE//
        public ActionResult SuperAdminMaPList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult FillTableMAPP(SI_USERS suser)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD) != null;
            if (!exist) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                var mapp = db.SI_MAPPAGES.Select(a => new
                {
                    PROJET = db.SI_PROJETS.FirstOrDefault(x => x.ID == a.IDPROJET).PROJET,
                    INSTANCE = a.INSTANCE,
                    DBASE = a.DBASE,
                    ID = a.ID
                }).ToList();

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Connexion avec succès. ", data = mapp }, settings));
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        //MAPPAGE DELETE//
        [HttpPost]
        public JsonResult DeleteMAPP(SI_USERS suser, string MAPPId)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD/* && a.IDPROJET == suser.IDPROJET*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int useID = int.Parse(MAPPId);
                var user = db.SI_MAPPAGES.FirstOrDefault(a => a.ID == useID);
                if (user != null)
                {
                    db.SI_MAPPAGES.Remove(user);
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

        //MAPPAGE DETAILS//
        public ActionResult DetailsMAPP(string UserId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DetailsMAPP(SI_USERS suser, string UserId)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD/* && a.IDPROJET == suser.IDPROJET*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int useID = int.Parse(UserId);
                var map = db.SI_MAPPAGES.FirstOrDefault(a => a.ID == useID);

                if (map != null)
                {
                    var mapp = new
                    {
                        soc = db.SI_PROJETS.FirstOrDefault(a => a.ID == map.IDPROJET).ID,
                        inst = map.INSTANCE,
                        auth = map.AUTH,
                        conn = map.CONNEXION,
                        mdp = map.CONNEXPWD,
                        baseD = map.DBASE,
                        id = map.ID
                    };

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = new { PROJET = mapp.soc, INSTANCE = mapp.inst, AUTH = mapp.auth, CONNEXION = mapp.conn, MDP = mapp.mdp, BASED = mapp.baseD, id = mapp.id } }, settings));
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

        //MAPPAGE CREATE//
        public ActionResult SuperAdminMaPCreate()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SuperAdminMaPCreate(SI_USERS suser, SI_MAPPAGES user)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD/* && a.IDPROJET == suser.IDPROJET*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                var userExist = db.SI_MAPPAGES.FirstOrDefault(a => a.IDPROJET == user.IDPROJET && a.INSTANCE == user.INSTANCE && a.DBASE == user.DBASE);
                if (userExist == null)
                {
                    var newUser = new SI_MAPPAGES()
                    {
                        INSTANCE = user.INSTANCE,
                        AUTH = user.AUTH,
                        CONNEXION = user.CONNEXION,
                        CONNEXPWD = user.CONNEXPWD,
                        DBASE = user.DBASE,
                        IDPROJET = user.IDPROJET
                    };
                    db.SI_MAPPAGES.Add(newUser);
                    //var eeee = db.GetValidationErrors();
                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = user }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Le mappage existe déjà. " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }

        //GET ALL INSTANCE//
        [HttpPost]
        public ActionResult GetNewInstance(SI_USERS suser, SI_MAPPAGES map)
        {
            //Get all bases with the instance
            var BaseList = new List<string>();

            try
            {
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder();
                connection.DataSource = map.INSTANCE;
                if (map.CONNEXION != null) connection.UserID = map.CONNEXION;
                if (map.CONNEXPWD != null) connection.Password = map.CONNEXPWD;

                if (map.AUTH == 1) connection.IntegratedSecurity = false;
                else connection.IntegratedSecurity = true;

                String strConn = connection.ToString();
                SqlConnection sqlConn = new SqlConnection(strConn);
                try
                {
                    sqlConn.Open();
                    DataTable tblDatabases = sqlConn.GetSchema("Databases");
                    sqlConn.Close();
                    foreach (DataRow row in tblDatabases.Rows)
                    {
                        String strDatabaseName = row["database_name"].ToString();
                        BaseList.Add(strDatabaseName);
                    }
                    BaseList.Sort();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "message", data = BaseList }, settings));
                }
                catch (Exception)
                {
                    if (map.AUTH == 1) return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Échec de l'ouverture de session de l'utilisateur 'sa'. Vérifiez vos identifiants pour la connexion à SQL Server. " }, settings));
                    else return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Une erreur liée au réseau ou spécifique à l'instance s'est produite lors de l'établissement d'une connexion à SQL Server. Le serveur est introuvable ou n'est pas accessible. Vérifiez que le nom de l'instance est correct et que SQL Server est configuré pour autoriser les connexions distantes. " }, settings));
                }
            }
            catch (Exception)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Une erreur liée au réseau ou spécifique à l'instance s'est produite lors de l'établissement d'une connexion à SQL Server. Le serveur est introuvable ou n'est pas accessible. Vérifiez que le nom de l'instance est correct et que SQL Server est configuré pour autoriser les connexions distantes. " }, settings));
            }
        }

        //MAPPAGE UPDATE//
        [HttpPost]
        public JsonResult SuperAdminMaPUpdate(SI_USERS suser, SI_MAPPAGES user, string UserId)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD/* && a.IDPROJET == suser.IDPROJET*/);
            if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

            try
            {
                int userId = int.Parse(UserId);
                var userExist = db.SI_MAPPAGES.FirstOrDefault(a => a.IDPROJET == user.IDPROJET && a.INSTANCE == user.INSTANCE && a.DBASE == user.DBASE);
                var userupdate = db.SI_MAPPAGES.FirstOrDefault(a => a.ID == userId);
                if (userExist == null)
                {
                    userupdate.INSTANCE = user.INSTANCE;
                    userupdate.AUTH = user.AUTH;
                    userupdate.CONNEXION = user.CONNEXION;
                    userupdate.CONNEXPWD = user.CONNEXPWD;
                    userupdate.DBASE = user.DBASE;
                    userupdate.IDPROJET = user.IDPROJET;

                    //var eeee = db.GetValidationErrors();
                    db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = user }, settings));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Le mappage existe déjà. " }, settings));
                }
            }
            catch (Exception e)
            {
                return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
            }
        }
        //DELETE SOA
        [HttpPost]
		public JsonResult DeleteSOA(SI_USERS suser, string SOAid)
		{
			var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD/* && a.IDPROJET == suser.IDPROJET*/);
			if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

			try
			{
				int IDSOA = int.Parse(SOAid);
				var SOA = db.SI_SOAS.FirstOrDefault(a => a.ID == IDSOA);
                var ProjSoa = db.SI_PROSOA.Where(F_ProjetSoa => F_ProjetSoa.IDSOA == IDSOA).Select(F_ProjetSoa=> F_ProjetSoa.IDSOA).ToList();
				if (SOA != null )
				{
					db.SI_SOAS.Remove(SOA);
                    if (ProjSoa != null)
                    {
                        foreach (var p in ProjSoa)
                        {
							var F_del = db.SI_PROSOA.Where(F_remSoa => F_remSoa.IDSOA == p).FirstOrDefault();
                            db.SI_PROSOA.Remove(F_del);
						}
                        
                    }
					db.SaveChanges();
					return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Suppression SOA avec succès. " }, settings));
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
        [HttpGet]
		public ActionResult SuperAdminDetailFSOA(SI_USERS suser,string SOAid)
		{
			return View();
		}
		[HttpPost]
		public ActionResult DetailsFSOA(SI_USERS suser, string SOAID)
		{
			var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD/* && a.IDPROJET == suser.IDPROJET*/);
			if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

			try
			{
				int IDSOA = int.Parse(SOAID);
				var soa = db.SI_SOAS.FirstOrDefault(a => a.ID == IDSOA);

				if (soa != null)
				{
                    var soas = new
                    {
                        soa = soa.SOA
					};

					return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Liste des SOA", data =  soas.soa }, settings));
				}
				else
				{
					return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Erreur de connexion" }, settings));
				}
			}
			catch (Exception e)
			{
				return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
			}
		}
        public ActionResult UpdatFSOA(SI_USERS suser, string SOAID,string SOAID_2)
        {
			var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD/* && a.IDPROJET == suser.IDPROJET*/);
			if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

			try
			{
				int IDSOA = int.Parse(SOAID);
				var SOAEXIST = db.SI_SOAS.Where(soaid =>soaid.ID == IDSOA).FirstOrDefault();
				var SOAupdate = db.SI_SOAS.FirstOrDefault(soaid => soaid.ID == IDSOA);
				if (SOAEXIST != null)
				{
                    SOAupdate.SOA = SOAID_2;

					//var eeee = db.GetValidationErrors();
					db.SaveChanges();

					return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement SOA avec succès. ", data = SOAID_2 }, settings));
				}
				else
				{
					return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Le mappage existe déjà. " }, settings));
				}
			}
			catch (Exception e)
			{
				return Json(JsonConvert.SerializeObject(new { type = "error", msg = e.Message }, settings));
			}
		}
        public ActionResult DeleteFPROSOA(SI_USERS suser, string PROSOAID)
        {
			var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD/* && a.IDPROJET == suser.IDPROJET*/);
			if (exist == null) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));

			try
			{
				int IDPROSOA = int.Parse(PROSOAID);
				var PROSOA = db.SI_PROSOA.Where(prosoa =>prosoa.ID == IDPROSOA).FirstOrDefault();
				//var ProjSoa = db.SI_PROSOA.Where(F_ProjetSoa => F_ProjetSoa.IDSOA == IDPROSOA).Select(F_ProjetSoa => F_ProjetSoa.IDSOA).ToList();
				if (PROSOA != null)
				{
					db.SI_PROSOA.Remove(PROSOA);
					db.SaveChanges();
					return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Suppression CORRESPONDANCES avec succès. " }, settings));
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
        public ActionResult SuperAdminDetailFPROSOA(SI_USERS suser, string PROSOAID)
        {
            return View();
        }
		public JsonResult UpdateFPROSOA(SI_USERS suser, PROSOA societe ,string idprosoaUp)
		{
			var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == suser.LOGIN && a.PWD == suser.PWD) != null;
			if (!exist) return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion. " }, settings));
            int idUp = int.Parse(idprosoaUp);
			var Projet = db.SI_PROJETS.FirstOrDefault(a => a.ID == societe.PROJET).ID;
			var Soa = db.SI_SOAS.FirstOrDefault(a => a.ID == societe.SOA).ID;

			var societeExist = db.SI_PROSOA.FirstOrDefault(a => a.IDPROJET == Projet && a.IDSOA == Soa);

			if (societeExist == null)
			{
                
				var upCorrespondance = db.SI_PROSOA.Where(x=>x.ID== idUp).FirstOrDefault();
                upCorrespondance.IDPROJET = Projet;
                upCorrespondance.IDSOA= Soa;
				//var eeee = db.GetValidationErrors();
				db.SaveChanges();

				return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès. ", data = societe }, settings));
			}
			else
			{
				return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Correspondance déjà existante. " }, settings));
			}
		}
	}
}