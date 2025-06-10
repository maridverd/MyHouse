using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyHouse.Pages
{
    public class PainelUsuarioModel : PageModel
    {
        private readonly ILogger<PainelUsuarioModel> _logger;

        public PainelUsuarioModel(ILogger<PainelUsuarioModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
