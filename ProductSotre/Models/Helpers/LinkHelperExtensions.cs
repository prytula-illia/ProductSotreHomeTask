using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.Mime.MediaTypeNames;

namespace ProductSotre.Models.Helpers
{
    public static class LinkHelperExtensions
    {
        public static IHtmlContent NorthwindImageLink(this IHtmlHelper helper, int imageId, string linkText)
        {
            var str = String.Format("<a href=\"/images/{0}\">{1}</a>", imageId, linkText);
            return new HtmlString(str);
        }
    }
}