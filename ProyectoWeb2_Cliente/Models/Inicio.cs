using ProyectoWeb2_Cliente.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProyectoWeb2_Cliente.Models
{
    public class Inicio
    {

        public async Task<List<Publico>>  lista()
        {
            var query = new List<Publico>();

            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

            var toUpdatePerson = (await firebase
            .Child("Publico")
            .OnceAsync<Publico>()).Where(a => a.Object.Seccion == "Abarrotes").ToList(); // parabuscar segun el tipo


            return query;
        }
       
    }
}