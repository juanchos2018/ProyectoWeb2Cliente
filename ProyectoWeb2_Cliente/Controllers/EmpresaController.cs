using LiteDB;
using ProyectoWeb2_Cliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWeb2_Cliente.Controllers
{
    public class EmpresaController : Controller
    {

        Pedidos pe = new Pedidos();
        Envios en = new Envios();
        // GET: Empresa
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pedidos()
        {
            return View();
        }

        public ActionResult Get_Pedidos()
        {
            var lista = pe.list_Get_Peidos();
            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> Save_Envio(Envios e)
        {
            string id = e.codigo_conductor;
            await en.Save_Envio(e,id);
            return Json(pe, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PedidosClientes()
        {
            return View();
        }

        public void buscar()
        {
            Console.WriteLine("Hoola we")
        }
    }
}