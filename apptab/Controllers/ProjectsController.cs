using System.Web.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using apptab.Data.Entities;
using System.Data.Entity;
using System;

namespace apptab.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly SOFTCONNECTSIIG _db;
        private readonly JsonSerializerSettings _settings;

        public ProjectsController()
        {
            _db = new SOFTCONNECTSIIG();
            _settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        private async Task<ProjectDetails> GetProjectDetails(int id)
        {
            var project_ = await (from project in _db.SI_PROJETS
                                  join user in _db.SI_USERS on project.ID equals user.IDPROJET
                                  where project.ID == id && project.DELETIONDATE == null
                                  select new ProjectDetails
                                  {
                                      Id = project.ID,
                                      Title = project.PROJET,
                                      DeletionDate = project.DELETIONDATE,
                                      AdminEmail = user.LOGIN
                                  }
                           ).FirstOrDefaultAsync();

            return project_;
        }

        public async Task<ActionResult> Details(int id)
        {
            var adminProject = await GetProjectDetails(id);

            if (adminProject == null)
            {
                var project = await _db.SI_PROJETS.Where(project_ => project_.ID == id).FirstOrDefaultAsync();

                ViewData["Id"] = id;
                ViewData["Title"] = project.PROJET;
                ViewData["AdminEmail"] = "";

                return View();
            }

            ViewData["Id"] = id;
            ViewData["Title"] = adminProject.Title;
            ViewData["AdminEmail"] = adminProject.AdminEmail;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Update(ProjectToUpdate projectToUpdate)
        {
            var user = await _db.SI_USERS.FirstOrDefaultAsync(a => a.LOGIN == projectToUpdate.Login && a.PWD == projectToUpdate.Password && a.DELETIONDATE == null);

            if (user == null)
            {
                return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion!" }, _settings));
            }

            var project = await _db.SI_PROJETS.FirstOrDefaultAsync(project_ => project_.ID == projectToUpdate.Id && project_.DELETIONDATE == null);

            if (project != null)
            {
                var admin = await _db.SI_USERS.FirstOrDefaultAsync(user_ => user_.IDPROJET == projectToUpdate.Id && user_.DELETIONDATE == null);

                var now = DateTime.Now;

                if (admin != null)
                {
                    admin.DELETIONDATE = now;
                }

                project.DELETIONDATE = now;

                var newProject = new SI_PROJETS
                {
                    PROJET = projectToUpdate.Title
                };

                _db.SI_PROJETS.Add(newProject);

                _db.SI_USERS.Add(new SI_USERS
                {
                    LOGIN = projectToUpdate.Login == "" ? admin.LOGIN : projectToUpdate.Login,
                    PWD = admin == null ? "" : admin.PWD,
                    IDPROJET = newProject.ID,
                    ROLE = Role.Administrateur,
                });

                await _db.SaveChangesAsync();

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Enregistrement avec succès." }, _settings));
            }

            return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Projet non trouvé!" }, _settings));
        }

        [HttpPost]
        public async Task<JsonResult> Delete(ProjectToDelete projectToDelete)
        {
            var user = await _db.SI_USERS.FirstOrDefaultAsync(a => a.LOGIN == projectToDelete.Login && a.PWD == projectToDelete.Password && a.DELETIONDATE == null);

            if (user == null)
            {
                return Json(JsonConvert.SerializeObject(new { type = "login", msg = "Problème de connexion!" }, _settings));
            }

            var project = await _db.SI_PROJETS.FirstOrDefaultAsync(x => x.ID == projectToDelete.Id && x.DELETIONDATE == null);

            if (project != null)
            {
                var prosoa = await _db.SI_PROSOA.FirstOrDefaultAsync(x => x.IDPROJET == projectToDelete.Id && x.DELETIONDATE == null);
                var now = DateTime.Now;

                if (prosoa != null)
                {
                    prosoa.DELETIONDATE = now;
                }

                project.DELETIONDATE = now;

                await _db.SaveChangesAsync();

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Suppression avec succès." }, _settings));
            }

            return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Projet non trouvé!" }, _settings));
        }
    }
}
