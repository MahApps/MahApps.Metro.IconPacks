using System;
using System.Collections.Generic;

namespace MahApps.Metro.IconPacks
{
    /// ******************************************
    /// This code is auto generated. Do not amend.
    /// ******************************************
    internal static class PackIconControlDataFactory
    {
        internal static IDictionary<Enum, string> Create()
        {
            var dictionary = new Dictionary<Enum, string>();
            foreach (var packIcon in PackIconEntypoDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconEvaIconsDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconFeatherIconsDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconFontAwesomeDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconIoniconsDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconJamIconsDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconMaterialDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconMaterialDesignDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconMaterialLightDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconModernDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconOcticonsDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconSimpleIconsDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconTypiconsDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconUniconsDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconWeatherIconsDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            foreach (var packIcon in PackIconZondiconsDataFactory.Create())
            {
                dictionary.Add(packIcon.Key, packIcon.Value);
            }

            return dictionary;
        }
    }
}