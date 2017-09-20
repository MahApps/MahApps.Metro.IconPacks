using System;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public interface IIconViewModel
    {
        string Name { get; set; }
        string Description { get; set; }
        Type IconPackType { get; set; }
        Type IconType { get; set; }
        object Value { get; set; }
        MainViewModel MainViewModel { get; set; }
    }
}
