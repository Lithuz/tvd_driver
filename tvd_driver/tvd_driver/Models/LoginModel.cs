using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace tvd_driver.Models
{
    public class LoginModel
    {
        public int idEnfermero { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Pass { get; set; }
        public string TallaFilipina { get; set; }
        public string TallaPantalon { get; set; }
        public string FechaAlta { get; set; }
        public bool Activo { get; set; }
        public int Estatus { get; set; }

        private static LoginModel instance;

        public static LoginModel GetInstance()
        {
            if (instance == null)
            {
                instance = new LoginModel();
            }
            return instance;
        }
    }
}
