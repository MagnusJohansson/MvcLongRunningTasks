using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCLongRunningTasks.Controllers
{
  public class HomeController : Controller
  {

    private static IDictionary<Guid, int> _tasks = new Dictionary<Guid, int>();

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Start()
    {
      var taskId = Guid.NewGuid();
      _tasks.Add(taskId, 0);

      Task.Factory.StartNew(() =>
      {
        for (var i = 0; i <= 100; i++)
        {
          _tasks[taskId] = i;
          Thread.Sleep(50);
        }
        _tasks.Remove(taskId);
      });
      return Json(taskId);
    }

    [HttpPost]
    public ActionResult Progress(Guid id)
    {
      return Json(_tasks.Keys.Contains(id) ? _tasks[id] : 100);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}