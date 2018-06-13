using FirstAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstAppMVC.Controllers
{
    public class TaskController : Controller
    {
        static string connString = "Data Source=(LocalDB)\\v12.0; " +
            "Integrated Security=SSPI;" +
            "AttachDbFileName=|DataDirectory|\\TaskDB.MDF;";
        private TaskDBContext Tdb = new TaskDBContext(connString);
        // GET: Task
        [ActionName("Index")]
        public ActionResult Index()
        {
            var tasksQ = from e in Tdb.Tasks where e.IsCompleted == false select e;
            return View(tasksQ);
        }

        [ActionName("New")]
        public ActionResult NewTaskView()
        {
            return PartialView("_NewTask");
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Task task = new Task
                {
                    Name = collection["Name"],
                    IsCompleted = false,
                    Created = DateTime.Now
                };

                Tdb.Tasks.Add(task);
                Tdb.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return PartialView("_NewTask");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var task = Tdb.Tasks.Single(m => m.ID == id);
            return PartialView("_EditTask", task);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                var tID = int.Parse(collection["ID"]);
                var task = Tdb.Tasks.Single(m => m.ID == tID);
                task.Name = collection["Name"];
                Tdb.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CompleteTask(FormCollection collection)
        {
            var tID = int.Parse(collection["task.ID"]);
            Task task = Tdb.Tasks.Find(tID);
            task.IsCompleted = true;
            Tdb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}