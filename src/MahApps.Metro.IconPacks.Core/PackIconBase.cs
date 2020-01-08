#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Controls;
#else
using System.Windows.Controls;
#endif

namespace MahApps.Metro.IconPacks
{
    public abstract class PackIconBase : Control
    {
        protected internal abstract void SetKind<TKind>(TKind iconKind);
        protected abstract void UpdateData();
    }
}