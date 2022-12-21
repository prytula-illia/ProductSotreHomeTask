using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace ProductSotre.Models.Helpers
{
    [HtmlTargetElement("northwind-link")]
    public class NorthwindImageLinkTagHelper : TagHelper
    {
        public string NorthwindId { get; set; }

        public string LinkText { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"/images/{NorthwindId}");
            output.Content.SetContent(LinkText);
        }
    }
}