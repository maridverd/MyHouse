using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyHouse.Data;
using System;

namespace MyHouse.Pages;

public class ResponderPerguntasModel : PageModel {
    [BindProperty]
    public long PerguntaId { get; set; }

    [BindProperty]
    public string? NovaResposta { get; set; }

    public JsonDict<long, Pergunta>? Perguntas;

    public void OnGet() {
        Perguntas = new("perguntas.json");
    }

    public IActionResult OnPostResposta() {
        Perguntas = new("perguntas.json");
        string? email = HttpContext.Session.GetString("UsuarioEmail");
        if (email == null) return RedirectToPage("/Login");
        Resposta resposta = new(NovaResposta!, DateTime.Now, email!);
        Perguntas.Data[PerguntaId].PreencherResposta(resposta);
        Perguntas.Save();
        return RedirectToPage();
    }
}