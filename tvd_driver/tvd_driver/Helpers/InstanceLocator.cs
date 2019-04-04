using System;
using System.Collections.Generic;
using System.Text;

namespace tvd_driver.Helpers
{
    using ViewModels;
    class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
