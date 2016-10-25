using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Threading;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _appVersion;
        private string _iconPacksVersion;
        private ICommand _goToGitHubCommand;
        private Dispatcher _dispatcher;
        private string _filterText;

        public MainViewModel(Dispatcher dispatcher)
        {
            this._dispatcher = dispatcher;
            this.AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.IconPacks = new ObservableCollection<IconPackViewModel>(
                new[]
                {
                    new IconPackViewModel(this, "Material", typeof(PackIconMaterialKind), typeof(PackIconMaterial)),
                    new IconPackViewModel(this, "FontAwesome", typeof(PackIconFontAwesomeKind), typeof(PackIconFontAwesome)),
                    new IconPackViewModel(this, "Octicons", typeof(PackIconOcticonsKind), typeof(PackIconOcticons)),
                    new IconPackViewModel(this, "Modern", typeof(PackIconModernKind), typeof(PackIconModern)),
                    new IconPackViewModel(this, "Entypo+", typeof(PackIconEntypoKind), typeof(PackIconEntypo))
                });
            this.IconPacksVersion = Assembly.GetAssembly(typeof(PackIconMaterial)).GetName().Version.ToString();
            this.GoToGitHubCommand =
                new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = x => System.Diagnostics.Process.Start("https://github.com/MahApps/MahApps.Metro.IconPacks")
                };
        }

        public ObservableCollection<IconPackViewModel> IconPacks { get; set; }

        public string AppVersion
        {
            get { return this._appVersion; }
            set
            {
                if (value == this._appVersion) return;
                this._appVersion = value;
                this.OnPropertyChanged();
            }
        }

        public string IconPacksVersion
        {
            get { return this._iconPacksVersion; }
            set
            {
                if (value == this._iconPacksVersion) return;
                this._iconPacksVersion = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand GoToGitHubCommand
        {
            get { return this._goToGitHubCommand; }
            set
            {
                if (Equals(value, this._goToGitHubCommand)) return;
                this._goToGitHubCommand = value;
                this.OnPropertyChanged();
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
                foreach (var iconPack in this.IconPacks)
                {
                    this._dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => iconPack.FilterText = value));
                }
            }
        }
    }
}