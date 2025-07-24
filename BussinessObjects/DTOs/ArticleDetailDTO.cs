using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects.DTOs
{
    public class ArticleDetailDTO
    {
        public int ArticlesId { get; set; }

        public string Title { get; set; } = null!;

        public string? Ncontent { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedAt { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
