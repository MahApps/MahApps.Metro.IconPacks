using System.Collections.ObjectModel;
using System.Reflection;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _appVersion;

        public MainViewModel()
        {
            AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public ObservableCollection<IconPackViewModel> IconPacks { get; set; } = new ObservableCollection<IconPackViewModel>(
            new[]
            {
                new IconPackViewModel("Material", typeof(PackIconMaterialKind)),
                new IconPackViewModel("Modern", typeof(PackIconModernKind)),
                new IconPackViewModel("FontAwesome", typeof(PackIconFontAwesomeKind)),
                new IconPackViewModel("Entypo+", typeof(PackIconEntypoKind))
            });

        public string AppVersion
        {
            get { return _appVersion; }
            set
            {
                if (value == _appVersion) return;
                _appVersion = value;
                OnPropertyChanged();
            }
        }
    }
}