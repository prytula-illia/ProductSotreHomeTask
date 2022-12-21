using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ProductSotre.Models
{
    public class BreadcrumbModel
    {
        public List<BreadcrumbLink> BreadcrumbLinks { get; set; } = new();

        public BreadcrumbModel(HttpContext context)
        {
            var host = context.Request.Host.ToString();
            var url = context.Request.Path.ToString();
            var allLinks = url.Split('/').Where(x => x != string.Empty);

            var firstLink = allLinks.First();

            BreadcrumbLinks.Add(new BreadcrumbLink
            {
                Url = $"https://{host}/{firstLink}",
                Index = 1,
                Title = firstLink
            });

            var index = 0;
            foreach(var link in allLinks.Skip(1))
            {
                index++;
                var lastLink = BreadcrumbLinks.Last();

                var breadcrumb = new BreadcrumbLink
                {
                    Url = $"{lastLink.Url}/{link}",
                    Index = index,
                    Title = link
                };

                if(IsCreateOrEdit(link))
                {
                    breadcrumb.Url = "#";
                    breadcrumb.Title = "New | Edit";
                }

                BreadcrumbLinks.Add(breadcrumb);
            }

            BreadcrumbLinks.Add(new BreadcrumbLink
            {
                Url = $"https://{host}/Home",
                Index = 0,
                Title = "Home"
            });
        }

        private bool IsCreateOrEdit(string link)
        {
            var linkToLower = link.ToLower();
            return linkToLower.Contains("create") || linkToLower.Contains("edit");
        }
    }
}