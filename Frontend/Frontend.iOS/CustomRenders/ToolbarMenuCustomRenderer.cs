using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Frontend.iOS.CustomRenders;
using Frontend.iOS.Sources;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(ContentPage), typeof(ToolbarMenuCustomRenderer))]
namespace Frontend.iOS.CustomRenders
{
    public class ToolbarMenuCustomRenderer : PageRenderer
    {
        List<ToolbarItem> _secondaryItems;
        //Tabla para mostrar el menu.
        UITableView table;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            //obtengo todos los items del secondary toolbar items y los elimino de la vista default.
            if (e.NewElement is ContentPage page)
            {
                _secondaryItems = page.ToolbarItems.Where(i => i.Order == ToolbarItemOrder.Secondary).ToList();
                _secondaryItems.ForEach(t => page.ToolbarItems.Remove(t));
            }
            base.OnElementChanged(e);
        }

        public override void ViewWillAppear(bool animated)
        {
            var element = (ContentPage)Element;
            //If global secondary toolbar items are not null, I created and added a primary toolbar item with image(Overflow) I         
            // want to show.
            if (_secondaryItems != null && _secondaryItems.Count > 0)
            {
                element.ToolbarItems.Add(new ToolbarItem()
                {
                    Order = ToolbarItemOrder.Primary,
                    Icon = "ic_add_white.png",
                    Priority = 1,
                    Command = new Command(() =>
                    {
                        ToolClicked();
                    })
                });
            }
            base.ViewWillAppear(animated);
        }

        //Create a table instance and added it to the view.
        private void ToolClicked()
        {
            if (table == null)
            {
                //Set the table position to right side. and set height to the content height.
                var childRect = new RectangleF((float)View.Bounds.Width - 250, 0, 250, _secondaryItems.Count() * 56);
                table = new UITableView(childRect)
                {
                    Source = new TableSource(_secondaryItems) // Created Table Source Class as Mentioned in the 
                                                              //Xamarin.iOS   Official site
                };
                Add(table);
                return;
            }
            foreach (var subview in View.Subviews)
            {
                if (subview == table)
                {
                    table.RemoveFromSuperview();
                    return;
                }
            }
            Add(table);
        }
    }
}
