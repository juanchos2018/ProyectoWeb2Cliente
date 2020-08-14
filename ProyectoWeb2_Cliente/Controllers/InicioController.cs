using Firebase.Auth;
using ProyectoWeb2_Cliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWeb2_Cliente.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        private static string key1 = "AIzaSyASBVEzU8ZqFHtMgdYW7-66ZQjrZGf-lAc";

        Empresas emp = new Empresas();
        public static string correo;
        public static string id_user;
        public static string id_empresa;
        public static string nombreusuario;
        public static string tipo_usuario;
        Usuarios usu = new Usuarios();
        Publico pu = new Publico();
        Pedidos p = new Pedidos();
        Detalle de = new Detalle();

        public ActionResult Index()
        {
            if (correo != null)
            {
              
                if (nombreusuario==null)
                {
                    ViewBag.Correo = "llego vacio" ;
                }
                else
                {
                    ViewBag.Correo = nombreusuario;
                }
            }
            //var query =  await pu.Lista_Productos();
          //  var datos = query.Where(a => a.Seccion == "Abarrotes").ToList();
            var query2 = pu.listaPublicos();
            return View(query2);
        }

        public ActionResult Registro()
        {    
            return View();
        }
        

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RegistrarEmpresa(Empresas o)
        {
            //para crear auntenticacion de correo electronico we
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(key1));

                var a = await auth.CreateUserWithEmailAndPasswordAsync(o.correo_usuario, o.clave_usuario, o.nombre_usuario, true);
                var token = a.User.FederatedId;
                var id = a.User.LocalId;  //para tener el id del usuario que esta registrado we :V
                emp.Save_Empresa(o, id);


                //  auth.CreateUserWithEmailAndPasswordAsync(o.correo_usuario, o.clave_usuario, o.nombre_usuario, true).contiwitd(task=>;
                //{
                //    if (task.IsCompleted)
                //    {
                //        var aa = task.Result.User.LocalId; // obtiene el id del usuario Registrado
                //        estado = task.Status;
                //        es = true;
                //        // aqui poner el Codigo para Registrar a SQL
                //    }
                //    if (task.IsFaulted)
                //    {
                //        estado = task.Status;
                //        es = false;
                //    }
                //    estado = task.Status;
                //});
                // ModelState.AddModelError(string.Empty, "verifica tu coreo" + " token" + token + " ids" + id);

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);

            }
            return Json("Registrado",JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returUrl)
        {
            try
            {
                if (this.Request.IsAuthenticated)
                {
                    // return this.Redi
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnurl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(key1));
                    var ab = await auth.SignInWithEmailAndPasswordAsync(model.Email, model.Password);
                    var token = ab.FirebaseToken;
                    id_user = ab.User.LocalId;
                    correo = ab.User.Email;

                    var user = ab.User;
                    if (token != "")
                    {

                        var allPersons = await usu.Lista_Usuarios();
              
                        var datos = allPersons.Where(a => a.id_usuario == ab.User.LocalId).FirstOrDefault();
                    
                        nombreusuario = datos.nombre_usuario;
                        id_empresa = datos.id_usuario;
                        tipo_usuario = datos.tipo_usuario;
                        if (tipo_usuario.Equals("Empresa"))
                        {
                          return  this.RedirectToAction("Index","Empresa");

                        }
                        else
                        {
                            return this.RedirectToAction("Index", "Inicio");
                        }

                        
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "invalide usrname or password");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return View(model);
        }

        private ActionResult RedirecToLocal(string retunrUrl)
        {
            try
            {
                if (Url.IsLocalUrl(retunrUrl))
                {
                    return this.Redirect(retunrUrl);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return this.RedirectToAction("Index", "Inicio");
        }
      

        public async Task<ActionResult> GetUsuario(Usuarios o)
        {    // despues borrar Esto  ya  lo probe we

            var allPersons = await usu.Lista_Usuarios();
            string personId = o.id_usuario;          
            var datos =allPersons.Where(a => a.id_usuario == personId).FirstOrDefault();
            string nombre = datos.nombre_usuario;
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ListaProductos_1()
        {
            var query = await pu.Lista_Productos();
            var datos = query.Where(a => a.Seccion == "Abarrotes").ToList();

            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Pediddo_Cliente(Pedidos o)
        {
            string id_cliente = id_empresa;
            o.id_cliente= id_empresa;
            await  p.Save_Pedido(o , id_cliente);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Pediddo_Detalle(Detalle o)
        {
            //  p.Save_Pedido(o);
            string id_cliente = id_empresa;
             await  de.Save_Detalle(o, id_cliente);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}