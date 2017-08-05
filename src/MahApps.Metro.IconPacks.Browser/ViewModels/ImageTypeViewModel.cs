using System;
using System.Windows.Media.Imaging;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public class ImageTypeViewModel
    {
        public ImageType Type { get; set; }
        public Func<BitmapEncoder> EncoderFactory { get; set; }
        public string DisplayName { get; set; }
    }
}
