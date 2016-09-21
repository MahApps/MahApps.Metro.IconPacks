using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public class IconPackViewModel : ViewModelBase
    {
        private IEnumerable<IIconViewModel> _icons;
        private ICollectionView _iconsCollectionView;
        private string _filterText;
        private IIconViewModel _selectedIcon;

        public IconPackViewModel(string caption, Type iconPackType)
        {
            this.Caption = caption;
            this.Icons = GetIcons(iconPackType);
            this.PrepareFiltering();
            this.SelectedIcon = this.Icons.First();
        }

        private void PrepareFiltering()
        {
            this._iconsCollectionView = CollectionViewSource.GetDefaultView(this.Icons);
            this._iconsCollectionView.Filter = o => this.FilterIconsPredicate(this.FilterText, (IIconViewModel) o);
        }

        private bool FilterIconsPredicate(string filterText, IIconViewModel iconViewModel)
        {
            return string.IsNullOrWhiteSpace(filterText)
                   || iconViewModel.Name.IndexOf(filterText, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        private static string GetDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attribute != null ? attribute.Description : value.ToString();
        }

        private static IEnumerable<IIconViewModel> GetIcons(Type enumType)
        {
            return new ObservableCollection<IIconViewModel>(
                Enum.GetValues(enumType)
                    .OfType<Enum>()
                    .Select(k => GetIconViewModel(enumType, k))
                    .OrderBy(m => m.Name, StringComparer.InvariantCultureIgnoreCase));
        }

        private static IIconViewModel GetIconViewModel(Type enumType, Enum k)
        {
            return new IconViewModel() {Name = k.ToString(), Description = GetDescription(k), Type = enumType, Value = k};
        }

        public string Caption { get; private set; }

        public IEnumerable<IIconViewModel> Icons
        {
            get { return _icons; }
            set
            {
                if (Equals(value, _icons)) return;
                _icons = value;
                OnPropertyChanged();
            }
        }

        public string FilterText

        {
            get { return _filterText; }
            set
            {
                if (value == _filterText) return;
                _filterText = value;
                OnPropertyChanged();
                this._iconsCollectionView.Refresh();
            }
        }

        public IIconViewModel SelectedIcon
        {
            get { return _selectedIcon; }
            set
            {
                if (Equals(value, _selectedIcon)) return;
                _selectedIcon = value;
                OnPropertyChanged();
            }
        }
    }

    public interface IIconViewModel
    {
        string Name { get; set; }
        string Description { get; set; }
        Type Type { get; set; }
        object Value { get; set; }
    }

    public class IconViewModel : ViewModelBase, IIconViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Type Type { get; set; }

        public object Value { get; set; }
    }
}