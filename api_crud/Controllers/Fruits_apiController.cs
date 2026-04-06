using api_crud.Data;
using api_crud.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_crud.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Fruits_apiController : ControllerBase
	{
		private readonly DB db;
		public Fruits_apiController(DB db)
		{
			this.db = db;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<Fruits_api> fruits_Apis = await db.fruits_Apis.ToListAsync();
			return Ok(fruits_Apis);
		}
		//public async Task<IActionResult> Create()
		//{
		//    await Task.CompletedTask; //this line is only written for consistency with other async actions
		//    return View(new Fruits_api());
		//}
		/*
		Why we DO NOT write this in an API
		APIs do NOT render UI
		An API:

Does NOT show forms

Does NOT return HTML

Does NOT need an empty model

An API only:

Receives data

Processes data

Returns JSON
		*/
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Fruits_api xData)
		{
			//[FromBody] -> It tells the API: "Take the data sent in the request body (usually JSON) and put it into this object."
			//Fruits_api is the type of the object (here -> type is the class or structure).

			// xData is the variable name / object that will hold the data sent in the request body.
			// -> An object is a real instance of that type.

			// It holds actual data in memory.)
			if (xData == null)
			{
				return BadRequest("Invalid data");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.fruits_Apis.Add(xData);   // use DbSet, not DbContext directly
			await db.SaveChangesAsync();

			return Ok(xData);
		}
		[HttpPut]
		public async Task<IActionResult> Edit([FromBody] Fruits_api obj)
		{
			try
			{
				db.fruits_Apis.Update(obj);
				await db.SaveChangesAsync();
				return Ok(obj);
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var fruit = await db.fruits_Apis.FindAsync(id);

			if (fruit == null)
			{
				return NotFound("Record not found");
			}

			db.fruits_Apis.Remove(fruit);
			await db.SaveChangesAsync();

			return Ok("Deleted Successfully.");
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Details(int id)
		{
			var fruit = await db.fruits_Apis.FindAsync(id);
			if (fruit == null)
			{
				return NotFound("Record not found");
			}
			return Ok(fruit);
		}
	}
}
