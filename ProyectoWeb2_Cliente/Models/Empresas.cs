using Firebase.Database.Query;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ProyectoWeb2_Cliente.Models
{
    public class Empresas
    {
        public string id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string  direccion_usuario { get; set; }
        public int telefono_usuario { get; set; }
        public string url_imagen { get; set; }
        public string correo_usuario { get; set; }
        public string clave_usuario { get; set; }
        public string tipo_usuario { get; set; }


        Conexion conexion;
        IFirebaseClient client;
        public async Task Save_Empresas(Empresas e)
        {
            //   conexion = new Conexion();
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
              var key = Firebase.Database.FirebaseKeyGenerator.Next();

            //await firebase
            //  .Child("Empresas").Child(key)
            //  .PutAsync(new Empresas() { id_empresa = e.id_empresa, nombre_empresa = e.nombre_empresa, direccion_empresa = e.direccion_empresa, telefono_empresa = e.telefono_empresa, url_imagen_empresa = "default_imagen",correo_empresa=e.correo_empresa,clave_empresa=e.clave_empresa });

            // await firebase
            //.Child("EntregaLista")
            //.PostAsync(new Envio() { key_envio = key, codigo_conductor = e.codigo_conductor, nombre_cliente = e.nombre_cliente, nombre_conductor = "Pepe", paquete = e.paquete });


        }

        public void Save_Empresa(Empresas collection, string idcurrent_user)
        {
            Conexion conexion = new Conexion();
            //CLientDataTypeModelValidatorProvider=
            client = new FireSharp.FirebaseClient(conexion.conec());
            var data = collection;
          //  PushResponse response = client.Push("Usuarios/", data);
            // data.id_usuario = response.Result.name;
            data.id_usuario = idcurrent_user;
             SetResponse setResponse =  client.Set("Usuarios_Sistema/" + data.id_usuario, data);


            //  throw new NotImplementedException();
        }

    }
}