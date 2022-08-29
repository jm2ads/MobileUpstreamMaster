using System;
using System.Collections.Generic;

namespace Frontend.Core.Models
{
    public class MenuItem
    {
        public string Icon { get; set; }

        public string Title { get; set; }

        public Type TargetType { get; set; }

        public MenuItemType Type { get; set; }

        public GroupType GroupType { get; set; }

        public IList<MenuItem> MenuItems { get; set; }

        public bool HasChildren { get { return MenuItems.Count > 0; } }

        public MenuItem()
        {
            Type = MenuItemType.MainPage;
        }
    }
}
