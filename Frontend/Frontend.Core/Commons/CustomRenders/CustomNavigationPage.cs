using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Frontend.Core.Commons.CustomRenders
{
    public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage() : base()
        {
        }

        public CustomNavigationPage(Page root) : base(root)
        {
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}