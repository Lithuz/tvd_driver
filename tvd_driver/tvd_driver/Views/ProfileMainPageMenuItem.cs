using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvd_driver.ViewModels;

namespace tvd_driver.Views
{

    public class ProfileMainPageMenuItem : BaseViewModel
    {




        public ProfileMainPageMenuItem()
        {
            TargetType = typeof(ProfileMainPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}