# MahApps.Metro.IconPacks

Some awesome icons all together...

The IconPacks library contains controls to use awesome icons from

- [Material](https://materialdesignicons.com/) (v1.7.22)
- [Font Awesome](http://fontawesome.io/icons/) (v4.6)
- [GitHub Octicons](https://octicons.github.com/) (v4.3.0)
- [Modern](http://modernuiicons.com/)
- [Entypo+](http://www.entypo.com/)

in a simple way.

It's not necessary to install [MahApps.Metro](https://github.com/MahApps/MahApps.Metro), but it makes your application nicer.

![iconbrowserfinal](https://cloud.githubusercontent.com/assets/658431/18764958/ec20dd3e-8113-11e6-8793-b012eaec2302.gif)

## Want to say thanks?

+ This framework is free and can be used anywhere, so please hit the :star: Star :star: button, cause this is the only payment (Cash donations are also being accepted ;-P ).

## Install

To install the IconPacks, run the following commands in the NuGet Package Manager Console or install it with the NuGet Package Manager extension.

- `MahApps.Metro.IconPacks` includes all Icons in one package [NuGet package](https://www.nuget.org/packages/MahApps.Metro.IconPacks/)
```bash
  PM> Install-Package MahApps.Metro.IconPacks
```
- `MahApps.Metro.IconPacks.Material` separate [NuGet package](https://www.nuget.org/packages/MahApps.Metro.IconPacks.Material/)
```bash
  PM> Install-Package MahApps.Metro.IconPacks.Material
```
- `MahApps.Metro.IconPacks.FontAwesome` separate [NuGet package](https://www.nuget.org/packages/MahApps.Metro.IconPacks.FontAwesome/)
```bash
  PM> Install-Package MahApps.Metro.IconPacks.FontAwesome
```
- `MahApps.Metro.IconPacks.Octicons` separate [NuGet package](https://www.nuget.org/packages/MahApps.Metro.IconPacks.Octicons/)
```bash
  PM> Install-Package MahApps.Metro.IconPacks.Octicons
```
- `MahApps.Metro.IconPacks.Modern` separate [NuGet package](https://www.nuget.org/packages/MahApps.Metro.IconPacks.Modern/)
```bash
  PM> Install-Package MahApps.Metro.IconPacks.Modern
```
- `MahApps.Metro.IconPacks.Entypo` separate [NuGet package](https://www.nuget.org/packages/MahApps.Metro.IconPacks.Entypo/)
```bash
  PM> Install-Package MahApps.Metro.IconPacks.Entypo
```

## Usage

If you want all icons together then just install the `MahApps.Metro.IconPacks` NuGet package. You can also only install one of the icon packs if you don't need them all.

```xaml
<iconPacks:PackIconMaterial Kind="EmoticonCool" />
```

```xaml
<iconPacks:PackIconFontAwesome Kind="FontAwesome" />
```

```xaml
<iconPacks:PackIconOcticons Kind="MarkGithub" />
```

```xaml
<iconPacks:PackIconModern Kind="ThumbsUp" />
```

```xaml
<iconPacks:PackIconEntypo Kind="EmojiHappy" />
```

The Xaml namespace for all icon packs is:

```xaml
xmlns:iconPacks="http://metro.mahapps.com/winfx/xaml/iconpacks"
```

![iconpacks001](https://cloud.githubusercontent.com/assets/658431/19322565/d6ec652c-90b9-11e6-8a06-00e326ca390a.gif)

## Sample

```xaml
<Window x:Class="IconPacksTest.App"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:iconPacks="http://metro.mahapps.com/winfx/xaml/iconpacks"
        Title="IconPacks" Height="300" Width="300">

   <Grid>
     <iconPacks:PackIconMaterial Kind="EmoticonCool" VerticalAlignment="Center" HorizontalAlignment="Center" />
   </Grid>

</Window>
```

## Properties

| Property | Description |
| --- | --- |
| `Kind` | Gets or sets the icon to display. Each IconPack has it's own icon type and an enumeration for it. |
| `Flip` | Gets or sets the flip orientation. Possible values are `Normal`, `Horizontal`, `Vertical` or `Both` from the enumeration type `PackIconFlipOrientation`. |
| `Rotation` | Gets or sets the rotation (angle) of the inner icon. Possible values are `0`-`360`. |
| `Spin` | Gets or sets a value indicating whether the inner icon is spinning (rotating) (`true` or `false`). |
| `SpinDuration` | Gets or sets the duration of the spinning animation (in seconds). This will also restart the spin animation and works only if `Spin` property is set to `true`. |
| `SpinEasingFunction` | Gets or sets the EasingFunction (`IEasingFunction`) of the spinning animation. This will also restart the spin animation and works only if `Spin` property is set to `true`. |
| `SpinAutoReverse` | Gets or sets the AutoReverse of the spinning animation. This will also restart the spin animation and works only if `Spin` property is set to `true`. |
| `Control.Properties` | All public properties of `Control`, e.g. `Width` and `Height` |

## Custom Styles

Sometimes it's necessary to change some properties for all used icon pack controls. All controls have styles which can be use for global changes or anything else.

For the `MahApps.Metro.IconPacks` you can e.g. create a custom resource dictionary (called here `CustomIconPacksStyles.xaml`) and add it to the `App.xaml` resource tag.

```xaml
<Application x:Class="IconPacksTest.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <!-- some other resources -->
                
                <!-- your custom icon resource -->
                <ResourceDictionary Source="pack://application:,,,/IconPacksTest;component/Resources/CustomIconPacksStyles.xaml" />

            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

Here is the resource dictionary content for all IconPacks in this sample (for `CustomIconPacksStyles.xaml`).

```xaml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:iconPacks="http://metro.mahapps.com/winfx/xaml/iconpacks">

    <ResourceDictionary.MergedDictionaries>
        <!-- reference all necessary original resource dictionaries -->
        <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks;component/Themes/PackIconMaterial.xaml" />
        <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks;component/Themes/PackIconFontAwesome.xaml" />
        <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks;component/Themes/PackIconOcticons.xaml" />
        <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks;component/Themes/PackIconModern.xaml" />
        <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks;component/Themes/PackIconEntypo.xaml" />
    </ResourceDictionary.MergedDictionaries>

    <!-- now make some global changes for the icon pack controls without using new keys -->

    <Style TargetType="{x:Type iconPacks:PackIconMaterial}" BasedOn="{StaticResource MahApps.Metro.Styles.PackIconMaterial}">
        <Setter Property="Width" Value="32" />
        <Setter Property="Height" Value="32" />
    </Style>

    <Style TargetType="{x:Type iconPacks:PackIconFontAwesome}" BasedOn="{StaticResource MahApps.Metro.Styles.PackIconFontAwesome}">
        <Setter Property="Width" Value="24" />
        <Setter Property="Height" Value="24" />
    </Style>

    <Style TargetType="{x:Type iconPacks:PackIconOcticons}" BasedOn="{StaticResource MahApps.Metro.Styles.PackIconOcticons}">
        <Setter Property="Width" Value="48" />
        <Setter Property="Height" Value="48" />
    </Style>

    <Style TargetType="{x:Type iconPacks:PackIconModern}" BasedOn="{StaticResource MahApps.Metro.Styles.PackIconModern}">
        <Setter Property="HorizontalAlignment" Value="Center" />
    </Style>

    <Style TargetType="{x:Type iconPacks:PackIconEntypo}" BasedOn="{StaticResource MahApps.Metro.Styles.PackIconEntypo}">
        <Setter Property="VerticalAlignment" Value="Center" />
        <Setter Property="HorizontalAlignment" Value="Center" />
    </Style>

</ResourceDictionary>
``` 
If you use the IconPack with all included icons you can also use this resource dictionary:

```xaml
<ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks;component/Themes/IconPacks.xaml" />
```

## Strong naming

If you need strong named dlls then you should use the [Strong Namer](https://github.com/dsplaisted/strongnamer) from @dsplaisted.
