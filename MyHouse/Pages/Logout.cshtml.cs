using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyHouse.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Limpa o usu�rio logado no seu sistema
            CadastroSistema.Logout();

            // Se voc� estiver usando Session para guardar algo, limpe tamb�m
            HttpContext.Session.Clear();

            // Redireciona para a p�gina principal
            return RedirectToPage("/Index");
        }
    }
}
