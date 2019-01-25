using System;
using System.Globalization;

namespace MahApps.Metro.IconPacks.Converter
{
    /// <summary>
    /// Converts any given PackIcon*Kind enum to its equivalent user control.
    /// </summary>
    public class IconKindToPackIconConverter : MarkupConverter
    {
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case PackIconEntypoKind kind:
                    return new PackIconEntypo() { Kind = kind };

                case PackIconFontAwesomeKind kind:
                    return new PackIconFontAwesome() { Kind = kind };

                case PackIconMaterialKind kind:
                    return new PackIconMaterial() { Kind = kind };

                case PackIconMaterialLightKind kind:
                    return new PackIconMaterialLight() { Kind = kind };

                case PackIconModernKind kind:
                    return new PackIconModern() { Kind = kind };

                case PackIconOcticonsKind kind:
                    return new PackIconOcticons() { Kind = kind };

                case PackIconSimpleIconsKind kind:
                    return new PackIconSimpleIcons() { Kind = kind };

                default:
                    return null;
            }
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}