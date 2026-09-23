using System;
using System.Collections.Generic;

namespace Dag3.Entities;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Excerpt { get; set; } = null!;

    public float Price { get; set; }

    public DateTime Date { get; set; }

    public int? AuthorId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<LoanedBook> LoanedBooks { get; set; } = new List<LoanedBook>();
}
