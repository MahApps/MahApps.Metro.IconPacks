using System;
using System.Collections.Generic;
#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#else
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endif

namespace MahApps.Metro.IconPacks
{
    public abstract class PackIconBase : Control
    {
        internal abstract void UpdateData();
    }

    /// <summary>
    /// Base class for creating an icon control for icon packs.
    /// </summary>
    /// <typeparam name="TKind"></typeparam>
    public abstract class PackIconBase<TKind> : PackIconBase
    {
        private static Lazy<IDictionary<TKind, string>> _dataIndex;

        /// <param name="dataIndexFactory">
        /// Inheritors should provide a factory for setting up the path data index (per icon kind).
        /// The factory will only be utilized once, across all closed instances (first instantiation wins).
        /// </param>
        protected PackIconBase(Func<IDictionary<TKind, string>> dataIndexFactory)
        {
            if (dataIndexFactory == null) throw new ArgumentNullException(nameof(dataIndexFactory));

            if (_dataIndex == null)
                _dataIndex = new Lazy<IDictionary<TKind, string>>(dataIndexFactory);
        }

        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(TKind), typeof(PackIconBase<TKind>), new PropertyMetadata(default(TKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((PackIconBase)dependencyObject).UpdateData();
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public TKind Kind
        {
            get { return (TKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if (NETFX_CORE || WINDOWS_UWP)
        private static readonly DependencyProperty DataProperty
            = DependencyProperty.Register(nameof(Data), typeof(string), typeof(PackIconBase<TKind>), new PropertyMetadata(""));

        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary>
        public string Data
        {
            get { return (string)GetValue(DataProperty); }
            private set { SetValue(DataProperty, value); }
        }
#else
        private static readonly DependencyPropertyKey DataPropertyKey
            = DependencyProperty.RegisterReadOnly(nameof(Data), typeof(string), typeof(PackIconBase<TKind>), new PropertyMetadata(""));

        // ReSharper disable once StaticMemberInGenericType
        public static readonly DependencyProperty DataProperty = DataPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary>
        [TypeConverter(typeof(GeometryConverter))]
        public string Data
        {
            get { return (string)GetValue(DataProperty); }
            private set { SetValue(DataPropertyKey, value); }
        }
#endif

#if (NETFX_CORE || WINDOWS_UWP)
        protected override void OnApplyTemplate()
#else
        public override void OnApplyTemplate()
#endif
        {
            base.OnApplyTemplate();

            UpdateData();
        }

        internal override void UpdateData()
        {
            string data = null;
            _dataIndex.Value?.TryGetValue(Kind, out data);
            this.Data = data;
        }
    }
}
