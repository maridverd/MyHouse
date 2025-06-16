using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyHouse.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<Casa>? ListaDeCasas { get; set; }

        public void OnGet()
        {
            string json = System.IO.File.ReadAllText("Houses/OnDataBase.json");
            ListaDeCasas = JsonSerializer.Deserialize<List<Casa>>(json);
        }
    }
}

public class Casa
{
    public string? Id { get; set; }
    public string? Img { get; set; }
}
