using System;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using ControlzEx;

namespace MahApps.Metro.IconPacks
{
    public interface IPackIconExtension
    {
        double? Width { get; set; }
        double? Height { get; set; }
        PackIconFlipOrientation? Flip { get; set; }
        double? Rotation { get; set; }
        bool? Spin { get; set; }
        bool? SpinAutoReverse { get; set; }
        IEasingFunction SpinEasingFunction { get; set; }
        double? SpinDuration { get; set; }
    }

    public static class PackIconExtensionHelper
    {
        public static PackIconControl<TKind> GetPackIcon<TPack, TKind>(this IPackIconExtension packIconExtension, TKind kind) where TPack : PackIconControl<TKind>, new()
        {
            var packIcon = new TPack {Kind = kind};
            if (packIconExtension.Width != null)
                packIcon.Width = packIconExtension.Width.Value;
            if (packIconExtension.Height != null)
                packIcon.Height = packIconExtension.Height.Value;
            if (packIconExtension.Flip != null)
                packIcon.Flip = packIconExtension.Flip.Value;
            if (packIconExtension.Rotation != null)
                packIcon.Rotation = packIconExtension.Rotation.Value;
            if (packIconExtension.Spin != null)
                packIcon.Spin = packIconExtension.Spin.Value;
            if (packIconExtension.SpinAutoReverse != null)
                packIcon.SpinAutoReverse = packIconExtension.SpinAutoReverse.Value;
            if (packIconExtension.SpinEasingFunction != null)
                packIcon.SpinEasingFunction = packIconExtension.SpinEasingFunction;
            if (packIconExtension.SpinDuration != null)
                packIcon.SpinDuration = packIconExtension.SpinDuration.Value;
            return packIcon;
        }
    }

    [MarkupExtensionReturnType(typeof(PackIconBase))]
    public class PackIconExtension : MarkupExtension, IPackIconExtension
    {
        [ConstructorArgument("kind")]
        public Enum Kind { get; set; }

        public double? Width { get; set; }
        public double? Height { get; set; }
        public PackIconFlipOrientation? Flip { get; set; }
        public double? Rotation { get; set; }
        public bool? Spin { get; set; }
        public bool? SpinAutoReverse { get; set; }
        public IEasingFunction SpinEasingFunction { get; set; }
        public double? SpinDuration { get; set; }

        public PackIconExtension()
        {
        }

        public PackIconExtension(Enum kind)
        {
            this.Kind = kind;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
#if ALL || ENTYPO
            if (this.Kind is PackIconEntypoKind)
            {
                return this.GetPackIcon<PackIconEntypo, PackIconEntypoKind>((PackIconEntypoKind) this.Kind);
            }
#endif
#if ALL || FONTAWESOME
            if (this.Kind is PackIconFontAwesomeKind)
            {
                return this.GetPackIcon<PackIconFontAwesome, PackIconFontAwesomeKind>((PackIconFontAwesomeKind) this.Kind);
            }
#endif
#if ALL || MATERIAL
            if (this.Kind is PackIconMaterialKind)
            {
                return this.GetPackIcon<PackIconMaterial, PackIconMaterialKind>((PackIconMaterialKind) this.Kind);
            }
#endif
#if ALL || MATERIALLIGHT
            if (this.Kind is PackIconMaterialLightKind)
            {
                return this.GetPackIcon<PackIconMaterialLight, PackIconMaterialLightKind>((PackIconMaterialLightKind) this.Kind);
            }
#endif
#if ALL || MODERN
            if (this.Kind is PackIconModernKind)
            {
                return this.GetPackIcon<PackIconModern, PackIconModernKind>((PackIconModernKind) this.Kind);
            }
#endif
#if ALL || OCTICONS
            if (this.Kind is PackIconOcticonsKind)
            {
                return this.GetPackIcon<PackIconOcticons, PackIconOcticonsKind>((PackIconOcticonsKind) this.Kind);
            }
#endif
#if ALL || SIMPLEICONS
            if (this.Kind is PackIconSimpleIconsKind)
            {
                return this.GetPackIcon<PackIconSimpleIcons, PackIconSimpleIconsKind>((PackIconSimpleIconsKind) this.Kind);
            }
#endif
            return null;
        }
    }

    [MarkupExtensionReturnType(typeof(PackIconBase))]
    public class PackIconExtension<TPack, TKind> : MarkupExtension, IPackIconExtension where TPack : PackIconControl<TKind>, new()
    {
        [ConstructorArgument("kind")]
        public TKind Kind { get; set; }

        public double? Width { get; set; }
        public double? Height { get; set; }
        public PackIconFlipOrientation? Flip { get; set; }
        public double? Rotation { get; set; }
        public bool? Spin { get; set; }
        public bool? SpinAutoReverse { get; set; }
        public IEasingFunction SpinEasingFunction { get; set; }
        public double? SpinDuration { get; set; }

        public PackIconExtension()
        {
        }

        public PackIconExtension(TKind kind)
        {
            this.Kind = kind;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this.GetPackIcon<TPack, TKind>(this.Kind);
        }
    }
}