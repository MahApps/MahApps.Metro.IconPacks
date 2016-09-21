using System.Collections.ObjectModel;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<IconPackViewModel> IconPacks { get; set; } = new ObservableCollection<IconPackViewModel>(
            new[]
            {
                new IconPackViewModel("Material", typeof(PackIconMaterialKind)),
                new IconPackViewModel("Modern", typeof(PackIconModernKind)),
                new IconPackViewModel("FontAwesome", typeof(PackIconFontAwesomeKind)),
                new IconPackViewModel("Entypo+", typeof(PackIconEntypoKind))
            });
    }
}