using BlogPlatform.Domain.Models;

namespace BlogPlatform.Infrastructure.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> GetByIdAsync(int id);
        Task<IEnumerable<BlogPost>> GetSearchAsync(string title, DateTime? date);
        Task AddAsync(BlogPost blogPost);
        Task UpdateAsync(BlogPost blogPost);
        Task DeleteAsync(int id);
    }
}