using BlogPlatform.Domain.Models;
using BlogPlatform.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogPlatform.Pages.BlogPosts
{
    public class SearchModel : PageModel
    {

        [ViewData]
        public string Title { get; set; }

        public void OnGet()
        {
            Title = "Searfchhhh";
        }
    }
}
