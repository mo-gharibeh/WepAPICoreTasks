using System;
using System.Collections.Generic;

namespace WepAPICoreTask2.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public virtual User? User { get; set; }
}
