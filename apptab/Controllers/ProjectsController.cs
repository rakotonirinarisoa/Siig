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
        private readonly SOFTCONNECTSIIG db;
        private readonly JsonSerializerSettings settings;

        public ProjectsController()
        {
            db = new SOFTCONNECTSIIG();
            settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public ActionResult Details(string id)
        {
            ViewData["Id"] = id;

            return View();
        }

        [Route("/projects/update")]
        [HttpPost]
        public async Task<JsonResult> Update(ProjectToUpdate projectToUpdate)
        {
            var res = await db.SI_PROJETS.FirstOrDefaultAsync(project => project.ID == projectToUpdate.Id);

            if (res != null)
            {
                res.PROJET = projectToUpdate.Title;

                await db.SaveChangesAsync();

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Modification avec succès." }, settings));
            }

            return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Projet non trouvé!" }, settings));
        }

        [Route("/projects/delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(ProjectToDelete projectToDelete)
        {
            var exist = db.SI_USERS.FirstOrDefault(a => a.LOGIN == projectToDelete.Login && a.PWD == projectToDelete.Password);

            var res = await db.SI_PROJETS.FirstOrDefaultAsync(project => project.ID == projectToDelete.Id);

            if (res != null)
            {
                res.DeletionDate = DateTime.Now;

                await db.SaveChangesAsync();

                return Json(JsonConvert.SerializeObject(new { type = "success", msg = "Suppression avec succès." }, settings));
            }

            return Json(JsonConvert.SerializeObject(new { type = "error", msg = "Projet non trouvé!" }, settings));
        }
    }
}
