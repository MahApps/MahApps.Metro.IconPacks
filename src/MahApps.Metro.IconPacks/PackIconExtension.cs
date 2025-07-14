using System;
using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconBase))]
    public class PackIconExtension : BasePackIconExtension
    {
        public PackIconExtension()
        {
        }

        public PackIconExtension(Enum kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
        public Enum Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
#if ALL || BOOTSTRAPICONS
            if (this.Kind is PackIconBootstrapIconsKind)
            {
                return this.GetPackIcon<PackIconBootstrapIcons, PackIconBootstrapIconsKind>((PackIconBootstrapIconsKind) this.Kind);
            }
#endif
#if ALL || BOXICONS
            if (this.Kind is PackIconBoxIconsKind)
            {
                return this.GetPackIcon<PackIconBoxIcons, PackIconBoxIconsKind>((PackIconBoxIconsKind) this.Kind);
            }
#endif
#if ALL || CIRCUMICONS
            if (this.Kind is PackIconCircumIconsKind)
            {
                return this.GetPackIcon<PackIconCircumIcons, PackIconCircumIconsKind>((PackIconCircumIconsKind) this.Kind);
            }
#endif
#if ALL || CODICONS
            if (this.Kind is PackIconCodiconsKind)
            {
                return this.GetPackIcon<PackIconCodicons, PackIconCodiconsKind>((PackIconCodiconsKind) this.Kind);
            }
#endif
#if ALL || COOLICONS
            if (this.Kind is PackIconCooliconsKind)
            {
                return this.GetPackIcon<PackIconCoolicons, PackIconCooliconsKind>((PackIconCooliconsKind) this.Kind);
            }
#endif
#if ALL || ENTYPO
            if (this.Kind is PackIconEntypoKind)
            {
                return this.GetPackIcon<PackIconEntypo, PackIconEntypoKind>((PackIconEntypoKind) this.Kind);
            }
#endif
#if ALL || EVAICONS
            if (this.Kind is PackIconEvaIconsKind)
            {
                return this.GetPackIcon<PackIconEvaIcons, PackIconEvaIconsKind>((PackIconEvaIconsKind) this.Kind);
            }
#endif
#if ALL || FEATHERICONS
            if (this.Kind is PackIconFeatherIconsKind)
            {
                return this.GetPackIcon<PackIconFeatherIcons, PackIconFeatherIconsKind>((PackIconFeatherIconsKind) this.Kind);
            }
#endif
#if ALL || FILEICONS
            if (this.Kind is PackIconFileIconsKind)
            {
                return this.GetPackIcon<PackIconFileIcons, PackIconFileIconsKind>((PackIconFileIconsKind) this.Kind);
            }
#endif
#if ALL || FONTAUDIO
            if (this.Kind is PackIconFontaudioKind)
            {
                return this.GetPackIcon<PackIconFontaudio, PackIconFontaudioKind>((PackIconFontaudioKind) this.Kind);
            }
#endif
#if ALL || FONTAWESOME
            if (this.Kind is PackIconFontAwesomeKind)
            {
                return this.GetPackIcon<PackIconFontAwesome, PackIconFontAwesomeKind>((PackIconFontAwesomeKind) this.Kind);
            }
#endif
#if ALL || FONTISTO
            if (this.Kind is PackIconFontistoKind)
            {
                return this.GetPackIcon<PackIconFontisto, PackIconFontistoKind>((PackIconFontistoKind) this.Kind);
            }
#endif
#if ALL || FORKAWESOME
            if (this.Kind is PackIconForkAwesomeKind)
            {
                return this.GetPackIcon<PackIconForkAwesome, PackIconForkAwesomeKind>((PackIconForkAwesomeKind) this.Kind);
            }
#endif
#if ALL || GAMEICONS
            if (this.Kind is PackIconGameIconsKind)
            {
                return this.GetPackIcon<PackIconGameIcons, PackIconGameIconsKind>((PackIconGameIconsKind) this.Kind);
            }
#endif
#if ALL || IONICONS
            if (this.Kind is PackIconIoniconsKind)
            {
                return this.GetPackIcon<PackIconIonicons, PackIconIoniconsKind>((PackIconIoniconsKind) this.Kind);
            }
#endif
#if ALL || JAMICONS
            if (this.Kind is PackIconJamIconsKind)
            {
                return this.GetPackIcon<PackIconJamIcons, PackIconJamIconsKind>((PackIconJamIconsKind) this.Kind);
            }
#endif
#if ALL || LUCIDE
            if (this.Kind is PackIconLucideKind)
            {
                return this.GetPackIcon<PackIconLucide, PackIconLucideKind>((PackIconLucideKind) this.Kind);
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
#if ALL || MATERIALDESIGN
            if (this.Kind is PackIconMaterialDesignKind)
            {
                return this.GetPackIcon<PackIconMaterialDesign, PackIconMaterialDesignKind>((PackIconMaterialDesignKind) this.Kind);
            }
#endif
#if ALL || MEMORYICONS
            if (this.Kind is PackIconMemoryIconsKind)
            {
                return this.GetPackIcon<PackIconMemoryIcons, PackIconMemoryIconsKind>((PackIconMemoryIconsKind) this.Kind);
            }
#endif
#if ALL || MICRONS
            if (this.Kind is PackIconMicronsKind)
            {
                return this.GetPackIcon<PackIconMicrons, PackIconMicronsKind>((PackIconMicronsKind) this.Kind);
            }
#endif
#if ALL || MINGCUTEICONS
            if (this.Kind is PackIconMingCuteIconsKind)
            {
                return this.GetPackIcon<PackIconMingCuteIcons, PackIconMingCuteIconsKind>((PackIconMingCuteIconsKind) this.Kind);
            }
#endif
#if ALL || MODERN
            if (this.Kind is PackIconModernKind)
            {
                return this.GetPackIcon<PackIconModern, PackIconModernKind>((PackIconModernKind) this.Kind);
            }
#endif
#if ALL || MYNAUIICONS
            if (this.Kind is PackIconMynaUIIconsKind)
            {
                return this.GetPackIcon<PackIconMynaUIIcons, PackIconMynaUIIconsKind>((PackIconMynaUIIconsKind) this.Kind);
            }
#endif
#if ALL || OCTICONS
            if (this.Kind is PackIconOcticonsKind)
            {
                return this.GetPackIcon<PackIconOcticons, PackIconOcticonsKind>((PackIconOcticonsKind) this.Kind);
            }
#endif
#if ALL || PHOSPHORICONS
            if (this.Kind is PackIconPhosphorIconsKind)
            {
                return this.GetPackIcon<PackIconPhosphorIcons, PackIconPhosphorIconsKind>((PackIconPhosphorIconsKind) this.Kind);
            }
#endif
#if ALL || PICOLICONS
            if (this.Kind is PackIconPicolIconsKind)
            {
                return this.GetPackIcon<PackIconPicolIcons, PackIconPicolIconsKind>((PackIconPicolIconsKind) this.Kind);
            }
#endif
#if ALL || PIXELARTICONS
            if (this.Kind is PackIconPixelartIconsKind)
            {
                return this.GetPackIcon<PackIconPixelartIcons, PackIconPixelartIconsKind>((PackIconPixelartIconsKind) this.Kind);
            }
#endif
#if ALL || RADIXICONS
            if (this.Kind is PackIconRadixIconsKind)
            {
                return this.GetPackIcon<PackIconRadixIcons, PackIconRadixIconsKind>((PackIconRadixIconsKind) this.Kind);
            }
#endif
#if ALL || REMIXICON
            if (this.Kind is PackIconRemixIconKind)
            {
                return this.GetPackIcon<PackIconRemixIcon, PackIconRemixIconKind>((PackIconRemixIconKind) this.Kind);
            }
#endif
#if ALL || RPGAWESOME
            if (this.Kind is PackIconRPGAwesomeKind)
            {
                return this.GetPackIcon<PackIconRPGAwesome, PackIconRPGAwesomeKind>((PackIconRPGAwesomeKind) this.Kind);
            }
#endif
#if ALL || SIMPLEICONS
            if (this.Kind is PackIconSimpleIconsKind)
            {
                return this.GetPackIcon<PackIconSimpleIcons, PackIconSimpleIconsKind>((PackIconSimpleIconsKind) this.Kind);
            }
#endif
#if ALL || TYPICONS
            if (this.Kind is PackIconTypiconsKind)
            {
                return this.GetPackIcon<PackIconTypicons, PackIconTypiconsKind>((PackIconTypiconsKind) this.Kind);
            }
#endif
#if ALL || UNICONS
            if (this.Kind is PackIconUniconsKind)
            {
                return this.GetPackIcon<PackIconUnicons, PackIconUniconsKind>((PackIconUniconsKind) this.Kind);
            }
#endif
#if ALL || VAADINICONS
            if (this.Kind is PackIconVaadinIconsKind)
            {
                return this.GetPackIcon<PackIconVaadinIcons, PackIconVaadinIconsKind>((PackIconVaadinIconsKind) this.Kind);
            }
#endif
#if ALL || WEATHERICONS
            if (this.Kind is PackIconWeatherIconsKind)
            {
                return this.GetPackIcon<PackIconWeatherIcons, PackIconWeatherIconsKind>((PackIconWeatherIconsKind) this.Kind);
            }
#endif
#if ALL || ZONDICONS
            if (this.Kind is PackIconZondiconsKind)
            {
                return this.GetPackIcon<PackIconZondicons, PackIconZondiconsKind>((PackIconZondiconsKind) this.Kind);
            }
#endif
            return null;
        }
    }
}