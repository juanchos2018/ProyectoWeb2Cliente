using Firebase.Auth;
using Firebase.Database.Query;
using Firebase.Storage;
using FireSharp.Interfaces;
using ProyectoWeb2_Cliente.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ProyectoWeb2_Cliente.Controllers
{
    public class Productos
    {
        public string id_producto { get; set; }
        public string nombre_producto { get; set; }
        public string descripcion_producto { get; set; }
        public double precio_producto { get; set; }
        public int stock_producto { get; set; }
        public string ruta_foto_producto { get; set; }

        public string estado_producto { get; set; }

        private Conexion conexion;
        private string Bucket = "fir-app-cf755.appspot.com";
        private IFirebaseClient client;

        public async Task<bool> Upload(FileStream stream, Productos obj, string filenanme,string id_empresa)
        {

            conexion = new Conexion();
            var auth = new FirebaseAuthProvider(new FirebaseConfig(conexion.Firekey()));
            var a = await auth.SignInWithEmailAndPasswordAsync(conexion.AthEmail(), conexion.AthPassword());

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                })
                .Child("FotosProductos")
                .Child(filenanme)
                .PutAsync(stream, cancellation.Token);
            try
            {
                string link = await task; // wwwfireabsdfksndfimg .com
                obj.ruta_foto_producto = link;
                Task tarea2 = Task.Run(() => Create_Producto(obj, id_empresa));
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
            }
            return true;
        }



        public async Task<string> Upload2(FileStream stream, Productos obj, string filenanme, string id_empresa)
        {
            string link="";

            conexion = new Conexion();
            var auth = new FirebaseAuthProvider(new FirebaseConfig(conexion.Firekey()));
            var a = await auth.SignInWithEmailAndPasswordAsync(conexion.AthEmail(), conexion.AthPassword());

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                })
                .Child("FotosProductos")
                .Child(filenanme)
                .PutAsync(stream, cancellation.Token);
            try
            {
               link = await task; // wwwfireabsdfksndfimg .com
                obj.ruta_foto_producto = link;
                Task tarea2 = Task.Run(() => Create_Producto(obj, id_empresa));
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
            }
            return link;
        }

        public async Task<bool> Create_Producto(Productos o,string id_empresa)
        {
            try
            {

                var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
                
                var key_producto = Firebase.Database.FirebaseKeyGenerator.Next();
                //o.estado_producto = "NoPublico";
                await firebase
                  .Child("Productos").Child(id_empresa).Child(key_producto)
                  .PutAsync(new Productos() { id_producto = key_producto, nombre_producto = o.nombre_producto, descripcion_producto = o.descripcion_producto, precio_producto = o.precio_producto, stock_producto = o.stock_producto, ruta_foto_producto =o.ruta_foto_producto,estado_producto="NoPublicado" });


            }
            catch (Exception ex)
            {
                 // ModelState.AddModelError(string.Empty, ex.Message);
            }
            return true;
        }

        public async Task<List<Productos>> GetAllProductos(string id_empresa)
        {

            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

            return (await firebase
              .Child("Productos").Child(id_empresa)
              .OnceAsync<Productos>()).Select(item => new Productos
              {
                  id_producto=item.Object.id_producto,
                  nombre_producto = item.Object.nombre_producto,
                  descripcion_producto = item.Object.descripcion_producto,
                  ruta_foto_producto=item.Object.ruta_foto_producto,
                  precio_producto=item.Object.precio_producto,
                  stock_producto=item.Object.stock_producto,
                  estado_producto = item.Object.estado_producto

              }).ToList();
        }
        public async Task UpdatePerson(string personId, string id_empresa)
        {
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

            var toUpdatePerson = (await firebase
              .Child("Productos").Child(id_empresa)
              .OnceAsync<Productos>()).Where(a => a.Object.id_producto == personId).FirstOrDefault();

            string descripcion = toUpdatePerson.Object.descripcion_producto;
            string rutafoto = toUpdatePerson.Object.ruta_foto_producto;
            double precio = toUpdatePerson.Object.precio_producto;
            int stock = toUpdatePerson.Object.stock_producto;
            string nombre = toUpdatePerson.Object.nombre_producto;

            await firebase
              .Child("Productos").Child(id_empresa)
              .Child(toUpdatePerson.Key)
              .PutAsync(new Productos() { id_producto = personId,nombre_producto=nombre, descripcion_producto=descripcion,ruta_foto_producto= rutafoto,precio_producto=precio,stock_producto=stock, estado_producto = "Publicado" });
        }


    }
}