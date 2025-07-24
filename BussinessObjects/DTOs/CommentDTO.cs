using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects.DTOs
{
    public class CommentDTO
    {
        public int ArticleId { get; set; }

        public int UserId { get; set; }

        public string Ncontent { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
