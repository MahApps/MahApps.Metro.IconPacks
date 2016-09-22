using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _appVersion;
        private string _iconPacksVersion;
        private ICommand _goToGitHubCommand;

        public MainViewModel()
        {
            AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            IconPacksVersion = Assembly.GetAssembly(typeof(PackIconMaterial)).GetName().Version.ToString();
            this.GoToGitHubCommand =
                new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = x => System.Diagnostics.Process.Start("https://github.com/MahApps/MahApps.Metro.IconPacks")
                };
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

        public string IconPacksVersion
        {
            get { return _iconPacksVersion; }
            set
            {
                if (value == _iconPacksVersion) return;
                _iconPacksVersion = value;
                OnPropertyChanged();
            }
        }

        public ICommand GoToGitHubCommand
        {
            get { return _goToGitHubCommand; }
            set
            {
                if (Equals(value, _goToGitHubCommand)) return;
                _goToGitHubCommand = value;
                OnPropertyChanged();
            }
        }
    }
}