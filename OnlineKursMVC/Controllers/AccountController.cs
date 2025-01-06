using Microsoft.AspNetCore.Mvc;
using OnlineKurs.Shared.Models;
using System.Text;
using System.Text.Json;

namespace OnlineKursMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Create an HttpClient instance
            var client = _httpClientFactory.CreateClient();

            // Create the login payload
            var loginRequest = new LoginDTO { Email = email, Password = password };

            // Send the POST request to the API
            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7121/api/Auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            // Parse the token from the response
            var responseData = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<LoginResponse>(responseData);

            if (jsonResponse != null && !string.IsNullOrEmpty(jsonResponse.token))
            {
                HttpContext.Session.SetString("jwtToken", jsonResponse.token);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "An unexpected error occurred. Please try again.";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("jwtToken"); // Clear the token
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password, string role)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var registerPayload = new RegisterDTO
                {
                    Username = username,
                    Email = email,
                    Password = password,
                    Role = role
                };

                // Send the POST request to the API
                var content = new StringContent(JsonSerializer.Serialize(registerPayload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7121/api/Auth/register", content);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Success = "Registration successful! Please login.";
                    return View();
                }

                // Handle errors
                var errorResponse = await response.Content.ReadAsStringAsync();
                ViewBag.Error = $"Registration failed: {errorResponse}";
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine($"Error during registration: {ex.Message}");
                ViewBag.Error = "An unexpected error occurred. Please try again.";
                return View();
            }
        }
    }
}
