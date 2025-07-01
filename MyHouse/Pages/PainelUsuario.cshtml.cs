using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyHouse;

public class PainelUsuarioModel : PageModel
{
    public string? NomeUsuario { get; private set; }
    public List<Casa>? ListaDeCasas { get; set; }

    public void OnGet()
    {
        // Carregar dados do usuário
        var email = HttpContext.Session.GetString("UsuarioEmail");
        if (!string.IsNullOrEmpty(email))
        {
            NomeUsuario = Cadastro.Usuarios.Data[email].Nome.Completo; // Exemplo, adapte conforme sua classe Nome
        }

        // Carregar lista de casas (mesma funcionalidade da IndexModel)
        string json = System.IO.File.ReadAllText("Houses/OnDataBase.json");
        ListaDeCasas = JsonConvert.DeserializeObject<List<Casa>>(json);
    }
}