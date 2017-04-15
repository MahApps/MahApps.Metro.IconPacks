using System;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using ControlzEx;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconBase))]
    public class PackIconExtension : MarkupExtension
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
                return this.GetPackIcon<PackIconEntypo, PackIconEntypoKind>((PackIconEntypoKind)this.Kind);
            }
#endif
#if ALL || FONTAWESOME
            if (this.Kind is PackIconFontAwesomeKind)
            {
                return this.GetPackIcon<PackIconFontAwesome, PackIconFontAwesomeKind>((PackIconFontAwesomeKind)this.Kind);
            }
#endif
#if ALL || MATERIAL
            if (this.Kind is PackIconMaterialKind)
            {
                return this.GetPackIcon<PackIconMaterial, PackIconMaterialKind>((PackIconMaterialKind)this.Kind);
            }
#endif
#if ALL || MATERIALLIGHT
            if (this.Kind is PackIconMaterialLightKind)
            {
                return this.GetPackIcon<PackIconMaterialLight, PackIconMaterialLightKind>((PackIconMaterialLightKind)this.Kind);
            }
#endif
#if ALL || MODERN
            if (this.Kind is PackIconModernKind)
            {
                return this.GetPackIcon<PackIconModern, PackIconModernKind>((PackIconModernKind)this.Kind);
            }
#endif
#if ALL || OCTICONS
            if (this.Kind is PackIconOcticonsKind)
            {
                return this.GetPackIcon<PackIconOcticons, PackIconOcticonsKind>((PackIconOcticonsKind)this.Kind);
            }
#endif
#if ALL || SIMPLEICONS
            if (this.Kind is PackIconSimpleIconsKind)
            {
                return this.GetPackIcon<PackIconSimpleIcons, PackIconSimpleIconsKind>((PackIconSimpleIconsKind)this.Kind);
            }
#endif
            return null;
        }

        private PackIcon<TKind> GetPackIcon<TPack, TKind>(TKind kind) where TPack : PackIcon<TKind>, new()
        {
            var packIcon = new TPack {Kind = kind};
            if (this.Width != null)
                packIcon.Width = this.Width.Value;
            if (this.Height != null)
                packIcon.Height = this.Height.Value;
            if (this.Flip != null)
                packIcon.Flip = this.Flip.Value;
            if (this.Rotation != null)
                packIcon.Rotation = this.Rotation.Value;
            if (this.Spin != null)
                packIcon.Spin = this.Spin.Value;
            if (this.SpinAutoReverse != null)
                packIcon.SpinAutoReverse = this.SpinAutoReverse.Value;
            if (this.SpinEasingFunction != null)
                packIcon.SpinEasingFunction = this.SpinEasingFunction;
            if (this.SpinDuration != null)
                packIcon.SpinDuration = this.SpinDuration.Value;
            return packIcon;
        }
    }

    [MarkupExtensionReturnType(typeof(PackIconBase))]
    public class PackIconExtension<TPack, TKind> : MarkupExtension where TPack : PackIcon<TKind>, new()
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
            var packIcon = new TPack {Kind = this.Kind};

            if (this.Width != null)
                packIcon.Width = this.Width.Value;
            if (this.Height != null)
                packIcon.Height = this.Height.Value;
            if (this.Flip != null)
                packIcon.Flip = this.Flip.Value;
            if (this.Rotation != null)
                packIcon.Rotation = this.Rotation.Value;
            if (this.Spin != null)
                packIcon.Spin = this.Spin.Value;
            if (this.SpinAutoReverse != null)
                packIcon.SpinAutoReverse = this.SpinAutoReverse.Value;
            if (this.SpinEasingFunction != null)
                packIcon.SpinEasingFunction = this.SpinEasingFunction;
            if (this.SpinDuration != null)
                packIcon.SpinDuration = this.SpinDuration.Value;
            return packIcon;
        }
    }
}