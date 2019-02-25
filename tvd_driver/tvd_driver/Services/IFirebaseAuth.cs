using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace tvd_driver.Services
{
    public interface IFirebaseAuth
    {
        Task<bool> SignIn(string email, string password);
    }
}
