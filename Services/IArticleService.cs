using BussinessObjects;
using BussinessObjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDTO>> GetArticlesForList();
        Task<ArticleDetailDTO?> GetArticleDetails(int id);
        Task<bool> CreateArticle(CreateArticleDTO dto);
        Task<bool> UpdateArticle(UpdateArticleDTO dto);
        Task<bool> DeleteArticle(int id);
        IQueryable<Article> GetArticlesAsQueryable();
    }
}
