using System;
using System.Windows;
using System.Windows.Input;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
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
                        var icon = (IIconViewModel)x;
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
        public MainViewModel MainViewModel { get; set; }
    }
}
