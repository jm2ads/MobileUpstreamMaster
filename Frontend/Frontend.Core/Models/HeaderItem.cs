using Frontend.Core.Commons.Observables;
using System;
using System.ComponentModel;

namespace Frontend.Core.Models
{
    public class HeaderItem : ObservableRangeCollection<MenuItem>
    {
        public string Title { get; set; }

        private bool _expanded;
        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Expanded"));
                    OnPropertyChanged(new PropertyChangedEventArgs("StateIcon"));
                }
            }
        }

        public string StateIcon
        {
            get { return Expanded ? "baseline_keyboard_arrow_up_black_24" : "ic_keyboard_arrow_down_black_24dp.png"; }
        }

        public bool StateIconVisible { set; get; }
        public string HeaderIcon { set; get; }
        public Type TargetType { get; set; }
        public MenuItemType Type { get; set; }

        public HeaderItem(string title, string headerIcon, MenuItemType type, Type targetType = null, bool expanded = false, bool iconVisible = true)
        {
            Title = title;
            Expanded = expanded;
            Type = type;
            TargetType = targetType;
            StateIconVisible = iconVisible;
            HeaderIcon = headerIcon;
        }

        public HeaderItem()
        {
        }

    }
}
