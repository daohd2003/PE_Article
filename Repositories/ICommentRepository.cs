using BussinessObjects;
using BussinessObjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICommentRepository
    {
        Task<bool> Comment(Comment entity);
        Task<IEnumerable<Comment>> GetCommentByArticleId(int articleId);
    }
}
