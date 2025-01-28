using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace PointGenerator.Pages;
using Point.Models;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

        // Initialize properties
        UserPoint = new Point();
    }

    [BindProperty]
    public Point UserPoint { get; set; }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Invalid input. Please try again."); // Print to console
            return Page();
        }

        try
        {
            var client = _httpClientFactory.CreateClient();
            var jsonContent = JsonSerializer.Serialize(UserPoint);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5190/points", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Point sent successfully!" + jsonContent);
                  TempData["SendStatus"] = "success";
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("error:" + errorContent);
                TempData["SendStatus"] = "error";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred while sending the point : " + ex.Message);
            TempData["SendStatus"] = "error";

        }

        return Page();
    }
}

