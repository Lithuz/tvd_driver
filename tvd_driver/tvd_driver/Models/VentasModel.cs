using System;
using System.Collections.Generic;
using System.Text;

namespace tvd_driver.Models
{
    public class VentasModel
    {
        public int idVenta { get; set; }
        public int NumeroOrden { get; set; }
        public float TotalVenta { get; set; }
        public string NombreCliente { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string TelefonoCLiente { get; set; }
        public string GeoLattitud { get; set; }
        public string GeoAltitud { get; set; }
        public bool Asignado { get; set; }
        public int Enfermero { get; set; }
        public string Fecha { get; set; }
        public string EstatusFinal { get; set; }
    }
}
