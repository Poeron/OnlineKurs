using Microsoft.AspNetCore.Mvc;

namespace OnlineKursMVC.Controllers
{
	public class ReviewsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
