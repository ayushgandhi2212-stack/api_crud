using System.ComponentModel.DataAnnotations;

namespace api_crud.Model
{
	public class Fruits_api
	{
		[Key]
		public int Id { get; set; }
		public string fruit_Name { get; set; }
		public int Quantity { get; set; }
	}
}
