using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(ImageSource))]
    public class BoxIcons2ImageExtension : BasePackIconImageExtension
    {
        public BoxIcons2ImageExtension()
        {
        }

        public BoxIcons2ImageExtension(PackIconBoxIcons2Kind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
        public PackIconBoxIcons2Kind Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(this.Kind, this.Brush ?? Brushes.Black);
        }

        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconBoxIcons2Kind kind)
            {
                PackIconDataFactory<PackIconBoxIcons2Kind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}