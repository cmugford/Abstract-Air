using System;
using System.ComponentModel.DataAnnotations;

namespace AbstractAir.Example.UI.Area.Models
{
	public class CreateProductModel
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Category { get; set; }
	}
}