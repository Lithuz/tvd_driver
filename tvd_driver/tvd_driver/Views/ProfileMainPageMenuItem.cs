using System;
using tvd_driver.ViewModels;

namespace tvd_driver.Views
{

    public class ProfileMainPageMenuItem : BaseViewModel
    {


        public ProfileMainPageMenuItem()
        {
            TargetType = typeof(ProfileMainPageDetail);
        }


        public int id;
        public string title;
        public string color;
        public Type targetType;
        public string vOptions;
        public string textAlign;


        public int Id
        {
            get { return this.id; }
            set { SetValue(ref this.id, value); }
        }
        public string Title
        {
            get { return this.title; }
            set { SetValue(ref this.title, value); }
        }
        public string Color
        {
            get { return this.color; }
            set { SetValue(ref this.color, value); }
        }
        public Type TargetType
        {
            get { return this.targetType; }
            set { SetValue(ref this.targetType, value); }
        }
        public string VOptions
        {
            get { return this.vOptions; }
            set { SetValue(ref this.vOptions, value); }
        }
        public string TextAlign
        {
            get { return this.textAlign; }
            set { SetValue(ref this.textAlign, value); }
        }
    }
}