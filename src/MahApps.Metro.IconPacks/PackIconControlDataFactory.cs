using System;
using System.Collections.Generic;

namespace MahApps.Metro.IconPacks
{
    /// ******************************************
    /// This code is auto generated. Do not amend.
    /// ******************************************
    public static class PackIconControlDataFactory
    {
        public static Lazy<IDictionary<Enum, string>> DataIndex { get; }

        static PackIconControlDataFactory()
        {
            if (DataIndex == null)
            {
                DataIndex = new Lazy<IDictionary<Enum, string>>(PackIconControlDataFactory.Create);
            }
        }

        internal static IDictionary<Enum, string> Create()
        {
            var dictionary = new Dictionary<Enum, string>();
            foreach (var packIcon in PackIconDataFactory<PackIconBootstrapIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconBoxIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconCircumIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconCodiconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconCooliconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconEntypoKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconEvaIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconFeatherIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconFileIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconFontaudioKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconFontAwesomeKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconFontistoKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconForkAwesomeKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconGameIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconIoniconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconJamIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconLucideKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconMaterialKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconMaterialLightKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconMaterialDesignKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconMemoryIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconMicronsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconModernKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconOcticonsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconPhosphorIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconPicolIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconPixelartIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconRadixIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconRemixIconKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconRPGAwesomeKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconSimpleIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconTypiconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconUniconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconVaadinIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconWeatherIconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            foreach (var packIcon in PackIconDataFactory<PackIconZondiconsKind>.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }
            return dictionary;
        }
    }
}