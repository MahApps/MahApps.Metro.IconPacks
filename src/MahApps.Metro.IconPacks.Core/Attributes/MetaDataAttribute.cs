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

        public MetaDataAttribute(string projectUrl, string licenseUrl)
        {
            ProjectUrl = projectUrl;
            LicenseUrl = licenseUrl;
        }

        public string ProjectUrl { get; }

        public string LicenseUrl { get; }
    }
}