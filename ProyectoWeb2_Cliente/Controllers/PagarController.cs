    using Firebase.Database.Query;
using ProyectoWeb2_Cliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWeb2_Cliente.Controllers
{
    public class PagarController : Controller
    {
        // GET: Pagar
        Pedidos ob = new Pedidos();
        Envios es = new Envios();
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Procesar(Pedidos o )
        {
          // await   ob.Save_Pedido(o);

            return Json("dd", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ver_Rastreo()
        {
           
            return View();
        }

        public async Task<ActionResult> Get_Position2(Envios e)
        {
            List<Envios> lis = new List<Envios>();
            string code = e.codigo_conductor;
            string key = e.key_envio;
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
            var toUpdatePerson = (await firebase
              .Child("Entregas").Child(code)
              .OnceAsync<Envios>()).Where(a => a.Object.key_envio == key).ToList();

            foreach (var item in toUpdatePerson)
            {
                Envios a = new Envios();
                a.latitud_conductor = item.Object.latitud_conductor;
                a.longitud_conductor = item.Object.longitud_conductor;
                a.latitud = item.Object.latitud;
                a.longitud = item.Object.longitud;
                lis.Add(a);
            }

            int contador = lis.Count;

            return Json(lis, JsonRequestBehavior.AllowGet);
        }

    }


}