using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDialogCoordinator dialogCoordinator;
        private readonly Dispatcher _dispatcher;
        private string _filterText;
        private static MainViewModel _Instance;

        public MainViewModel(Dispatcher dispatcher)
        {
            if (_Instance is null)
            {
                _Instance = this;
            }
            else
            {
                return;
            }

            this.dialogCoordinator = DialogCoordinator.Instance;
            this._dispatcher = dispatcher;
            this.AppVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            
            var availableIconPacks = new List<(Type EnumType, Type IconPackType)>(
                new []
                {
                    (typeof(PackIconBootstrapIconsKind), typeof(PackIconBootstrapIcons)),
                    (typeof(PackIconBoxIconsKind), typeof(PackIconBoxIcons)),
                    (typeof(PackIconCodiconsKind), typeof(PackIconCodicons)),
                    (typeof(PackIconCooliconsKind), typeof(PackIconCoolicons)),
                    (typeof(PackIconEntypoKind), typeof(PackIconEntypo)),
                    (typeof(PackIconEvaIconsKind), typeof(PackIconEvaIcons)),
                    (typeof(PackIconFeatherIconsKind), typeof(PackIconFeatherIcons)),
                    (typeof(PackIconFileIconsKind), typeof(PackIconFileIcons)),
                    (typeof(PackIconFontaudioKind), typeof(PackIconFontaudio)),
                    (typeof(PackIconFontAwesomeKind), typeof(PackIconFontAwesome)),
                    (typeof(PackIconFontistoKind), typeof(PackIconFontisto)),
                    (typeof(PackIconForkAwesomeKind), typeof(PackIconForkAwesome)),
                    (typeof(PackIconIoniconsKind), typeof(PackIconIonicons)),
                    (typeof(PackIconJamIconsKind), typeof(PackIconJamIcons)),
                    (typeof(PackIconMaterialKind), typeof(PackIconMaterial)),
                    (typeof(PackIconMaterialDesignKind), typeof(PackIconMaterialDesign)),
                    (typeof(PackIconMaterialLightKind), typeof(PackIconMaterialLight)),
                    (typeof(PackIconMicronsKind), typeof(PackIconMicrons)),
                    (typeof(PackIconModernKind), typeof(PackIconModern)),
                    (typeof(PackIconOcticonsKind), typeof(PackIconOcticons)),
                    (typeof(PackIconPicolIconsKind), typeof(PackIconPicolIcons)),
                    (typeof(PackIconPixelartIconsKind), typeof(PackIconPixelartIcons)),
                    (typeof(PackIconRadixIconsKind), typeof(PackIconRadixIcons)),
                    (typeof(PackIconRemixIconKind), typeof(PackIconRemixIcon)),
                    (typeof(PackIconRPGAwesomeKind), typeof(PackIconRPGAwesome)),
                    (typeof(PackIconSimpleIconsKind), typeof(PackIconSimpleIcons)),
                    (typeof(PackIconTypiconsKind), typeof(PackIconTypicons)),
                    (typeof(PackIconUniconsKind), typeof(PackIconUnicons)),
                    (typeof(PackIconVaadinIconsKind), typeof(PackIconVaadinIcons)),
                    (typeof(PackIconWeatherIconsKind), typeof(PackIconWeatherIcons)),
                    (typeof(PackIconZondiconsKind), typeof(PackIconZondicons)),
                });



            this.IconPacks = new ObservableCollection<IconPackViewModel>();

            foreach (var (EnumType, IconPackType) in availableIconPacks)
            {
                IconPacks.Add(new IconPackViewModel(this, EnumType, IconPackType, dialogCoordinator));
            }

            this.AllIconPacksCollection = new List<IconPackViewModel>(1)
            {
                new IconPackViewModel(this, "All Icons", availableIconPacks.Select(x => x.EnumType).ToArray(), availableIconPacks.Select(x=> x.IconPackType).ToArray(), dialogCoordinator)
            };

            this.IconPacksVersion = FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(PackIconMaterial)).Location).FileVersion;

            this.Settings = new SettingsViewModel(this.dialogCoordinator);
        }

        private async static void OpenUrlLink(string link)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = link ?? throw new System.ArgumentNullException(nameof(link)),
                    // UseShellExecute is default to false on .NET Core while true on .NET Framework.
                    // Only this value is set to true, the url link can be opened.
                    UseShellExecute = true,
                });
            }
            catch (Exception e)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(MainViewModel._Instance , "Error", e.Message);
            }
        }

        public ObservableCollection<IconPackViewModel> IconPacks { get; }

        public List<IconPackViewModel> AllIconPacksCollection { get; }

        public string AppVersion { get; }

        public string IconPacksVersion { get; }

        public static ICommand OpenUrlCommand { get; } =
            new SimpleCommand
            {
                CanExecuteDelegate = x => !string.IsNullOrWhiteSpace(x as string),
                ExecuteDelegate = x => OpenUrlLink(x as string)
            };

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (Set(ref _filterText, value))
                {
                    foreach (var iconPack in this.IconPacks)
                    {
                        this._dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => iconPack.FilterText = value));
                    }
                    foreach (var iconPack in this.AllIconPacksCollection)
                    {
                        this._dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => iconPack.FilterText = value));
                    }
                }
            }
        }

        public static ICommand CopyTextToClipboardCommand { get; } =
            new SimpleCommand
            {
                CanExecuteDelegate = x => (x is string),
                ExecuteDelegate = x => Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Clipboard.SetDataObject(x as string);
                }))
            };

        public SettingsViewModel Settings { get; }
    }
}