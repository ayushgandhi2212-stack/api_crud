using Microsoft.AspNetCore.Mvc;
using Mvc_client.Models;

namespace Mvc_client.Controllers
{
	public class FruitsController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiUrl;

		public FruitsController(HttpClient httpClient, IConfiguration config)
		{
			// you can send GET requests to fetch data or POST/PUT requests to insert or update data to an API.
			_httpClient = httpClient;

			// It builds the full URL of the API endpoint by combining the base URL from configuration
			// with the controller route.
			_apiUrl = config["ApiBaseUrl"] + "/api/Fruits_api";
		}

		// GET: /Fruits
		public async Task<IActionResult> Index()
		{
			// HTTP GET request to the API at _apiUrl and automatically deserializes the JSON response
			// into a List<Fruits_api> object
			List<Fruits_api> fruits = await _httpClient.GetFromJsonAsync<List<Fruits_api>>(_apiUrl);

			// deserialization is “unpacking” data from a format like JSON (or XML) into a usable object in your program.
			return View(fruits);
		}
		//GET: /Fruits/Create
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(Fruits_api newFruit)
		{
			if (!ModelState.IsValid)
			{
				return View(newFruit);
			}

			await _httpClient.PostAsJsonAsync(_apiUrl, newFruit);

			return RedirectToAction(nameof(Index));
		}
		// GET: /Fruits/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			//GetFromJsonAsync sends an HTTP GET request to the API URL (/api/Fruits_api/{id}).
			Fruits_api fruit = await _httpClient.GetFromJsonAsync<Fruits_api>($"{_apiUrl}/{id}");
			if (fruit == null)
			{
				return NotFound();
			}
			return View(fruit);
		}
		// POST: /Fruits/Edit
		[HttpPost]
		public async Task<IActionResult> Edit(Fruits_api fruit)
		{
			if (!ModelState.IsValid)
			{
				return View(fruit);
			}
			//PutAsJsonAsync sends an HTTP PUT request to the API with updated data
			await _httpClient.PutAsJsonAsync(_apiUrl, fruit);

			return RedirectToAction(nameof(Index));
		}


		// GET: /Fruits/Delete/5  -> Show confirmation page
		public async Task<IActionResult> Delete(int id)
		{

			// Get the fruit by ID from API
			//This sends an HTTP GET request to your API endpoint for a specific id
			Fruits_api fruit = await _httpClient.GetFromJsonAsync<Fruits_api>($"{_apiUrl}/{id}");
			if (fruit == null)
			{
				return NotFound();
			}

			return View(fruit); // Show Delete Confirmation page
		}

		// POST: /Fruits/DeleteConfirmed
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			//This sends an HTTP DELETE request to your API URL
			// Call API DELETE
			await _httpClient.DeleteAsync($"{_apiUrl}/{id}");

			// Redirect to Index after deletion
			return RedirectToAction(nameof(Index));
		}

		// GET: /Fruits/Details/5
		public async Task<IActionResult> Details(int id)
		{
			// Call API to get fruit by ID
			Fruits_api fruit = await _httpClient.GetFromJsonAsync<Fruits_api>($"{_apiUrl}/{id}");

			if (fruit == null)
			{
				return NotFound();
			}

			return View(fruit);
		}

	}
}

