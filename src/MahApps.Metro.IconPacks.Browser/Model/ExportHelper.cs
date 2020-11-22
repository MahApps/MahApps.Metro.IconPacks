using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        internal static string FillTemplate(string template, ExportParameters parameters) 
        {
            return template.Replace("{{IconKind}}",parameters.IconKind)
                           .Replace("{{IconPackName}}",parameters.IconPackName)
                           .Replace("{{IconPackHomepage}}",parameters.IconPackHomepage)
                           .Replace("{{IconPackLicense}}",parameters.IconPackLicense)
                           .Replace("{{PageWidth}}", parameters.PageWidth)
                           .Replace("{{PageHeight}}", parameters.PageHeight)
                           .Replace("{{PathData}}", parameters.PathData)
                           .Replace("{{FillColor}}", parameters.FillColor)
                           .Replace("{{StrokeColor}}", parameters.StrokeColor)
                           .Replace("{{StrokeWidth}}", parameters.StrokeWidth)
                           .Replace("{{StrokeLineCap}}", parameters.StrokeLineCap)
                           .Replace("{{StrokeLineJoin}}", parameters.StrokeLineJoin)
                           .Replace("{{TranformMatrix}}", parameters.TranformMatrix);
        }
    }


    internal struct ExportParameters
    {
        internal string IconKind { get; set; }
        internal string IconPackName { get; set; }
        internal string IconPackHomepage { get; set; }
        internal string IconPackLicense { get; set; }
        internal string PageWidth { get; set; }
        internal string PageHeight { get; set; }
        internal string PathData { get; set; }
        internal string FillColor { get; set; }
        internal string StrokeColor { get; set; }
        internal string StrokeWidth { get; set; }
        internal string StrokeLineCap { get; set; }
        internal string StrokeLineJoin { get; set; }
        internal string TranformMatrix { get; set; }
    }            
}
