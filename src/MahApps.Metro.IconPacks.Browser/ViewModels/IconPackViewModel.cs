using MahApps.Metro.IconPacks.Browser.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using AsyncAwaitBestPractices;
using Microsoft.Win32;
using MahApps.Metro.Controls.Dialogs;
using System.Globalization;
using MahApps.Metro.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using io = System.IO;
using MahApps.Metro.IconPacks.Browser.Properties;
using System.Windows.Media.Imaging;
using System.Windows.Markup;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public class IconPackViewModel : ViewModelBase
    {
        private IEnumerable<IIconViewModel> _icons;
        private int _iconCount;
        private ICollectionView _iconsCollectionView;
        private string _filterText;
        private IIconViewModel _selectedIcon;
        private string _projectUrl;
        private string _licenseUrl;
        private readonly IDialogCoordinator dialogCoordinator;


        private IconPackViewModel(MainViewModel mainViewModel, IDialogCoordinator dialogCoordinator)
        {
            this.MainViewModel = mainViewModel;
            this.dialogCoordinator = dialogCoordinator;

            // Export commands
            SaveAsSvg_Command = new SimpleCommand((_) => SaveAsSvg_Execute(), (_) => SelectedIcon is IconViewModel);
            SaveAsWpf_Command = new SimpleCommand((_) => SaveAsWpf_Execute(), (_) => !(SelectedIcon is null));
            SaveAsUwp_Command = new SimpleCommand((_) => SaveAsUwp_Execute(), (_) => !(SelectedIcon is null));
            SaveAsBitmap_Command = new SimpleCommand((_) => SaveAsBitmap_Execute(), (_) => !(SelectedIcon is null));
        }

        public IconPackViewModel(MainViewModel mainViewModel, Type enumType, Type packType, IDialogCoordinator dialogCoordinator) : this(mainViewModel, dialogCoordinator)
        {
            // Get the Name of the IconPack via Attributes
            var attributes = Attribute.GetCustomAttribute(packType, typeof(MetaDataAttribute)) as MetaDataAttribute;
            this.Caption = attributes.Name;

            this.LoadEnumsAsync(enumType, packType).SafeFireAndForget();
        }

        public IconPackViewModel(MainViewModel mainViewModel, string caption, Type[] enumTypes, Type[] packTypes, IDialogCoordinator dialogCoordinator) : this(mainViewModel, dialogCoordinator)
        {
            this.MainViewModel = mainViewModel;
            this.Caption = caption;

            this.LoadAllEnumsAsync(enumTypes, packTypes).SafeFireAndForget();
        }

        private async Task LoadEnumsAsync(Type enumType, Type packType)
        {
            var collection = await Task.Run(() => GetIcons(enumType, packType).OrderBy(i => i.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

            this.Icons = new ObservableCollection<IIconViewModel>(collection);
            this.IconCount = ((ICollection)this.Icons).Count;
            this.PrepareFiltering();
            this.SelectedIcon = this.Icons.First();
        }

        private async Task LoadAllEnumsAsync(Type[] enumTypes, Type[] packTypes)
        {
            var collection = await Task.Run(() =>
            {
                var allIcons = Enumerable.Empty<IIconViewModel>();
                for (var counter = 0; counter < enumTypes.Length; counter++)
                {
                    allIcons = allIcons.Concat(GetIcons(enumTypes[counter], packTypes[counter]));
                }

                return allIcons.OrderBy(i => i.Name, StringComparer.InvariantCultureIgnoreCase).ToList();
            });

            this.Icons = new ObservableCollection<IIconViewModel>(collection);
            this.IconCount = ((ICollection)this.Icons).Count;
            this.PrepareFiltering();
            this.SelectedIcon = this.Icons.First();
        }

        private void PrepareFiltering()
        {
            this._iconsCollectionView = CollectionViewSource.GetDefaultView(this.Icons);
            this._iconsCollectionView.Filter = o => this.FilterIconsPredicate(this.FilterText, (IIconViewModel)o);
        }

        private bool FilterIconsPredicate(string filterText, IIconViewModel iconViewModel)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return true;
            }
            else
            {
                var filterSubStrings = filterText.Split(new char[] { '+', ',', ';', '&' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var filterSubString in filterSubStrings)
                {
                    var filterOrSubStrings = filterSubString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    var isInName = filterOrSubStrings.Any(x => iconViewModel.Name.IndexOf(x.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    var isInDescription = filterOrSubStrings.Any(x => (iconViewModel.Description?.IndexOf(x.Trim(), StringComparison.CurrentCultureIgnoreCase) ?? -1) >= 0);

                    if (!(isInName || isInDescription)) return false;
                }

                return true;
            }
        }

        private static string GetDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            return fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() is DescriptionAttribute attribute ? attribute.Description : value.ToString();
        }

        private static IEnumerable<IIconViewModel> GetIcons(Type enumType, Type packType)
        {
            return Enum.GetValues(enumType)
                       .OfType<Enum>()
                       .Where(k => k.ToString() != "None")
                       .Select(k => GetIconViewModel(enumType, packType, k));
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

        public MainViewModel MainViewModel { get; }

        public string Caption { get; }

        public IEnumerable<IIconViewModel> Icons
        {
            get { return _icons; }
            set { Set(ref _icons, value); }
        }

        public int IconCount
        {
            get { return _iconCount; }
            set { Set(ref _iconCount, value); }
        }

        public string ProjectUrl
        {
            get { return _projectUrl; }
            set { Set(ref _projectUrl, value); }
        }

        public string LicenseUrl
        {
            get { return _licenseUrl; }
            set { Set(ref _licenseUrl, value); }
        }

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (Set(ref _filterText, value))
                {
                    this._iconsCollectionView?.Refresh();

                }
            }
        }

        public IIconViewModel SelectedIcon
        {
            get { return _selectedIcon; }
            set
            {
                if (Set(ref _selectedIcon, value) && !(_selectedIcon is null))
                {
                    var metaData = Attribute.GetCustomAttribute(_selectedIcon.IconPackType, typeof(MetaDataAttribute)) as MetaDataAttribute;
                    this.ProjectUrl = metaData != null ? metaData.ProjectUrl : string.Empty;
                    this.LicenseUrl = metaData != null ? metaData.LicenseUrl : string.Empty;
                }
            }
        }

        public ICommand SaveAsSvg_Command { get; }

        private async void SaveAsSvg_Execute()
        {
            var progress = await dialogCoordinator.ShowProgressAsync(MainViewModel, "Export", "Saving selected icon as SVG-file");
            progress.SetIndeterminate();

            try
            {
                var fileSaveDialog = new SaveFileDialog()
                {
                    AddExtension = true,
                    DefaultExt = "svg",
                    FileName = $"{SelectedIcon.IconPackName}-{SelectedIcon.Name}",
                    Filter = "SVG Drawing (*.svg)|*.svg",
                    OverwritePrompt = true
                };

                if (fileSaveDialog.ShowDialog() == true && SelectedIcon is IconViewModel icon)
                {
                    string svgFileContent;

                    var iconContol = icon.GetPackIconControlBase();

                    iconContol.BeginInit();
                    iconContol.Width = Settings.Default.IconPreviewSize;
                    iconContol.Height = Settings.Default.IconPreviewSize;
                    iconContol.EndInit();
                    iconContol.ApplyTemplate();

                    var iconPath = iconContol.FindChild<Path>();

                    var bBox = iconPath.Data.Bounds;

                    var svgSize = Math.Max(bBox.Width, bBox.Height);
                    var scaleFactor = Settings.Default.IconPreviewSize / svgSize;
                    var T = iconPath.LayoutTransform.Value;

                    T.Translate(-bBox.Left - (T.M11 < 0 ? bBox.Width : 0) + Math.Sign(T.M11) * (svgSize - bBox.Width)/2 , 
                                -bBox.Top - (T.M22 < 0 ? bBox.Height : 0) + Math.Sign(T.M22) * (svgSize - bBox.Height) / 2);
                    T.Scale(scaleFactor, scaleFactor);


                    var transform = string.Join(",", new[]
                    {
                        T.M11.ToString(CultureInfo.InvariantCulture),
                        T.M21.ToString(CultureInfo.InvariantCulture),
                        T.M12.ToString(CultureInfo.InvariantCulture),
                        T.M22.ToString(CultureInfo.InvariantCulture),
                        (Math.Sign(T.M11)*T.OffsetX).ToString(CultureInfo.InvariantCulture),
                        (Math.Sign(T.M22)*T.OffsetY).ToString(CultureInfo.InvariantCulture)
                    });

                    var parameters = new ExportParameters(SelectedIcon)
                    {
                        FillColor = iconPath.Fill is Brush ? Settings.Default.IconForeground.ToString(CultureInfo.InvariantCulture).Remove(1, 2) : "none", // We need to remove the alpha channel for svg
                        Background = Settings.Default.IconBackground.ToString(CultureInfo.InvariantCulture).Remove(1, 2),
                        PathData = iconContol.Data,
                        StrokeColor = iconPath.Stroke is Brush ? Settings.Default.IconForeground.ToString(CultureInfo.InvariantCulture).Remove(1, 2) : "none", // We need to remove the alpha channel for svg
                        StrokeWidth = iconPath.Stroke is null ? "0" : (scaleFactor * iconPath.StrokeThickness).ToString(CultureInfo.InvariantCulture),
                        StrokeLineCap = iconPath.StrokeEndLineCap.ToString().ToLower(),
                        StrokeLineJoin = iconPath.StrokeLineJoin.ToString().ToLower(),
                        TranformMatrix = transform
                    };

                    var svgFileTemplate = ExportHelper.SvgFileTemplate;

                    svgFileContent = ExportHelper.FillTemplate(svgFileTemplate, parameters);

                    using io.StreamWriter file = new io.StreamWriter(fileSaveDialog.FileName);
                    file.Write(svgFileContent);
                }
            }
            catch (Exception e)
            {
                await dialogCoordinator.ShowMessageAsync(MainViewModel, "Error", e.Message);
            }

            await progress.CloseAsync();
        }


        public ICommand SaveAsWpf_Command { get; }
        private async void SaveAsWpf_Execute()
        {
            var progress = await dialogCoordinator.ShowProgressAsync(MainViewModel, "Export", "Saving selected icon as WPF-XAML-file");
            progress.SetIndeterminate();

            try
            {
                var fileSaveDialog = new SaveFileDialog()
                {
                    AddExtension = true,
                    DefaultExt = "xaml",
                    FileName = $"{SelectedIcon.IconPackName}-{SelectedIcon.Name}",
                    Filter = "WPF-XAML (*.xaml)|*.xaml",
                    OverwritePrompt = true
                };

                if (fileSaveDialog.ShowDialog() == true && SelectedIcon is IconViewModel icon)
                {
                    string wpfFileContent;

                    var iconContol = icon.GetPackIconControlBase();

                    iconContol.BeginInit();
                    iconContol.Width = Settings.Default.IconPreviewSize;
                    iconContol.Height = Settings.Default.IconPreviewSize;
                    iconContol.EndInit();
                    iconContol.ApplyTemplate();

                    var iconPath = iconContol.FindChild<Path>();

                    var bBox = iconPath.Data.Bounds;

                    var xamlSize = Math.Max(bBox.Width, bBox.Height);
                    var T = iconPath.LayoutTransform.Value;

                    var scaleFactor = Settings.Default.IconPreviewSize / xamlSize;

                    var wpfFileTemplate = ExportHelper.WpfFileTemplate;

                    var parameters = new ExportParameters(SelectedIcon)
                    {
                        FillColor = iconPath.Fill is Brush ? Settings.Default.IconForeground.ToString(CultureInfo.InvariantCulture) : "{x:Null}",
                        PathData = iconContol.Data,
                        StrokeColor = iconPath.Stroke is Brush ? Settings.Default.IconForeground.ToString(CultureInfo.InvariantCulture) : "{x:Null}",
                        StrokeWidth = iconPath.Stroke is null ? "0" : (scaleFactor * iconPath.StrokeThickness).ToString(CultureInfo.InvariantCulture),
                        StrokeLineCap = iconPath.StrokeEndLineCap.ToString().ToLower(),
                        StrokeLineJoin = iconPath.StrokeLineJoin.ToString().ToLower(),
                        TranformMatrix = T.ToString(CultureInfo.InvariantCulture)
                    };

                    wpfFileContent = ExportHelper.FillTemplate(wpfFileTemplate, parameters);


                    using io.StreamWriter file = new io.StreamWriter(fileSaveDialog.FileName);
                    file.Write(wpfFileContent);

                }
            }
            catch (Exception e)
            {
                await dialogCoordinator.ShowMessageAsync(MainViewModel, "Error", e.Message);
            }
            await progress.CloseAsync();
        }

        public ICommand SaveAsUwp_Command { get; }
        private async void SaveAsUwp_Execute()
        {
            var progress = await dialogCoordinator.ShowProgressAsync(MainViewModel, "Export", "Saving selected icon as WPF-XAML-file");
            progress.SetIndeterminate();

            try
            {
                var fileSaveDialog = new SaveFileDialog()
                {
                    AddExtension = true,
                    DefaultExt = "xaml",
                    FileName = $"{SelectedIcon.IconPackName}-{SelectedIcon.Name}",
                    Filter = "UWP-XAML (*.xaml)|*.xaml",
                    OverwritePrompt = true
                };

                if (fileSaveDialog.ShowDialog() == true && SelectedIcon is IconViewModel icon)
                {
                    string wpfFileContent;

                    var iconContol = icon.GetPackIconControlBase();

                    iconContol.BeginInit();
                    iconContol.Width = Settings.Default.IconPreviewSize;
                    iconContol.Height = Settings.Default.IconPreviewSize;
                    iconContol.EndInit();
                    iconContol.ApplyTemplate();

                    var iconPath = iconContol.FindChild<Path>();

                    var bBox = iconPath.Data.Bounds;

                    var xamlSize = Math.Max(bBox.Width, bBox.Height);
                    var scaleFactor = Settings.Default.IconPreviewSize / xamlSize;
                    var T = iconPath.LayoutTransform.Value;


                    var wpfFileTemplate = ExportHelper.UwpFileTemplate;

                    var parameters = new ExportParameters(SelectedIcon)
                    {
                        FillColor = iconPath.Fill is Brush ? iconPath.Fill.ToString(CultureInfo.InvariantCulture) : "{x:Null}",
                        PathData = iconContol.Data,
                        StrokeColor = iconPath.Stroke is Brush ? iconPath.Stroke.ToString(CultureInfo.InvariantCulture) : "{x:Null}",
                        StrokeWidth = iconPath.Stroke is null ? "0" : (scaleFactor * iconPath.StrokeThickness).ToString(CultureInfo.InvariantCulture),
                        StrokeLineCap = iconPath.StrokeEndLineCap.ToString().ToLower(),
                        StrokeLineJoin = iconPath.StrokeLineJoin.ToString().ToLower(),
                        TranformMatrix = T.ToString(CultureInfo.InvariantCulture)
                    };

                    wpfFileContent = ExportHelper.FillTemplate(wpfFileTemplate, parameters);


                    using io.StreamWriter file = new io.StreamWriter(fileSaveDialog.FileName);
                    file.Write(wpfFileContent);

                }
            }
            catch (Exception e)
            {
                await dialogCoordinator.ShowMessageAsync(MainViewModel, "Error", e.Message);
            }
            await progress.CloseAsync();
        }


        public ICommand SaveAsBitmap_Command { get; }

        private async void SaveAsBitmap_Execute()
        {
            var progress = await dialogCoordinator.ShowProgressAsync(MainViewModel, "Export", "Saving selected icon as bitmap image");
            progress.SetIndeterminate();

            try
            {
                var fileSaveDialog = new SaveFileDialog()
                {
                    AddExtension = true,
                    FileName = $"{SelectedIcon.IconPackName}-{SelectedIcon.Name}",
                    Filter = "PNG-File (*.png)|*.png|JPEG-File (*.jpg)|*.jpg|BMP-File (*.bmp)|*.bmp",
                    OverwritePrompt = true
                };

                if (fileSaveDialog.ShowDialog() == true && SelectedIcon is IconViewModel icon)
                {
                    string wpfContent;

                    var iconContol = icon.GetPackIconControlBase();

                    iconContol.BeginInit();
                    iconContol.Width = Settings.Default.IconPreviewSize;
                    iconContol.Height = Settings.Default.IconPreviewSize;
                    iconContol.EndInit();
                    iconContol.ApplyTemplate();

                    var iconPath = iconContol.FindChild<Path>();

                    var T = iconPath.LayoutTransform.Value;

                    var bitmapTemplate = ExportHelper.BitmapImageTemplate;

                    var parameters = new ExportParameters(SelectedIcon)
                    {
                        FillColor = Settings.Default.IconForeground.ToString(CultureInfo.InvariantCulture),
                        PageHeight = Settings.Default.IconPreviewSize.ToString(CultureInfo.InvariantCulture),
                        PageWidth = Settings.Default.IconPreviewSize.ToString(CultureInfo.InvariantCulture),
                        PathData = iconContol.Data,
                        StrokeWidth = iconPath.Stroke is null ? "0" : iconPath.StrokeThickness.ToString(CultureInfo.InvariantCulture),
                        StrokeLineCap = iconPath.StrokeEndLineCap.ToString().ToLower(),
                        StrokeLineJoin = iconPath.StrokeLineJoin.ToString().ToLower(),
                        TranformMatrix = T.ToString(CultureInfo.InvariantCulture)
                    };

                    wpfContent = ExportHelper.FillTemplate(bitmapTemplate, parameters);


                    var pathHolder = (UIElement)XamlReader.Parse(wpfContent);


                    var size = new Size(Settings.Default.IconPreviewSize, Settings.Default.IconPreviewSize);
                    pathHolder.Measure(size);
                    pathHolder.Arrange(new Rect(size));

                    var renderTargetBitmap = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);
                    renderTargetBitmap.Render(pathHolder);
                    BitmapEncoder encoder = (io.Path.GetExtension(fileSaveDialog.FileName).ToLowerInvariant()) switch
                    {
                        ".png" => new PngBitmapEncoder(),
                        ".jpg" => new JpegBitmapEncoder(),
                        ".bmp" => new BmpBitmapEncoder(),
                        _ => throw new io.FileFormatException($"You selected a wrong file type. Currently images of type \"{io.Path.GetExtension(fileSaveDialog.FileName)}\" are not supported"),
                    };
                    encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

                    using var fileStream = new io.FileStream(fileSaveDialog.FileName, io.FileMode.Create);
                    encoder.Save(fileStream);
                }
            }
            catch (Exception e)
            {
                await dialogCoordinator.ShowMessageAsync(MainViewModel, "Error", e.Message);
            }
            await progress.CloseAsync();
        }
    }

    public interface IIconViewModel
    {
        string Name { get; set; }
        string IconPackName { get; }
        string Description { get; set; }
        Type IconPackType { get; set; }
        Type IconType { get; set; }
        object Value { get; set; }

    }

    public class IconViewModel : ViewModelBase, IIconViewModel
    {
        public IconViewModel()
        {
        }

        public string CopyToClipboardText => ExportHelper.FillTemplate(ExportHelper.ClipboardWpf, new ExportParameters(this)); // $"<iconPacks:{IconPackType.Name} Kind=\"{Name}\" />";

        public string CopyToClipboardAsContentText => ExportHelper.FillTemplate(ExportHelper.ClipboardContent, new ExportParameters(this)); // $"{{iconPacks:{IconPackType.Name.Replace("PackIcon", "")} Kind={Name}}}";

        public string CopyToClipboardAsPathIconText => ExportHelper.FillTemplate(ExportHelper.ClipboardUwp, new ExportParameters(this)); // $"<iconPacks:{IconPackType.Name.Replace("PackIcon", "PathIcon")} Kind=\"{Name}\" />";

        public string CopyToClipboardAsGeometryText => ExportHelper.FillTemplate(ExportHelper.ClipboardData, new ExportParameters(this) { PathData = GetPackIconControlBase().Data }); // GetPackIconControlBase().Data;

        public string Name { get; set; }

        public string IconPackName => IconPackType.Name.Replace("PackIcon", "");

        public string Description { get; set; }

        public Type IconPackType { get; set; }

        public Type IconType { get; set; }

        public object Value { get; set; }


        internal PackIconControlBase GetPackIconControlBase()
        {
            if (!(Activator.CreateInstance(IconPackType) is PackIconControlBase iconPack)) return null;
            var kindProperty = IconPackType.GetProperty("Kind");
            if (kindProperty == null) return null;
            kindProperty.SetValue(iconPack, Value);

            return iconPack;
        }
    }
}