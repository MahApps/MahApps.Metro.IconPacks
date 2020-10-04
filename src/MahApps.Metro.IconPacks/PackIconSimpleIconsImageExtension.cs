using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(ImageSource))]
    public class SimpleIconsImageExtension : BasePackIconImageExtension
    {
        public SimpleIconsImageExtension()
        {
        }

        public SimpleIconsImageExtension(PackIconSimpleIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
        public PackIconSimpleIconsKind Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(this.Kind, this.Brush ?? Brushes.Black);
        }

        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconSimpleIconsKind kind)
            {
                PackIconSimpleIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}