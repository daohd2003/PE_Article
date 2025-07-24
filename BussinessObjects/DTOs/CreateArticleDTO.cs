using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects.DTOs
{
    public class CreateArticleDTO
    {
        public string Title { get; set; } = null!;

        public string? Ncontent { get; set; }

        public int CategoryId { get; set; }

        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
