using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyHouse.Data;
using System;

namespace MyHouse.Pages;

public class FaqModel : PageModel{
    // [BindProperty]
    // public string NovaPergunta { get; set; }

    // [BindProperty]
    // public string NovaResposta { get; set; }

    // [BindProperty]
    // public int PerguntaId { get; set; }

    // public List<Pergunta> Perguntas { get; set; }
    public JsonDict<int, Pergunta>? Perguntas{ get; private set; }
    public JsonDict<int, Resposta>? Respostas{ get; private set; }

    public void OnGet() {
        Perguntas = new("perguntas.json");
        Respostas = new("respostas.json");
    }
}