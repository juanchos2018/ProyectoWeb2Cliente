using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWeb2_Cliente.Models
{
    public class Conductor
    {
        public string id_conductor { get; set; }
        public string nombres_conductor { get; set; }
        public string apellido_conductor { get; set; }
        public string correo_conductor { get; set; }
        public string licencia_conductor { get; set; }
        public string celular_conductor { get; set; }
        public string clave_conductor { get; set; }
        public string estado_conductor { get; set; }
        public string rutafoto_conductor { get; set; }
        public double lat_conductor { get; set; }
        public double lon_conductor { get; set; }
        public string fecha_creacion { get; set; }

        private Conexion conexion;
        private IFirebaseClient client;

        public List<Conductor> listaConductor()
        {
            List<Conductor> lista = new List<Conductor>();
            conexion = new Conexion();
            client = new FireSharp.FirebaseClient(conexion.conec());
            FirebaseResponse response = client.Get("Conductores");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            if (data == null)
            {
                lista = null;
            }
            else
            {
                foreach (var item in data)
                {
                    lista.Add(JsonConvert.DeserializeObject<Conductor>(((JProperty)item).Value.ToString()));
                }
                return lista;
                //  return Json(lista, JsonRequestBehavior.AllowGet);
            }


            return lista;
        }
    }
}