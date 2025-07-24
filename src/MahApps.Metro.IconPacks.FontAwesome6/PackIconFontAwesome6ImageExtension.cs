using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(ImageSource))]
    public class FontAwesome6ImageExtension : BasePackIconImageExtension
    {
        public FontAwesome6ImageExtension()
        {
        }

        public FontAwesome6ImageExtension(PackIconFontAwesome6Kind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
        public PackIconFontAwesome6Kind Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(this.Kind, this.Brush ?? Brushes.Black);
        }

        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconFontAwesome6Kind kind)
            {
                PackIconDataFactory<PackIconFontAwesome6Kind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}