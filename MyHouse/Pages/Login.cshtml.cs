using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyHouse.Pages;
public class LoginModel : PageModel{
    [BindProperty] public string? Email { get; set; }
    [BindProperty] public string? Senha { get; set; }

    public List<CodigoRetorno> Mensagens { get; set; } = new();

    public void OnGet() {}

    public IActionResult OnPost(){
        Mensagens.Clear();

        if (CadastroSistema.AutenticaUsuario(Email, Senha)) {
            return RedirectToPage("/PainelUsuario");
        }
        else {
            Log.Instance.WriteLine("Erro no Login");
            Mensagens.Add(new CodigoRetorno(1, "Email e/ou senha incorretos"));
        }

        return Page();
    }
}