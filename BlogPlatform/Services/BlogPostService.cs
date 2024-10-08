
using BlogPlatform.Domain.Models;
using BlogPlatform.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPlatform.Web.Services
{
    public class BlogService
    {
        private readonly IBlogPostRepository _blogRepository;

        public BlogService(IBlogPostRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _blogRepository.GetAllAsync();
        }
        public async Task<IEnumerable<BlogPost>> SearchBlogPostsAsync(string title , DateTime date)
        {
            return await _blogRepository.GetSearchAsync( title, date);
        }

        public async Task<BlogPost> GetBlogPostByIdAsync(int id)
        {
            return await _blogRepository.GetByIdAsync(id);
        }

        public async Task AddBlogPostAsync(BlogPost blogPost)
        {
            await _blogRepository.AddAsync(blogPost);
        }

        public async Task UpdateBlogPostAsync(BlogPost blogPost)
        {
            await _blogRepository.UpdateAsync(blogPost);
        }

        public async Task DeleteBlogPostAsync(int id)
        {
            await _blogRepository.DeleteAsync(id);
        }
    }
}
