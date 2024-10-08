using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlogPlatform.Data;
using BlogPlatform.Domain.Models;

namespace BlogPlatform.Pages.Blogs
{
    public class CreateModel : PageModel
    {
        private readonly BlogPlatform.Data.ApplicationDbContext _context;

        public CreateModel(BlogPlatform.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BlogPost BlogPost { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
          

            _context.BlogPosts.Add(BlogPost);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Blog created successfully!";
            return RedirectToPage("./Index");
        }
    }
}
