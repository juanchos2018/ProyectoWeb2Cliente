using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProyectoWeb2_Cliente.Models
{
    public class Envios
    {
        public string codigo_conductor { get; set; }
        public string nombre_cliente { get; set; }
        public string nombre_conductor { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }


        public double latitud_conductor { get; set; }
        public double longitud_conductor { get; set; }
        public string key_envio { get; set; }
        public string paquete { get; set; }
        public string estado_envio { get; set; }


        public async Task Save_Envio(Envios e, string id)
        {
            //   conexion = new Conexion();
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
            var key = Firebase.Database.FirebaseKeyGenerator.Next();

            await firebase
              .Child("Entregas").Child(id).Child(key)
              .PutAsync(new Envios() { key_envio = key, nombre_cliente = e.nombre_cliente, codigo_conductor = id, latitud = e.latitud, longitud = e.longitud, paquete = e.paquete, estado_envio = "EnProceso" });

           // await firebase
           //.Child("EntregaLista")
           //.PostAsync(new Envios() { key_envio = key, codigo_conductor = e.codigo_conductor, nombre_cliente = e.nombre_cliente, nombre_conductor = "Pepe", paquete = e.paquete });


        }

        public async Task<List<Envios>> Get_Position_Conductor(string codigo_conductor, string key_envio)
        {
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

            return (await firebase
              .Child("Entregas").Child(codigo_conductor)

              .OnceAsync<Envios>()).Select(item => new Envios
              {
                  latitud_conductor = item.Object.latitud_conductor,
                  longitud_conductor = item.Object.longitud_conductor
              }).ToList();

        }

    }
}