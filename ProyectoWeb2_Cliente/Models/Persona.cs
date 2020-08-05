using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWeb2_Cliente.Models
{
    public class Persona
    {
        public string DNI { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
    }
}