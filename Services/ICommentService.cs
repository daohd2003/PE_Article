using BussinessObjects;
using BussinessObjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICommentService
    {
        Task<bool> Comment(CommentDTO dto);
        Task<IEnumerable<CommentDTO>> GetCommentByArticleId(int articleId);
    }
}
