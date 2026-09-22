using System;
using System.Collections.Generic;

namespace Dag2.Entities;

public partial class LoanLog
{
    public int Id { get; set; }

    public int LoanId { get; set; }

    public int BookId { get; set; }

    public int UserId { get; set; }

    public int StaffId { get; set; }

    public string Action { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
