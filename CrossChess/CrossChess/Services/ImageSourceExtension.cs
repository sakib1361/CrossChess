using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossChess.Services
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }


            // Do your translation lookup here, using whatever method you require


            return ImageHelper.GetImage(Source);
        }
    }

    internal static class ImageHelper
    {
        static readonly Assembly assembly = typeof(ImageResourceExtension).GetTypeInfo().Assembly;
        internal static ImageSource GetImage(string source)
        {
            var path = "CrossChess.Resources.Images." + source;
            var imageSource = ImageSource.FromResource(path, assembly);
            return imageSource;
        }
    }
}
