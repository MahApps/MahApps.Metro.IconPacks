using System;
using System.Collections.Generic;

namespace MahApps.Metro.IconPacks
{
    public class PackIconDataFactory : IPackIconDataFactory
    {
        private readonly IDictionary<PackIconMaterialKind, string> _packIconMaterialDataFactory;
        private readonly IDictionary<PackIconEntypoKind, string> _packIconEntypoDataFactory;
        private readonly IDictionary<PackIconFontAwesomeKind, string> _packIconFontAwesomeDataFactory;
        private readonly IDictionary<PackIconMaterialLightKind, string> _packIconMaterialLightDataFactory;
        private readonly IDictionary<PackIconModernKind, string> _packIconModernDataFactory;
        private readonly IDictionary<PackIconOcticonsKind, string> _packIconOcticonsDataFactory;
        private readonly IDictionary<PackIconSimpleIconsKind, string> _packIconSimpleIconsDataFactory;

        public PackIconDataFactory()
        {
            _packIconMaterialDataFactory = PackIconMaterialDataFactory.Create();
            _packIconEntypoDataFactory = PackIconEntypoDataFactory.Create();
            _packIconFontAwesomeDataFactory = PackIconFontAwesomeDataFactory.Create();
            _packIconMaterialLightDataFactory = PackIconMaterialLightDataFactory.Create();
            _packIconModernDataFactory = PackIconModernDataFactory.Create();
            _packIconOcticonsDataFactory = PackIconOcticonsDataFactory.Create();
            _packIconSimpleIconsDataFactory = PackIconSimpleIconsDataFactory.Create();
        }

        public bool GetData(Type enumType, string name, out string pathData)
        {
            if (enumType == typeof(PackIconMaterialKind))
            {
                if (Enum.TryParse(name, out PackIconMaterialKind kind))
                    return _packIconMaterialDataFactory.TryGetValue(kind, out pathData);
            }

            if (enumType == typeof(PackIconEntypoKind))
            {
                if (Enum.TryParse(name, out PackIconEntypoKind kind))
                    return _packIconEntypoDataFactory.TryGetValue(kind, out pathData);
            }

            if (enumType == typeof(PackIconFontAwesomeKind))
            {
                if (Enum.TryParse(name, out PackIconFontAwesomeKind kind))
                    return _packIconFontAwesomeDataFactory.TryGetValue(kind, out pathData);
            }

            if (enumType == typeof(PackIconMaterialLightKind))
            {
                if (Enum.TryParse(name, out PackIconMaterialLightKind kind))
                    return _packIconMaterialLightDataFactory.TryGetValue(kind, out pathData);
            }

            if (enumType == typeof(PackIconModernKind))
            {
                if (Enum.TryParse(name, out PackIconModernKind kind))
                    return _packIconModernDataFactory.TryGetValue(kind, out pathData);
            }

            if (enumType == typeof(PackIconOcticonsKind))
            {
                if (Enum.TryParse(name, out PackIconOcticonsKind kind))
                    return _packIconOcticonsDataFactory.TryGetValue(kind, out pathData);
            }

            if (enumType == typeof(PackIconSimpleIconsKind))
            {
                if (Enum.TryParse(name, out PackIconSimpleIconsKind kind))
                    return _packIconSimpleIconsDataFactory.TryGetValue(kind, out pathData);
            }

            pathData = string.Empty;
            return false;
        }
    }
}
