using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public class IconPackViewModel : ViewModelBase
    {
        private IEnumerable<IIconViewModel> _icons;
        private ICollectionView _iconsCollectionView;
        private string _filterText;
        private IIconViewModel _selectedIcon;

        public IconPackViewModel(MainViewModel mainViewModel, string caption, Type enumType, Type packType)
        {
            this.MainViewModel = mainViewModel;
            this.Caption = caption;
            this.Icons = GetIcons(enumType, packType);
            this.PrepareFiltering();
            this.SelectedIcon = this.Icons.First();
        }

        public IconPackViewModel(MainViewModel mainViewModel, string caption, Type[] enumTypes, Type[] packTypes)
        {
            IEnumerable<IIconViewModel> Icons = new List<IIconViewModel>();

            for (int counter = 0; counter < enumTypes.Length; counter++)
            {
                Icons = Icons.Concat(GetIcons(enumTypes[counter], packTypes[counter]));
            }

            this.MainViewModel = mainViewModel;
            this.Caption = caption;
            this.Icons = Icons.OrderBy((x) => { return x.Name; });
            this.PrepareFiltering();
            this.SelectedIcon = this.Icons.First();
        }

        public MainViewModel MainViewModel { get; private set; }

        private void PrepareFiltering()
        {
            this._iconsCollectionView = CollectionViewSource.GetDefaultView(this.Icons);
            this._iconsCollectionView.Filter = o => this.FilterIconsPredicate(this.FilterText, (IIconViewModel) o);
        }

        private bool FilterIconsPredicate(string filterText, IIconViewModel iconViewModel)
        {
            return string.IsNullOrWhiteSpace(filterText)
                   || iconViewModel.Name.IndexOf(filterText, StringComparison.CurrentCultureIgnoreCase) >= 0
                   || (!string.IsNullOrWhiteSpace(iconViewModel.Description) && iconViewModel.Description.IndexOf(filterText, StringComparison.CurrentCultureIgnoreCase) >= 0);
        }

        private static string GetDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attribute != null ? attribute.Description : value.ToString();
        }

        private static IEnumerable<IIconViewModel> GetIcons(Type enumType, Type packType)
        {
            return new ObservableCollection<IIconViewModel>(
                Enum.GetValues(enumType)
                    .OfType<Enum>()
                    .Select(k => GetIconViewModel(enumType, packType, k))
                    .OrderBy(m => m.Name, StringComparer.InvariantCultureIgnoreCase));
        }

        private static IIconViewModel GetIconViewModel(Type enumType, Type packType, Enum k)
        {
            var description = GetDescription(k);
            return new IconViewModel()
            {
                Name = k.ToString(),
                Description = description,
                IconPackType = packType,
                IconType = enumType,
                Value = k
            };
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
        Type IconPackType { get; set; }
        Type IconType { get; set; }
        object Value { get; set; }
    }

    public class IconViewModel : ViewModelBase, IIconViewModel
    {
        public IconViewModel()
        {
            this.CopyToClipboard =
                new SimpleCommand
                {
                    CanExecuteDelegate = x => (x != null),
                    ExecuteDelegate = x => Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        var icon = (IIconViewModel) x;
                        var text = $"<iconPacks:{icon.IconPackType.Name} Kind=\"{icon.Name}\" />";
                        Clipboard.SetDataObject(text);
                    }))
                };
        }

        private ICommand _copyToClipboard;

        public ICommand CopyToClipboard
        {
            get { return _copyToClipboard; }
            set
            {
                if (Equals(value, _copyToClipboard)) return;
                _copyToClipboard = value;
                OnPropertyChanged();
            }
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public Type IconPackType { get; set; }

        public Type IconType { get; set; }

        public object Value { get; set; }
    }
}