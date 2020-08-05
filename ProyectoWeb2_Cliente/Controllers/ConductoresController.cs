using Newtonsoft.Json;
using ProyectoWeb2_Cliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
            ViewBag.Id_empresa= InicioController.id_empresa;
            return View();
        }


        public ActionResult Get_Conductores(string id_empresa)
        {
            var lista = o.listaConductor();
            return Json(lista, JsonRequestBehavior.AllowGet);

        }


        public async Task<ActionResult> Get_Conductores2(string id_empresa)
        {    // despues borrar Esto  ya  lo probe we

            var allPersons = await o.Lista_Conductores2();
     
            var datos = allPersons.Where(a => a.id_empresa == id_empresa).ToList();
        
            return Json(datos, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> Get_Consulta_Dni(string dni)
        {
            HttpClient client = new HttpClient();

            String URL2 = "https://quertium.com/api/v1/reniec/dni/" + dni + "?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.MTM3Mw.x-jUgUBcJukD5qZgqvBGbQVMxJFUAIDroZEm4Y9uTyg";
            HttpResponseMessage response = await client.GetAsync(URL2);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Persona>(data);
                return Json(product, JsonRequestBehavior.AllowGet);
            }

            return Json("sin Datos", JsonRequestBehavior.AllowGet);

        }


        public ActionResult Save_Conductores(Conductor o)
        {
            bool completado = false;
           
            string id_empresa = InicioController.id_empresa;
            Task tarea = Task.Run(() =>o.Create_Conductor(o, id_empresa));
                      
            if (tarea.IsCompleted)
            {
                completado = true;
            }

            return Json(completado, JsonRequestBehavior.AllowGet);

        }
    }
}