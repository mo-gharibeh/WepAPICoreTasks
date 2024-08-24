using System;
using System.Collections.Generic;

namespace WepAPICoreTask2.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryImage { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
