using ProyectoWeb2_Cliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWeb2_Cliente.Controllers
{
    public class ConductoresController : Controller
    {
        // GET: Conductores

        Conductor o = new Conductor();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Get_Conductores()
        {
            var lista = o.listaConductor();
            return Json(lista, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Get_ListaConductores()
        {
            var lista = o.listaConductor();
            var hola = "ASDasd";
            return Json(lista, JsonRequestBehavior.AllowGet);

        }
    }
}