using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(ImageSource))]
    public class MingCuteIconsImageExtension : BasePackIconImageExtension
    {
        public MingCuteIconsImageExtension()
        {
        }

        public MingCuteIconsImageExtension(PackIconMingCuteIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
        public PackIconMingCuteIconsKind Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(this.Kind, this.Brush ?? Brushes.Black);
        }

        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMingCuteIconsKind kind)
            {
                PackIconDataFactory<PackIconMingCuteIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }

        /// <inheritdoc />
        protected override ScaleTransform GetScaleTransform(object iconKind)
        {
            return new ScaleTransform(1, -1);
        }
    }
}