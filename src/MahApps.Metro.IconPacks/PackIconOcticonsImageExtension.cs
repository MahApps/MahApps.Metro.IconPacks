using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(ImageSource))]
    public class OcticonsImageExtension : BasePackIconImageExtension
    {
        public OcticonsImageExtension()
        {
        }

        public OcticonsImageExtension(PackIconOcticonsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
        public PackIconOcticonsKind Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(this.Kind, this.Brush ?? Brushes.Black);
        }

        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconOcticonsKind kind)
            {
                PackIconOcticonsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}