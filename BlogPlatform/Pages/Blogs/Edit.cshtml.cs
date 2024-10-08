using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogPlatform.Data;
using BlogPlatform.Domain.Models;

namespace BlogPlatform.Pages.Blogs
{
    public class EditModel : PageModel
    {
        private readonly BlogPlatform.Data.ApplicationDbContext _context;

        public EditModel(BlogPlatform.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BlogPost BlogPost { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogpost =  await _context.BlogPosts.FirstOrDefaultAsync(m => m.Id == id);
            if (blogpost == null)
            {
                return NotFound();
            }
            BlogPost = blogpost;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }

            _context.Attach(BlogPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Blog edited successfully!";
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(BlogPost.Id))
                {
                    return NotFound();
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurred while creating the blog.";
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }
    }
}
