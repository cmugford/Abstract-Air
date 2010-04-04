using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AbstractAir.Example.Web.Areas.Example.Models
{
    public class ProductCreateModel
    {
        [Required]
        public string Name { get; set; }
    }
}
