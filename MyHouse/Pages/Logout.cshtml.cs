using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyHouse.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Limpa o usuário logado no seu sistema
            CadastroSistema.Logout();

            // Se você estiver usando Session para guardar algo, limpe também
            HttpContext.Session.Clear();

            // Redireciona para a página principal
            return RedirectToPage("/Index");
        }
    }
}
