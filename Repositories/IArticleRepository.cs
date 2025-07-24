using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticles();
        Task<Article?> GetArticleById(int id);
        Task<bool> CreateArticle(Article article);
        Task<bool> UpdateArticle(Article article);
        Task<bool> DeleteArticle(int id);
        IQueryable<Article> GetAllArticlesAsQueryable();
    }
}
