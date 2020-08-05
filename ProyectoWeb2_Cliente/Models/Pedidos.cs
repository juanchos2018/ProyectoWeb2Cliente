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
    public class Pedidos
    {
        public string id_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string id_empresa { get; set; }
     //   public string producto { get; set; }
        public string estado { get; set; }
        public DateTime fecha { get; set; }


        Conexion conexion;
        IFirebaseClient client;
        public async Task Save_Pedido(Pedidos e,string id_cliente)
        {
         //   conexion = new Conexion();
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
          //  var key = Firebase.Database.FirebaseKeyGenerator.Next();

             await firebase
              .Child("PedidosCliente").Child(e.id_empresa).Child(id_cliente)
              .PutAsync(new Pedidos() { id_cliente = e.id_cliente, nombre_cliente = e.nombre_cliente, latitud = e.latitud, longitud = e.longitud,estado="NoEnviado"});

           // await firebase
           //.Child("EntregaLista")
           //.PostAsync(new Envio() { key_envio = key, codigo_conductor = e.codigo_conductor, nombre_cliente = e.nombre_cliente, nombre_conductor = "Pepe", paquete = e.paquete });


        }


        public List<Pedidos> list_Get_Peidos()
        {
            List<Pedidos> lista = new List<Pedidos>();
            conexion = new Conexion();
            client = new FireSharp.FirebaseClient(conexion.conec());
            FirebaseResponse response = client.Get("PedidosCliente");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            if (data == null)
            {
                lista = null;
            }
            else
            {
                foreach (var item in data)
                {
                    lista.Add(JsonConvert.DeserializeObject<Pedidos>(((JProperty)item).Value.ToString()));
                }
                return lista;
                //  return Json(lista, JsonRequestBehavior.AllowGet);
            }


            return lista;
        }

        public async Task<List<Pedidos>> Lista_Pedidos(string id_empresa)
        {
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

            return (await firebase
              .Child("PedidosCliente").Child(id_empresa)
              .OnceAsync<Pedidos>()).Select(item => new Pedidos
              {
                  id_cliente = item.Object.id_cliente,
                  nombre_cliente = item.Object.nombre_cliente,
                  estado = item.Object.estado,
                  fecha = item.Object.fecha,
                  latitud=item.Object.latitud,
                  longitud=item.Object.longitud
              }).ToList();
        }
    }


}