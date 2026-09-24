using System;
using System.Collections.Generic;

namespace dag4.Entities;

public partial class LoanedBook
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public int StaffId { get; set; }

    public DateTime LoanStart { get; set; }

    public DateTime LoanEnd { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
