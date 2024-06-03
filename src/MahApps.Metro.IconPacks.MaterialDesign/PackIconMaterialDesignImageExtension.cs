﻿using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(ImageSource))]
    public class MaterialDesignImageExtension : BasePackIconImageExtension
    {
        public MaterialDesignImageExtension()
        {
        }

        public MaterialDesignImageExtension(PackIconMaterialDesignKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
        public PackIconMaterialDesignKind Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(this.Kind, this.Brush ?? Brushes.Black);
        }

        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMaterialDesignKind kind)
            {
                PackIconDataFactory<PackIconMaterialDesignKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}