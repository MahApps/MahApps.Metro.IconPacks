﻿using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(ImageSource))]
    public class MicronsImageExtension : BasePackIconImageExtension
    {
        public MicronsImageExtension()
        {
        }

        public MicronsImageExtension(PackIconMicronsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
        public PackIconMicronsKind Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(this.Kind, this.Brush ?? Brushes.Black);
        }

        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMicronsKind kind)
            {
                PackIconDataFactory<PackIconMicronsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}