using Firebase.Auth;
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
    public class Conductor
    {
        public string id_conductor { get; set; }
        public double dni_conductor { get; set; }
        public string nombres_conductor { get; set; }
        public string apellido_conductor { get; set; }
        public string correo_conductor { get; set; }
        public string celular_conductor { get; set; }
        //  public string licencia_conductor { get; set; }

        public string clave_conductor { get; set; }
        public string estado_conductor { get; set; }
        public string rutafoto_conductor { get; set; }

        public string id_empresa { get; set; }
        public string verificado { get; set; }
        //public double lat_conductor { get; set; }
        // public double lon_conductor { get; set; }
        // public string fecha_creacion { get; set; }

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
                
            }


            return lista;
        }

        public async Task<bool> Create_Conductor(Conductor o,string id_empresa)
        {
            try
            {
                conexion = new Conexion();
                var auth = new FirebaseAuthProvider(new FirebaseConfig(conexion.Firekey()));
                var a = await auth.CreateUserWithEmailAndPasswordAsync(o.correo_conductor, o.clave_conductor, o.nombres_conductor, true);

                var id = a.User.LocalId;  //para tener el id del usuario que esta registrado we :V
                client = new FireSharp.FirebaseClient(conexion.conec());
                var data = o;

                data.id_conductor = id;
                data.id_empresa = id_empresa;
                data.verificado = "false";
                SetResponse setResponse = client.Set("Conductores/" + data.id_conductor, data);

            ///    var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
               // await firebase
                 //.Child("Conductores").Child(id_empresa)
                 //.PostAsync(new Conductor() { dni_conductor = o.dni_conductor, nombres_conductor = o.nombres_conductor,apellido_conductor=o.apellido_conductor });


            }
            catch (Exception ex)
            {

                // ModelState.AddModelError(string.Empty, ex.Message);

            }
            return true;
        }

        public async Task<List<Conductor>> Lista_Conductores2()
        {
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

            return (await firebase
              .Child("Conductores")
              .OnceAsync<Conductor>()).Select(item => new Conductor
              {
                  id_conductor = item.Object.id_conductor,
                  dni_conductor = item.Object.dni_conductor,
                  nombres_conductor = item.Object.nombres_conductor,
                  apellido_conductor = item.Object.apellido_conductor,
                  celular_conductor=item.Object.celular_conductor,
                  correo_conductor=item.Object.correo_conductor

              }).ToList();
        }
    }
}