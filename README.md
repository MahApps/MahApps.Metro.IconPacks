# MahApps.Metro.IconPacks

Some awesome icons all together...

The IconPacks are a side project of [MahApps.Metro](https://github.com/MahApps/MahApps.Metro) and contains controls to use the [Material](https://materialdesignicons.com/) (v1.6.50) and [Modern](http://modernuiicons.com/) icons, the [Fontawesome](http://fontawesome.io/icons/) icons (v4.6) and the [Entypo+](http://www.entypo.com/) icons in a simple way. It's not necessary to install MahApps.Metro, but it would be nice ;-D

## Usage

If you want all icons together then just install the `MahApps.Metro.IconPacks` NuGet package. You can also only install one of the icon packs if you don't need them all.

```xaml
<iconPacks:PackIconMaterial Kind="EmoticonCool" />
```
![2016-07-26_02h28_22](https://cloud.githubusercontent.com/assets/658431/17122168/71270be8-52d9-11e6-89a2-d670bd72aabb.png)

```xaml
<iconPacks:PackIconModern Kind="ThumbsUp" />
```
![2016-07-26_02h28_37](https://cloud.githubusercontent.com/assets/658431/17122171/729eb156-52d9-11e6-8baf-12345ddb9262.png)

```xaml
<iconPacks:PackIconFontAwesome Kind="FontAwesome" />
```
![2016-07-26_02h29_35](https://cloud.githubusercontent.com/assets/658431/17122172/73fe79f0-52d9-11e6-821e-204d1ee99fc3.png)

```xaml
<iconPacks:PackIconEntypo Kind="EmojiHappy" />
```
![2016-07-26_02h30_11](https://cloud.githubusercontent.com/assets/658431/17122173/7573d3ca-52d9-11e6-9a2e-8ecadad254e4.png)

The Xaml namespace for all icon packs is `xmlns:iconPacks="http://metro.mahapps.com/winfx/xaml/iconpacks"`.

## Styles

All controls have also styles, so you can use it for global changes or anything else.

For the `MahApps.Metro.IconPacks` you can e.g. create a custom resource dictionary and add it to the `App.xaml` resource tag.

```xaml
<Application x:Class="IconPacksTest.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <!-- other resources -->
                
                <!-- custom app resources -->
                <ResourceDictionary Source="pack://application:,,,/IconPacksTest;component/Resources/CustomIconPacksStyles.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

Here is the resource dictionary content.

```xaml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:iconPacks="http://metro.mahapps.com/winfx/xaml/iconpacks">

    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks;component/Themes/PackIconEntypo.xaml" />
        <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks;component/Themes/PackIconFontAwesome.xaml" />
        <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks;component/Themes/PackIconMaterial.xaml" />
        <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks;component/Themes/PackIconModern.xaml" />
    </ResourceDictionary.MergedDictionaries>

    <Style TargetType="{x:Type iconPacks:PackIconMaterial}" BasedOn="{StaticResource MahApps.Metro.Styles.PackIconMaterial}">
        <Setter Property="Width" Value="32" />
        <Setter Property="Height" Value="32" />
    </Style>
    <Style TargetType="{x:Type iconPacks:PackIconModern}" BasedOn="{StaticResource MahApps.Metro.Styles.PackIconModern}">
        <Setter Property="HorizontalAlignment" Value="Center" />
    </Style>
    <Style TargetType="{x:Type iconPacks:PackIconFontAwesome}" BasedOn="{StaticResource MahApps.Metro.Styles.PackIconFontAwesome}">
        <Setter Property="Width" Value="24" />
        <Setter Property="Height" Value="24" />
    </Style>
    <Style TargetType="{x:Type iconPacks:PackIconEntypo}" BasedOn="{StaticResource MahApps.Metro.Styles.PackIconEntypo}">
        <Setter Property="VerticalAlignment" Value="Center" />
        <Setter Property="HorizontalAlignment" Value="Center" />
    </Style>

</ResourceDictionary>
``` 

Resource dictionaries for the single NuGet packages are

- for `MahApps.Metro.IconPacks.Entypo`  
```xaml
  <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks.Entypo;component/Themes/PackIconEntypo.xaml" />
```
- for `MahApps.Metro.FontAwesome`  
```xaml
  <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks.FontAwesome;component/Themes/PackIconFontAwesome.xaml" />
```
- for `MahApps.Metro.Material`  
```xaml
  <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks.Material;component/Themes/PackIconMaterial.xaml" />
```
- for `MahApps.Metro.Modern`  
```xaml
  <ResourceDictionary Source="pack://application:,,,/MahApps.Metro.IconPacks.Modern;component/Themes/PackIconModern.xaml" />
```

The styles are the same as in the `MahApps.Metro.IconPacks` package.

## NuGet

- `MahApps.Metro.IconPacks` [NuGet package](https://www.nuget.org/packages/MahApps.Metro.IconPacks/)
- `MahApps.Metro.IconPacks.Entypo` [NuGet package](https://www.nuget.org/packages/MahApps.Metro.IconPacks.Entypo/)
- `MahApps.Metro.FontAwesome` [NuGet package](https://www.nuget.org/packages/MahApps.Metro.IconPacks.FontAwesome/)
- `MahApps.Metro.Material` [NuGet package](https://www.nuget.org/packages/MahApps.Metro.IconPacks.Material/)
- `MahApps.Metro.Modern` [NuGet package](https://www.nuget.org/packages/MahApps.Metro.IconPacks.Modern/)

## Strong naming

I will not do this for this packages. If you need this then you should use the [Strong Namer](https://github.com/dsplaisted/strongnamer) from @dsplaisted.

![iconpacks](https://cloud.githubusercontent.com/assets/658431/16098473/6a88963a-3353-11e6-8b97-71c07700c17c.gif)
