using System;

namespace tvd_driver.Views
{

    public class ProfileMainPageMenuItem
    {
        public ProfileMainPageMenuItem()
        {
            TargetType = typeof(ProfileMainPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public Type TargetType { get; set; }
    }
}