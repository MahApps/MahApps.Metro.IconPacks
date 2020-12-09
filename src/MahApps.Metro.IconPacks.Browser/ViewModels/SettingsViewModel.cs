using ControlzEx.Theming;
using MahApps.Metro.IconPacks.Browser.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Windows;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public static class SettingsViewModel
    {

        static SettingsViewModel()
        {
            AccentColorNamesDictionary = new Dictionary<Color?, string>();

            var rm = new ResourceManager(typeof(AccentColorNames));
            var resourceSet = rm.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

            if (resourceSet != null)
            {
                foreach (var entry in resourceSet.OfType<DictionaryEntry>())
                {
                    try
                    {
                        if (ColorConverter.ConvertFromString(entry.Key.ToString()) is Color color)
                        {
                            AccentColorNamesDictionary.Add(color, entry.Value.ToString());
                        }
                    }
                    catch (Exception)
                    {
                        Trace.TraceError($"{entry.Key} is not a valid color key!");
                    }
                }
            }

            AccentColors = new List<Color?>(AccentColorNamesDictionary.Keys);
        }

        public static Dictionary<Color?, string> AccentColorNamesDictionary { get; } = new Dictionary<Color?, string>();


        public static List<Color?> AccentColors { get; }


        public static void SetTheme()
        {
            Theme newTheme = new Theme("AppTheme", "AppTheme", Settings.Default.AppTheme, Settings.Default.AppAccentColor.ToString(), Settings.Default.AppAccentColor, new SolidColorBrush(Settings.Default.AppAccentColor), true, false);
            ThemeManager.Current.ChangeTheme(App.Current, newTheme);
        }

        public static SimpleCommand SelectTemplateFolderCommand { get; } = new SimpleCommand((_) => SelectTemplateFolderCommand_Executed());
        private static void SelectTemplateFolderCommand_Executed()
        {
            var dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                Multiselect = false,
                Title = "Select the Template Folder"
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Settings.Default.ExportTemplatesDir = dialog.FileName;
            }
        }

        public static SimpleCommand CopyOriginalTemplatesCommand { get; } = new SimpleCommand((_) => CopyOriginalTemplates_Executed(), (_) => Directory.Exists(Settings.Default.ExportTemplatesDir));
        private static void CopyOriginalTemplates_Executed()
        {
            string[] failedItems = Array.Empty<string>();
            string[] orignalTemplates = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExportTemplates"));
            
            foreach (var template in orignalTemplates)
            {
                //Do your job with "file"  
                string destination = Path.Combine(Settings.Default.ExportTemplatesDir, Path.GetFileName(template));
                if (!File.Exists(destination))
                {
                    File.Copy(template, destination);
                }
                else
                {
                    // TODO failedItems.Append(Path.GetFileName(template));
                }
            }
        }

    }
}
