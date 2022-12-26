using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Storage.Models.ViewModels;
using System.Text;
using System.Text.Encodings.Web;

namespace Storage.TagHelpers
{
    [HtmlTargetElement("star")]
    public class StarTagHelper : TagHelper
    {

        public float Rating { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.AddClass("star", HtmlEncoder.Default);

            var doubleRating = (int)Math.Round(Rating * 2);

            var stars = doubleRating / 2;
            var isHalfStar = doubleRating % 2 == 1;

            var commons = "https://upload.wikimedia.org/wikipedia/commons/";
            var star = commons + "e/e5/Full_Star_Yellow.svg";
            var half = commons + "d/d6/Half_Star_Yellow.svg";

            var builder = new StringBuilder();

            for (int i = 0; i < stars; i++)
            {
                builder.Append($"<img src='{star}' />");
            }
            if (isHalfStar) builder.Append($"<img src='{half}' />");

            output.Content.SetHtmlContent( builder.ToString() );


        }
    }
}
