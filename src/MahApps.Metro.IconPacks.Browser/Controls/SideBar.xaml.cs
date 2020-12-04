using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks.Browser.Model;
using MahApps.Metro.IconPacks.Browser.Properties;
using MahApps.Metro.IconPacks.Browser.ViewModels;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using io = System.IO;
using System.Windows.Markup;
using System.Windows;

namespace MahApps.Metro.IconPacks.Browser.Controls
{
    /// <summary>
    /// Interaction logic for SideBar.xaml
    /// </summary>
    public partial class SideBar : UserControl
    {
        IIconViewModel SelectedIcon => DataContext as IIconViewModel;

        public SideBar()
        {
            SaveAsSvg_Command = new SimpleCommand((_) => SaveAsSvg_Execute(), (_) => DataContext is IIconViewModel);
            SaveAsWpf_Command = new SimpleCommand((_) => SaveAsWpf_Execute(), (_) => DataContext is IIconViewModel);
            SaveAsUwp_Command = new SimpleCommand((_) => SaveAsUwp_Execute(), (_) => DataContext is IIconViewModel);
            SaveAsBitmap_Command = new SimpleCommand((_) => SaveAsBitmap_Execute(), (_) => DataContext is IIconViewModel);

            InitializeComponent();
        }

        private void IconPreview_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta != 0 && Keyboard.Modifiers == ModifierKeys.Control)
            {
                Settings.Default.IconPreviewSize += Math.Sign(e.Delta) * 4;
                e.Handled = true;
            }
            else if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                this.PreviewScrollViewer.ScrollToHorizontalOffset(this.PreviewScrollViewer.HorizontalOffset - e.Delta / 3);
                e.Handled = true;
            }
        }

        public ICommand SaveAsSvg_Command { get; }

        private async void SaveAsSvg_Execute()
        {
            var progress = await this.TryFindParent<MetroWindow>()?.ShowProgressAsync("Export", "Saving selected icon as SVG-file");
            progress.SetIndeterminate();

            var fileSaveDialog = new SaveFileDialog()
            {
                AddExtension = true,
                DefaultExt = "svg",
                FileName=$"{SelectedIcon?.IconPackType.Name.Remove(0,8)}-{SelectedIcon.Name}",
                Filter = "SVG Drawing (*.svg)|*.svg",
                OverwritePrompt = true
            };

            if (fileSaveDialog.ShowDialog() == true)
            {
                string svgFileContent;

                var iconContol = PreviewHolder.FindChild<PackIconControlBase>();
                var iconPath = PreviewHolder.FindChild<Path>();

                var bBox = iconPath.Data.Bounds;

                var svgSize = Math.Max(bBox.Width, bBox.Height);
                var T = iconPath.LayoutTransform.Value;

                var transform =string.Join(",", new[]
                {
                    T.M11.ToString(CultureInfo.InvariantCulture),
                    T.M21.ToString(CultureInfo.InvariantCulture),
                    T.M12.ToString(CultureInfo.InvariantCulture),
                    T.M22.ToString(CultureInfo.InvariantCulture),
                    (T.M11*(T.OffsetX - bBox.Left + T.M11*(svgSize - bBox.Width)/2) + (bBox.Width - T.M11 * bBox.Width) / 2).ToString(CultureInfo.InvariantCulture),
                    (T.M22*(T.OffsetY - (bBox.Top - T.M22*(svgSize - bBox.Height)/2)) + (bBox.Height - T.M22 * bBox.Height) / 2).ToString(CultureInfo.InvariantCulture)
                });

                var parameters = new ExportParameters(SelectedIcon)
                {
                    FillColor = iconPath.Fill is Brush ? iconPath.Fill.ToString(CultureInfo.InvariantCulture).Remove(1,2) : "none", // We need to remove the alpha channel for svg
                    PageHeight = svgSize.ToString(CultureInfo.InvariantCulture),
                    PageWidth = svgSize.ToString(CultureInfo.InvariantCulture),
                    PathData = iconContol.Data,
                    StrokeColor = iconPath.Stroke is Brush ? iconPath.Stroke.ToString(CultureInfo.InvariantCulture).Remove(1, 2) : "none", // We need to remove the alpha channel for svg
                    StrokeWidth = iconPath.Stroke is null ? "0" : iconPath.StrokeThickness.ToString(CultureInfo.InvariantCulture),
                    StrokeLineCap = iconPath.StrokeEndLineCap.ToString().ToLower(),
                    StrokeLineJoin = iconPath.StrokeLineJoin.ToString().ToLower(),
                    TranformMatrix = transform
                };

                var svgFileTemplate = ExportHelper.SvgFileTemplate;

                svgFileContent = ExportHelper.FillTemplate(svgFileTemplate, parameters);

                using (io.StreamWriter file = new io.StreamWriter(fileSaveDialog.FileName))
                {
                    file.Write(svgFileContent);
                }

            }
            await progress.CloseAsync();
        }


        public ICommand SaveAsWpf_Command { get; }
        private async void SaveAsWpf_Execute()
        {
            var progress = await this.TryFindParent<MetroWindow>()?.ShowProgressAsync("Export", "Saving selected icon as WPF-XAML-file");
            progress.SetIndeterminate();

            var fileSaveDialog = new SaveFileDialog()
            {
                AddExtension = true,
                DefaultExt = "xaml",
                FileName = $"{SelectedIcon?.IconPackType.Name.Remove(0, 8)}-{SelectedIcon.Name}",
                Filter = "WPF-XAML (*.xaml)|*.xaml",
                OverwritePrompt = true
            };

            if (fileSaveDialog.ShowDialog() == true)
            {
                string wpfFileContent;

                var iconContol = PreviewHolder.FindChild<PackIconControlBase>();
                var iconPath = PreviewHolder.FindChild<Path>();

                var bBox = iconPath.Data.Bounds;

                var xamlSize = Math.Max(bBox.Width, bBox.Height);
                var T = iconPath.LayoutTransform.Value; 


                var wpfFileTemplate = ExportHelper.WpfFileTemplate;

                var parameters = new ExportParameters(SelectedIcon)
                {
                    FillColor = iconPath.Fill is Brush ? iconPath.Fill.ToString(CultureInfo.InvariantCulture) : "{x:Null}",
                    PageHeight = xamlSize.ToString(CultureInfo.InvariantCulture),
                    PageWidth = xamlSize.ToString(CultureInfo.InvariantCulture),
                    PathData = iconContol.Data,
                    StrokeColor = iconPath.Stroke is Brush ? iconPath.Stroke.ToString(CultureInfo.InvariantCulture) : "{x:Null}",
                    StrokeWidth = iconPath.Stroke is null ? "0" : iconPath.StrokeThickness.ToString(CultureInfo.InvariantCulture),
                    StrokeLineCap = iconPath.StrokeEndLineCap.ToString().ToLower(),
                    StrokeLineJoin = iconPath.StrokeLineJoin.ToString().ToLower(),
                    TranformMatrix = T.ToString(CultureInfo.InvariantCulture)
                };

                wpfFileContent = ExportHelper.FillTemplate(wpfFileTemplate, parameters);


                using (io.StreamWriter file = new io.StreamWriter(fileSaveDialog.FileName))
                {
                    file.Write(wpfFileContent);
                }

            }
            await progress.CloseAsync();
        }

        public ICommand SaveAsUwp_Command { get; }
        private async void SaveAsUwp_Execute()
        {
            var progress = await this.TryFindParent<MetroWindow>()?.ShowProgressAsync("Export", "Saving selected icon as WPF-XAML-file");
            progress.SetIndeterminate();

            var fileSaveDialog = new SaveFileDialog()
            {
                AddExtension = true,
                DefaultExt = "xaml",
                FileName = $"{SelectedIcon?.IconPackType.Name.Remove(0, 8)}-{SelectedIcon.Name}",
                Filter = "UWP-XAML (*.xaml)|*.xaml",
                OverwritePrompt = true
            };

            if (fileSaveDialog.ShowDialog() == true)
            {
                string wpfFileContent;

                var iconContol = PreviewHolder.FindChild<PackIconControlBase>();
                var iconPath = PreviewHolder.FindChild<Path>();

                var bBox = iconPath.Data.Bounds;

                var xamlSize = Math.Max(bBox.Width, bBox.Height);
                var T = iconPath.LayoutTransform.Value;


                var wpfFileTemplate = ExportHelper.UwpFileTemplate;

                var parameters = new ExportParameters(SelectedIcon)
                {
                    FillColor = iconPath.Fill is Brush ? iconPath.Fill.ToString(CultureInfo.InvariantCulture) : "{x:Null}",
                    PageHeight = xamlSize.ToString(CultureInfo.InvariantCulture),
                    PageWidth = xamlSize.ToString(CultureInfo.InvariantCulture),
                    PathData = iconContol.Data,
                    StrokeColor = iconPath.Stroke is Brush ? iconPath.Stroke.ToString(CultureInfo.InvariantCulture) : "{x:Null}",
                    StrokeWidth = iconPath.Stroke is null ? "0" : iconPath.StrokeThickness.ToString(CultureInfo.InvariantCulture),
                    StrokeLineCap = iconPath.StrokeEndLineCap.ToString().ToLower(),
                    StrokeLineJoin = iconPath.StrokeLineJoin.ToString().ToLower(),
                    TranformMatrix = T.ToString(CultureInfo.InvariantCulture)
                };

                wpfFileContent = ExportHelper.FillTemplate(wpfFileTemplate, parameters);


                using (io.StreamWriter file = new io.StreamWriter(fileSaveDialog.FileName))
                {
                    file.Write(wpfFileContent);
                }

            }
            await progress.CloseAsync();
        }


        public ICommand SaveAsBitmap_Command { get; }

        private async void SaveAsBitmap_Execute()
        {
            var progress = await this.TryFindParent<MetroWindow>()?.ShowProgressAsync("Export", "Saving selected icon as bitmap image");
            progress.SetIndeterminate();

            var fileSaveDialog = new SaveFileDialog()
            {
                AddExtension = true,
                FileName = $"{SelectedIcon?.IconPackType.Name.Remove(0, 8)}-{SelectedIcon.Name}",
                Filter = "PNG-File (*.png)|*.png|JPEG-File (*.jpg)|*.jpg|BMP-File (*.bmp)|*.bmp",
                OverwritePrompt = true
            };

            if (fileSaveDialog.ShowDialog() == true)
            {
                BitmapEncoder encoder;

                string wpfContent;

                var iconContol = PreviewHolder.FindChild<PackIconControlBase>();
                var iconPath = PreviewHolder.FindChild<Path>();

                var T = iconPath.LayoutTransform.Value;

                var bitmapTemplate = ExportHelper.BitmapImageTemplate;

                var parameters = new ExportParameters(SelectedIcon)
                {
                    FillColor = iconPath.Fill is Brush ? iconPath.Fill.ToString(CultureInfo.InvariantCulture) : "{x:Null}",
                    PageHeight = Settings.Default.IconPreviewSize.ToString(CultureInfo.InvariantCulture),
                    PageWidth = Settings.Default.IconPreviewSize.ToString(CultureInfo.InvariantCulture),
                    PathData = iconContol.Data,
                    StrokeColor = iconPath.Stroke is Brush ? iconPath.Stroke.ToString(CultureInfo.InvariantCulture) : "{x:Null}",
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
               
                switch (io.Path.GetExtension(fileSaveDialog.FileName).ToLowerInvariant())
                {
                    case ".png":
                        encoder = new PngBitmapEncoder();
                        break;
                    case ".jpg":
                        encoder = new JpegBitmapEncoder();
                        break;
                    case ".bmp":
                        encoder = new BmpBitmapEncoder();
                        break;
                    default:
                        await this.TryFindParent<MetroWindow>()?.ShowMessageAsync("Error", $"You selected a wrong file type. Currently images of type \"{io.Path.GetExtension(fileSaveDialog.FileName)}\" are not supported");
                        return;
                }

                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

                using var fileStream = new io.FileStream(fileSaveDialog.FileName, io.FileMode.Create);
                encoder.Save(fileStream);
            }
            await progress.CloseAsync();
        }
    }
}
