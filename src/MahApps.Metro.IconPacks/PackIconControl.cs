using System;
using System.Collections.Generic;
using System.Linq;
#if NETFX_CORE || WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
#endif

namespace MahApps.Metro.IconPacks
{
    public class PackIconControl : ContentControl
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(
                nameof(Kind),
                typeof(Enum),
                typeof(PackIconControl),
                new PropertyMetadata(default(Enum), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue && e.NewValue is Enum kind)
            {
                var control = (PackIconControl)dependencyObject;
                control.SetContentByPackIconKind(kind);
            }
        }

        private void SetContentByPackIconKind(Enum packIconKind)
        {
            object newContent;
            switch (packIconKind)
            {
                case PackIconEntypoKind kind:
                    newContent = new PackIconEntypo { Kind = kind };
                    break;
                case PackIconFontAwesomeKind kind:
                    newContent = new PackIconFontAwesome { Kind = kind };
                    break;
                case PackIconMaterialKind kind:
                    newContent = new PackIconMaterial { Kind = kind };
                    break;
                case PackIconMaterialLightKind kind:
                    newContent = new PackIconMaterialLight { Kind = kind };
                    break;
                case PackIconModernKind kind:
                    newContent = new PackIconModern { Kind = kind };
                    break;
                case PackIconOcticonsKind kind:
                    newContent = new PackIconOcticons { Kind = kind };
                    break;
                case PackIconSimpleIconsKind kind:
                    newContent = new PackIconSimpleIcons { Kind = kind };
                    break;
                case PackIconWeatherIconsKind kind:
                    newContent = new PackIconWeatherIcons { Kind = kind };
                    break;
                case PackIconTypiconsKind kind:
                    newContent = new PackIconTypicons { Kind = kind };
                    break;
                case PackIconFeatherIconsKind kind:
                    newContent = new PackIconFeatherIcons { Kind = kind };
                    break;
                case PackIconMaterialDesignKind kind:
                    newContent = new PackIconMaterialDesign { Kind = kind };
                    break;
                case PackIconIoniconsKind kind:
                    newContent = new PackIconIonicons { Kind = kind };
                    break;
                case PackIconJamIconsKind kind:
                    newContent = new PackIconJamIcons { Kind = kind };
                    break;
                case PackIconUniconsKind kind:
                    newContent = new PackIconUnicons { Kind = kind };
                    break;
                case PackIconZondiconsKind kind:
                    newContent = new PackIconZondicons { Kind = kind };
                    break;
                default:
                    newContent = null;
                    break;
            }

            if (newContent != null)
            {
                this.SetValue(ContentProperty, newContent);
            }
        }

        public Enum Kind
        {
            get => (Enum)this.GetValue(KindProperty);
            set => this.SetValue(KindProperty, value);
        }
    }
}