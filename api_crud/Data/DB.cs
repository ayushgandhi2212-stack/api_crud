using api_crud.Model;
using Microsoft.EntityFrameworkCore;

namespace api_crud.Data
{
	public class DB:DbContext
	{
		public DB() { }
		public DB(DbContextOptions<DB> options) : base(options)
		{
		}
		public DbSet<Fruits_api> fruits_Apis { get; set; }
	}	
}
