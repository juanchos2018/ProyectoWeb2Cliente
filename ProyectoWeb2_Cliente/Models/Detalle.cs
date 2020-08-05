using Firebase.Database.Query;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProyectoWeb2_Cliente.Models
{
    public class Detalle
    {
        public string id_cliente { get; set; }
        public string id_producto { get; set; }
        public string nombre_producto { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public string id_empresa { get; set; }

        Conexion conexion;
        IFirebaseClient client;
        public async Task Save_Detalle(Detalle e, string id_cliente)
        {
            //   conexion = new Conexion();
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
            //  var key = Firebase.Database.FirebaseKeyGenerator.Next();

            await firebase
             .Child("PedidosDetalle").Child(id_cliente)
             .PostAsync(new Detalle() { id_cliente = e.id_cliente,  id_producto = e.id_producto, nombre_producto = e.nombre_producto, cantidad =e.cantidad,total=e.total,id_empresa=e.id_empresa });

            // await firebase
            //.Child("EntregaLista")
            //.PostAsync(new Envio() { key_envio = key, codigo_conductor = e.codigo_conductor, nombre_cliente = e.nombre_cliente, nombre_conductor = "Pepe", paquete = e.paquete });


        }

        public async Task<List<Detalle>> Lista_Detalle(string id_cliente)
        {
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

            return (await firebase
              .Child("PedidosDetalle").Child(id_cliente)
              .OnceAsync<Detalle>()).Select(item => new Detalle
              {
                  id_cliente = item.Object.id_cliente,
                  id_empresa = item.Object.id_empresa,
                  nombre_producto = item.Object.nombre_producto,
                  cantidad = item.Object.cantidad,
                  total=item.Object.total
              }).ToList();
        }

    }
}