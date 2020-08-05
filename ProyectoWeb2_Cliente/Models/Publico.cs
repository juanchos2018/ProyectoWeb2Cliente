using Firebase.Database.Query;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProyectoWeb2_Cliente.Models
{
    public class Publico
    {
        public string id_producto { get; set; }
        public string id_empresa { get; set; }
        public string nombre_producto { get; set; }
        public string precio_producto { get; set; }
        public string descripcion_producto { get; set; }
        public string Seccion { get; set; }
        public string tipo { get; set; }

        public string ruta_foto { get; set; }

        private Conexion conexion;
        private IFirebaseClient client;

        public async Task<bool> Registrar(Publico o)
        {
             var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
          
            await firebase
           .Child("Publico")
           .PostAsync(new Publico() { id_producto = o.id_producto, id_empresa = o.id_empresa, nombre_producto = o.nombre_producto,precio_producto=o.precio_producto, descripcion_producto=o.descripcion_producto,Seccion=o.Seccion,tipo=o.tipo,ruta_foto=o.ruta_foto });
            return true;
        }
        public async Task<List<Publico>> Lista_Productos()
        {
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

            return (await firebase
              .Child("Publico")
              .OnceAsync<Publico>()).Select(item => new Publico
              {
                  id_producto = item.Object.id_producto,
                  id_empresa = item.Object.id_empresa,
                  nombre_producto = item.Object.nombre_producto,
                  descripcion_producto = item.Object.descripcion_producto,
                  precio_producto = item.Object.precio_producto,
                  ruta_foto=item.Object.ruta_foto
                  
              }).ToList();
        }
        public List<Publico> listaPublicos()
        {
            List<Publico> lista = new List<Publico>();
            conexion = new Conexion();
            client = new FireSharp.FirebaseClient(conexion.conec());
            FirebaseResponse response = client.Get("Publico");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            if (data == null)
            {
                lista = null;
            }
            else
            {
                foreach (var item in data)
                {
                    lista.Add(JsonConvert.DeserializeObject<Publico>(((JProperty)item).Value.ToString()));
                }
                return lista;

            }


            return lista;
        }

    }
}