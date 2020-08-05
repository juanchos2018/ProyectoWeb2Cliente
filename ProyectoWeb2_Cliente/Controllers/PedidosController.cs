using ProyectoWeb2_Cliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWeb2_Cliente.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos

        Pedidos pe = new Pedidos();
        Detalle de = new Detalle();
        public ActionResult Index()
        {
            return View();
        }

        //Lista_Pedidos
        public async Task<ActionResult> GetPedidos()
        {   
            string id_emprea = InicioController.id_empresa;
            var query = await pe.Lista_Pedidos(id_emprea);
            
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetDetallePedidos(string id_cliente)
        {   
            string id_emprea = InicioController.id_empresa;        
            var detalle = await de.Lista_Detalle(id_cliente);
            var datos = detalle.Where(a => a.id_empresa == id_emprea).ToList();
           
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
    }
}