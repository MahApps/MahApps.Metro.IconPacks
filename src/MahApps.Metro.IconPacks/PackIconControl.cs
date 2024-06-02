using System;
using System.Windows;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// </summary>
    public class PackIconControl : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(Enum), typeof(PackIconControl), new PropertyMetadata(default(Enum), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconControl)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public Enum Kind
        {
            get { return (Enum)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        static PackIconControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconControl), new FrameworkPropertyMetadata(typeof(PackIconControl)));
        }

        public PackIconControl()
        {
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
            this.SetCurrentValue(KindProperty, iconKind);
        }

        protected override void UpdateData()
        {
            if (Kind != default(Enum))
            {
                string data = null;
                PackIconControlDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}