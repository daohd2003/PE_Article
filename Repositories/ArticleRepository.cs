using BussinessObjects;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly Sp25Prn231Pe2Context _context;

        public ArticleRepository(Sp25Prn231Pe2Context context) { _context = context; }

        public async Task<bool> CreateArticle(Article article)
        {
            try
            {
                await _context.Articles.AddAsync(article);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteArticle(int id)
        {
            try
            {
                var article = await _context.Articles.FindAsync(id);
                if (article == null) return false;
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting article: {ex.Message}");
                return false;
            }
        }

        public IQueryable<Article> GetAllArticlesAsQueryable()
        {
            return _context.Articles;
        }

        public async Task<Article?> GetArticleById(int id)
        {
            return await _context.Articles
                     .Include(c => c.Category)
                     .Include(c => c.Author)
                     .Include(c => c.Comments)
                     .FirstOrDefaultAsync(c => c.ArticlesId == id);
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            return await _context.Articles
                     .Include(c => c.Category)
                     .Include(c => c.Author)
                     .ToListAsync();
        }

        public async Task<bool> UpdateArticle(Article article)
        {
            try
            {
                _context.Entry<Article>(article).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating article: {ex.Message}");
                return false;
            }
        }
    }
}
