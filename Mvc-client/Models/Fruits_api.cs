using System.ComponentModel.DataAnnotations;

namespace Mvc_client.Models
{
	public class Fruits_api
	{
			[Key]
			public int Id { get; set; }
			public string fruit_Name { get; set; }
			public int Quantity { get; set; }
	}
}
