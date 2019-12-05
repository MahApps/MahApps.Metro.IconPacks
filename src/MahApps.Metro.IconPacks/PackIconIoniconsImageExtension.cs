using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(ImageSource))]
    public class IoniconsImageExtension : BasePackIconImageExtension
    {
        public IoniconsImageExtension()
        {
        }

        public IoniconsImageExtension(PackIconIoniconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
        public PackIconIoniconsKind Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(this.Kind, this.Brush ?? Brushes.Black);
        }

        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconIoniconsKind ioniconsKind)
            {
                PackIconIoniconsDataFactory.DataIndex.Value?.TryGetValue(ioniconsKind, out data);
            }
            return data;
        }
    }
}