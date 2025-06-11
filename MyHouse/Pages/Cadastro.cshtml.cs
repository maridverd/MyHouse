using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyHouse;

public class CadastroModel : PageModel {
    [BindProperty] public string? PrimeiroNome{ get; set; }
    [BindProperty] public string? UltimoNome { get; set; }
    [BindProperty] public string? Email { get; set; }
    [BindProperty] public string? CPF { get; set; }
    [BindProperty] public string? Endereco { get; set; }
    [BindProperty] public string? Senha { get; set; }
    [BindProperty] public string? ConfirmaSenha { get; set; }

    public List<CodigoRetorno> Mensagens { get; set; } = new();

    public void OnGet() {}

    public IActionResult OnPost()
    {
        Mensagens.Clear();

        if (Senha != ConfirmaSenha)
        {
            Mensagens.Add(new CodigoRetorno(4, "Senhas não coincidem."));
            return Page();
        }

        var resultado = CadastroSistema.CadastrarUsuario(
            Email!,
            Senha!,
            new Nome(PrimeiroNome!, UltimoNome!),
            CPF!,
            Endereco!
        );

        Mensagens.AddRange(resultado);

        if (resultado.Any(r => r.Codigo == 0))
        {
            return RedirectToPage("/PainelUsuario");
        }

        return Page();
    }
}