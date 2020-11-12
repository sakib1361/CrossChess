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
            var assembly = typeof(ImageResourceExtension).GetTypeInfo().Assembly;
            var path = "CrossChess.Resources.Images." + Source;
            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(path, assembly);

            return imageSource;
        }
    }
}
