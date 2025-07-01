using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyHouse.Pages
{
    public class PersonalizarComodoModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? ComodoId { get; set; }

        [BindProperty]
        public bool Iluminacao { get; set; }

        [BindProperty]
        public bool Moveis { get; set; }

        [BindProperty]
        public bool Piso { get; set; }

        public string? Mensagem { get; set; }

        public void OnGet() {
        }

        public IActionResult OnPost()
        {
            Mensagem = "Personalização salva com sucesso!";

            

            return Page();
        }
    }
}
