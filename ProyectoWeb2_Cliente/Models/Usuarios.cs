using Firebase.Database.Query;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProyectoWeb2_Cliente.Models
{
    public class Usuarios
    {
        public string id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string direccion_usuario { get; set; }
        public int telefono_usuario { get; set; }
        public string url_imagen { get; set; }
        public string correo_usuario { get; set; }
        public string clave_usuario { get; set; }
        public string tipo_usuario { get; set; }


        Conexion conexion;
        IFirebaseClient client;

        public void Save_Usuario(Usuarios collection, string idcurrent_user)
        {
             conexion = new Conexion();
          
            client = new FireSharp.FirebaseClient(conexion.conec());
            var data = collection;
        
            data.id_usuario = idcurrent_user;
            SetResponse setResponse = client.Set("Usuarios_Sistema/" + data.id_usuario, data);


            //  throw new NotImplementedException();
        }

        public async Task<Usuarios>  Obtener_Usuario(string id_usuario)
        {//no estoy usandi esto we
            var usuario = new Usuarios();

            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
                  
              var usu = (await firebase
             .Child("Usuarios_Sistema")
             .OrderByKey()
             .OnceAsync<Usuarios>()).ToList().Find(x => x.Object.id_usuario == id_usuario);

            usuario = usu.Object;

            return usuario;
        }

       
        public async Task<List<Usuarios>> Lista_Usuarios()
        {
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

            return (await firebase
              .Child("Usuarios_Sistema")
              .OnceAsync<Usuarios>()).Select(item => new Usuarios
              {
                  id_usuario = item.Object.id_usuario,
                  nombre_usuario = item.Object.nombre_usuario,
                  direccion_usuario = item.Object.direccion_usuario,
                  tipo_usuario=item.Object.tipo_usuario
              }).ToList();
        }

      
    }
}