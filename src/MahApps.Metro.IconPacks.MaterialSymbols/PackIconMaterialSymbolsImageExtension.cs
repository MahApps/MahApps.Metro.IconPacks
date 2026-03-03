using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(ImageSource))]
    public class MaterialSymbolsImageExtension : BasePackIconImageExtension
    {
        public MaterialSymbolsImageExtension()
        {
        }

        public MaterialSymbolsImageExtension(PackIconMaterialSymbolsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
        public PackIconMaterialSymbolsKind Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(this.Kind, this.Brush ?? Brushes.Black);
        }

        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMaterialSymbolsKind kind)
            {
                PackIconDataFactory<PackIconMaterialSymbolsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}