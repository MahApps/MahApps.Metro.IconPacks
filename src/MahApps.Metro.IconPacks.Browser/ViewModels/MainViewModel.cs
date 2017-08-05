﻿using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Brush _selectedBursh;
        private IconPackViewModel _selectedIconPackViewModel;

        private string _appVersion;
        private string _iconPacksVersion;
        private string _filterText;

        private ICommand _goToGitHubCommand;

        private readonly Dispatcher _dispatcher;

        public MainViewModel(Dispatcher dispatcher)
        {
            this._dispatcher = dispatcher;
            this.AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.IconPacks = new ObservableCollection<IconPackViewModel>(
                new[]
                {
                    new IconPackViewModel(this, "Material", typeof(PackIconMaterialKind), typeof(PackIconMaterial)),
                    new IconPackViewModel(this, "MaterialLight", typeof(PackIconMaterialLightKind), typeof(PackIconMaterialLight)),
                    new IconPackViewModel(this, "FontAwesome", typeof(PackIconFontAwesomeKind), typeof(PackIconFontAwesome)),
                    new IconPackViewModel(this, "Octicons", typeof(PackIconOcticonsKind), typeof(PackIconOcticons)),
                    new IconPackViewModel(this, "Modern", typeof(PackIconModernKind), typeof(PackIconModern)),
                    new IconPackViewModel(this, "Entypo+", typeof(PackIconEntypoKind), typeof(PackIconEntypo)),
                    new IconPackViewModel(this, "SimpleIcons", typeof(PackIconSimpleIconsKind), typeof(PackIconSimpleIcons)),
                    new IconPackViewModel(this, "All",
                                          new Type[] { typeof(PackIconMaterialKind), typeof(PackIconMaterialLightKind), typeof(PackIconFontAwesomeKind), typeof(PackIconOcticonsKind), typeof(PackIconModernKind), typeof(PackIconEntypoKind), typeof(PackIconSimpleIconsKind) },
                                          new Type[] { typeof(PackIconMaterial), typeof(PackIconMaterialLight), typeof(PackIconFontAwesome), typeof(PackIconOcticons), typeof(PackIconModern), typeof(PackIconEntypo), typeof(PackIconSimpleIcons) })
                    });
            this.IconPacksVersion = Assembly.GetAssembly(typeof(PackIconMaterial)).GetName().Version.ToString();
            this.ImageGeneratorViewModel = new ImageGeneratorViewModel(this);
            this.GoToGitHubCommand =
                new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = x => System.Diagnostics.Process.Start("https://github.com/MahApps/MahApps.Metro.IconPacks")
                };
        }

        public ObservableCollection<IconPackViewModel> IconPacks { get; set; }
        public ImageGeneratorViewModel ImageGeneratorViewModel { get; }

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

        public Brush SelectedBrush
        {
            get { return this._selectedBursh; }
            set
            {
                if (Equals(value, this._selectedBursh)) return;
                this._selectedBursh = value;
                this.OnPropertyChanged();
            }
        }

        public IconPackViewModel SelectedIconPackViewModel
        {
            get { return this._selectedIconPackViewModel; }
            set
            {
                if (value == this._selectedIconPackViewModel) return;
                this._selectedIconPackViewModel = value;
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
