using CrossChess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossChess.Services
{
    public class FontIconFont : IMarkupExtension
    {
        public FontAwesomeIconType IconType { get; set; }
        public double FontSize { get; set; } = 24;
        public Color Color { get; set; } = Color.White;


        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return GetSource(IconType, Color, FontSize);
        }

        internal static FontImageSource GetSource(FontAwesomeIconType iconType, Color color, double size = 14)
        {
            return new FontImageSource()
            {
                FontFamily = IconHelper.FontAwesomeResource,
                Glyph = IconHelper.FontAwesomeValues[iconType],
                Size = size,
                Color = color
            };
        }
    }
}
