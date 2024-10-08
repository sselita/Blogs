using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogPlatform.Pages.BlogPosts
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            return  RedirectToPage("/BlogPosts/Index");
        }
    }
}
