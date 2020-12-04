using MahApps.Metro.IconPacks.Browser.Properties;
using MahApps.Metro.IconPacks.Browser.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Browser.Model
{
    internal class ExportHelper
    {

        // SVG-File
        private static string _SvgFileTemplate;
        internal static string SvgFileTemplate => _SvgFileTemplate ??= File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExportTemplates", "SVG.xml"));


        // XAML-File (WPF)
        private static string _WpfFileTemplate;
        internal static string WpfFileTemplate => _WpfFileTemplate ??= File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExportTemplates", "WPF.xml"));

        // XAML-File (WPF)
        private static string _UwpFileTemplate;
        internal static string UwpFileTemplate => _UwpFileTemplate ??= File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExportTemplates", "WPF.xml"));

        // Bitmap-Image
        private static string _BitmapImageTemplate;
        internal static string BitmapImageTemplate => _BitmapImageTemplate ??= File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExportTemplates", "Bitmap.xml"));



        internal static string FillTemplate(string template, ExportParameters parameters) 
        {
            return template.Replace("@IconKind", parameters.IconKind)
                           .Replace("@IconPackName", parameters.IconPackName)
                           .Replace("@IconPackHomepage", parameters.IconPackHomepage)
                           .Replace("@IconPackLicense", parameters.IconPackLicense)
                           .Replace("@PageWidth", parameters.PageWidth)
                           .Replace("@PageHeight", parameters.PageHeight)
                           .Replace("@PathData", parameters.PathData)
                           .Replace("@FillColor", parameters.FillColor)
                           .Replace("@Background", parameters.Background)
                           .Replace("@StrokeColor", parameters.StrokeColor)
                           .Replace("@StrokeWidth", parameters.StrokeWidth)
                           .Replace("@StrokeLineCap", parameters.StrokeLineCap)
                           .Replace("@StrokeLineJoin", parameters.StrokeLineJoin)
                           .Replace("@TranformMatrix", parameters.TranformMatrix);
        }
    }


    internal struct ExportParameters
    {

        /// <summary>
        /// Provides a default set of Export parameters. You should edit this to your needs. 
        /// </summary>
        /// <param name="icon"></param>
        internal ExportParameters(IIconViewModel icon)
        {
            var metaData = Attribute.GetCustomAttribute(icon.IconPackType, typeof(MetaDataAttribute)) as MetaDataAttribute;

            this.IconKind = icon.Name;
            this.IconPackName = icon.IconPackType.Name;
            this.PageWidth = Settings.Default.IconPreviewSize.ToString(CultureInfo.InvariantCulture);
            this.PageHeight = Settings.Default.IconPreviewSize.ToString(CultureInfo.InvariantCulture);
            this.FillColor = Settings.Default.IconForeground.ToString(CultureInfo.InvariantCulture);
            this.Background = Settings.Default.IconBackground.ToString(CultureInfo.InvariantCulture);
            this.StrokeColor = Settings.Default.IconForeground.ToString(CultureInfo.InvariantCulture);
            this.StrokeWidth = "0";
            this.StrokeLineCap = PenLineCap.Round.ToString();
            this.StrokeLineJoin = PenLineJoin.Round.ToString();
            this.PathData = null;
            this.TranformMatrix = Matrix.Identity.ToString(CultureInfo.InvariantCulture);


            this.IconPackHomepage = metaData?.ProjectUrl;
            this.IconPackLicense = metaData?.LicenseUrl;
        }

        internal string IconKind { get; set; }
        internal string IconPackName { get; set; }
        internal string IconPackHomepage { get; set; }
        internal string IconPackLicense { get; set; }
        internal string PageWidth { get; set; }
        internal string PageHeight { get; set; }
        internal string PathData { get; set; }
        internal string FillColor { get; set; }
        internal string Background { get; set; }
        internal string StrokeColor { get; set; }
        internal string StrokeWidth { get; set; }
        internal string StrokeLineCap { get; set; }
        internal string StrokeLineJoin { get; set; }
        internal string TranformMatrix { get; set; }
    }            
}
