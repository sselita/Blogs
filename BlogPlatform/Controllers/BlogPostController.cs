
using BlogPlatform.Domain.Models;
using BlogPlatform.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;

namespace BlogPlatform.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }
        //public async Task<IActionResult> Search(string title, DateTime date)
        //{
        
            
        //    blogs.ToPagedList(1, 2);
         
        //    return View("/Pages/BlogPosts/Search.cshtml", blogs);
        //}
        public async Task<IActionResult> Index(int? page, string title, DateTime date)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            if(title is null)
            {
                var bloggs = await _blogService.GetAllBlogPostsAsync();
                var blogPosts = bloggs.OrderByDescending(b => b.CreatedAt).ToPagedList(pageNumber, pageSize);
                return View("/Pages/BlogPosts/Index.cshtml", blogPosts);
            }

            var blogs = await _blogService.SearchBlogPostsAsync(title, date);
            blogs.ToPagedList(pageNumber, 1);
            return View("/Pages/BlogPosts/Search.cshtml",blogs);
        }
      
        public IActionResult Create()
        {
            return View("/Pages/BlogPosts/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogPost blogPost)
        {
            try
            {             
                if (ModelState.IsValid)
                {
                    blogPost.CreatedAt = DateTime.Now;
                    await _blogService.AddBlogPostAsync(blogPost);
                    TempData["SuccessMessage"] = "Blog created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                return View("/Pages/BlogPosts/Index.cshtml", blogPost);
            }
            catch (Exception ex)
            {
                // Log the exception
                TempData["ErrorMessage"] = "An error occurred while creating the blog."+ex;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _blogService.GetBlogPostByIdAsync(id);
            if (blog == null)
            {
                TempData["ErrorMessage"] = "The blog does not exist. ";
                return RedirectToAction(nameof(Index));
            }
            return View("/Pages/BlogPosts/Edit.cshtml", blog);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogPost blogPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _blogService.UpdateBlogPostAsync(blogPost);
                    TempData["SuccessMessage"] = "Blog updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = "Model is not valid , check the data.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception
                TempData["ErrorMessage"] = "An error occurred while updating the blog.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogService.GetBlogPostByIdAsync(id);
                if (blog == null)
                {
                    TempData["ErrorMessage"] = "The blog does not exist. ";
                    return RedirectToAction(nameof(Index));
                }
            return View("/Pages/BlogPosts/Delete.cshtml",blog);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _blogService.DeleteBlogPostAsync(id);
            TempData["SuccessMessage"] = "Blog deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
