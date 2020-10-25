using System;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Specifies meta data for a class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class MetaDataAttribute : Attribute
    {
        public MetaDataAttribute()
        {
        }

        public MetaDataAttribute(string name, string projectUrl, string licenseUrl)
        {
            Name = name;
            ProjectUrl = projectUrl;
            LicenseUrl = licenseUrl;
        }

        public string Name { get; }

        public string ProjectUrl { get; }

        public string LicenseUrl { get; }
    }
}