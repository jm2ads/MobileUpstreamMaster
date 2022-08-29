using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Home.Models
{
    public class HomeModel
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public Color Color { get; set; }
        public ICommand Command { get; set; }
    }
}
