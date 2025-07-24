using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BussinessObjects;

public partial class Article
{
    [Key]
    public int ArticlesId { get; set; }

    public string Title { get; set; } = null!;

    public string? Ncontent { get; set; }

    public int CategoryId { get; set; }

    public int AuthorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
