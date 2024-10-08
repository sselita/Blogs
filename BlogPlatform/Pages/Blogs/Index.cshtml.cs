using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

using BlogPlatform.Data;
using BlogPlatform.Domain.Models;

namespace BlogPlatroms.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IPagedList<BlogPost> BlogPost { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? SearchDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;  // Default to the first page

        public int PageSize { get; set; } = 6;  // Display 5 posts per page

        public async Task OnGetAsync()
        {
            IQueryable<BlogPost> postsQuery = _context.BlogPosts;

            // Apply title filter if provided
            if (!string.IsNullOrEmpty(SearchTitle))
            {
                postsQuery = postsQuery.Where(b => b.Title.Contains(SearchTitle));
            }

            // Apply date filter if provided
            if (SearchDate.HasValue)
            {
                postsQuery = postsQuery.Where(b => b.CreatedAt.Date == SearchDate.Value.Date);
            }

            // Fetch paginated data using PagedList
            BlogPost = postsQuery
                .OrderByDescending(b => b.CreatedAt)
                .ToPagedList(PageNumber, PageSize);
        }
    }
}