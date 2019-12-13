using System;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Specifies a description for a property or event.
    /// CA1813: Avoid unsealed attributes
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public sealed class DescriptionAttribute
        : Attribute
    {
        /// <summary>
        /// Gets the description stored in this attribute.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the DescriptionAttribute class with a description.
        /// </summary>
        /// <param name="description">The description text.</param>
        public DescriptionAttribute(string description)
        {
            this.Description = description;
        }

    }
}