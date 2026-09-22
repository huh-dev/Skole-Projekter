using System;
using System.Collections.Generic;

namespace Dag2.Entities;

public partial class Staff
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<LoanedBook> LoanedBooks { get; set; } = new List<LoanedBook>();
}
