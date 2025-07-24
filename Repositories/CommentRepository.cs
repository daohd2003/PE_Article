using BussinessObjects;
using BussinessObjects.DTOs;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly Sp25Prn231Pe2Context _context;

        public CommentRepository(Sp25Prn231Pe2Context context)
        {
            _context = context;
        }
        public async Task<bool> Comment(Comment entity)
        {
            try
            {
                await _context.Comments.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentByArticleId(int articleId)
        {
            try
            {
                return await _context.Comments.Where(a => a.ArticleId == articleId).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
