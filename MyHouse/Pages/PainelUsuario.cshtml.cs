using Microsoft.AspNetCore.Mvc.RazorPages;
using MyHouse;

public class PainelUsuarioModel : PageModel
{
    public string? NomeUsuario { get; private set; }

    public void OnGet()
    {
        var email = HttpContext.Session.GetString("UsuarioEmail");
        if (!string.IsNullOrEmpty(email))
        {
            NomeUsuario = CadastroSistema.Usuarios.Data[email].Nome.Completo; // Exemplo, adapte conforme sua classe Nome
        }
    }
}
