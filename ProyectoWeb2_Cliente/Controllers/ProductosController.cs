using ProyectoWeb2_Cliente.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWeb2_Cliente.Controllers
{
    public class ProductosController : Controller
    {
        // GET: Productos
        Productos objp = new Productos();
        Publico pu = new Publico();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateProducto()
        {
            Productos producto = new Productos();
            object status = "";
            if (Request.Files.Count > 0)
            {
                try
                {
                    System.IO.FileStream stream;
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];
                    string id_empresa = InicioController.id_empresa;
                    producto.nombre_producto = HttpContext.Request.Params["nombre_producto"];
                    producto.descripcion_producto = HttpContext.Request.Params["descripcion_producto"];
                    producto.precio_producto = double.Parse(HttpContext.Request.Params["precio_producto"]);
                    producto.stock_producto =  int.Parse( HttpContext.Request.Params["stock_producto"]);
                //    producto.clave_conductor = HttpContext.Request.Params["clave_conductor"];
                  //  producto.fecha_creacion = DateTime.Now.ToShortDateString();

                     
                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), file.FileName);
                    file.SaveAs(path);
                    stream = new FileStream(Path.Combine(path), FileMode.Open);
                    Directory.CreateDirectory(Server.MapPath("~/uploads/"));

                        Task task = Task.Run(() => producto.Upload(stream, producto, file.FileName, id_empresa));
                 
                    task.Wait();
                    status = task.Status;  // 5 task complete..
                    if (task.IsCompleted)
                    {
                        return Json(status, JsonRequestBehavior.AllowGet);
                    }

                }

                catch (Exception e)
                {
                    return Json("error" + e.Message);
                }
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Get_Productos()
        {
            string id_empresas = InicioController.id_empresa;
            var lista = objp.GetAllProductos(id_empresas);
            var allPersons = await objp.GetAllProductos(id_empresas);
            return Json(allPersons, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> Publicar(Publico o)
        {
            o.id_empresa = InicioController.id_empresa;
            string id_empres= InicioController.id_empresa;
            string id_pro = o.id_producto;
            await  pu.Registrar(o);
            await objp.UpdatePerson(id_pro, id_empres);
            return Json("registado", JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Modificar()
        {
            string id_empres = InicioController.id_empresa;
            string id_producto= "-MDrjBi9PvtEBrDGzvxC";
            await objp.UpdatePerson(id_producto, id_empres);
            return Json("modificado", JsonRequestBehavior.AllowGet);
        }
    }
}