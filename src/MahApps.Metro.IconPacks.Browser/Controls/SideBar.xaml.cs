using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks.Browser.Properties;
using MahApps.Metro.IconPacks.Browser.ViewModels;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using io = System.IO;

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
            SaveAsSvg_Command = new SimpleCommand((_) => SaveAsSvg_Execute(),(_) => DataContext is IIconViewModel);
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

                var svgFileTemplate = io.File.ReadAllText(io.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExportTemplates", "SVG.xml"));

                svgFileContent = svgFileTemplate
                    .Replace("{{PageWidth}}", svgSize.ToString(CultureInfo.InvariantCulture))
                    .Replace("{{PageHeight}}", svgSize.ToString(CultureInfo.InvariantCulture))
                    .Replace("{{PathData}}", iconContol.Data)
                    .Replace("{{FillColor}}", iconPath.Fill is Brush ? "black" : "none")
                    .Replace("{{StrokeColor}}", iconPath.Stroke is Brush ? "black" : "none")
                    .Replace("{{StrokeWidth}}", iconPath.Stroke is null ? "0" : iconPath.StrokeThickness.ToString(CultureInfo.InvariantCulture))
                    .Replace("{{StrokeLineCap}}", iconPath.StrokeEndLineCap.ToString().ToLower())
                    .Replace("{{StrokeLineJoin}}", iconPath.StrokeLineJoin.ToString().ToLower())
                    .Replace("{{TranformMatrix}}", transform);

                using (io.StreamWriter file = new io.StreamWriter(fileSaveDialog.FileName))
                {
                    file.Write(svgFileContent);
                }

            }
            await progress.CloseAsync();
        }

    }
}
