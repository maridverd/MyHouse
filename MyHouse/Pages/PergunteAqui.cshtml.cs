﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyHouse.Data;
using System;

namespace MyHouse.Pages;

public class PergunteAquiModel : PageModel {
    [BindProperty]
    public string? NovaPergunta { get; set; }

    [BindProperty]
    public string? NovaResposta { get; set; }

    public IJsonDictServices<long, Pergunta>? Perguntas = new JsonDict<long, Pergunta>("perguntas.json");
    public IJsonDictServices<long, Resposta>? Respostas = new JsonDict<long, Resposta>("respostas.json");

    public void OnGet() {
    }

    public IActionResult OnPostPergunta() {
        string? email = HttpContext.Session.GetString("UsuarioEmail");
        if (email == null) return RedirectToPage("/Login");
        Pergunta pergunta = new Pergunta(NovaPergunta!, DateTime.Now, email);
        if (!Perguntas!.Data.ContainsKey(pergunta.Id)) {
            Perguntas!.Data.Add(pergunta.Id, pergunta);
            Perguntas.Save();
        }
        return RedirectToPage();
    }

    public IActionResult OnPostResposta() {
        // if (CadastroSistema.Usuario == null) return RedirectToPage("/Login");
        // var pergunta = _context.Perguntas.Include(p => p.Resposta).First(p => p.Id == PerguntaId);
        // if (pergunta.Resposta == null)
        // {
        //     var resposta = new Resposta { Texto = NovaResposta, Pergunta = pergunta, Usuario = usuario };
        //     pergunta.PreencherResposta(resposta);
        //     _context.Respostas.Add(resposta);
        //     _context.SaveChanges();
        // }
        return RedirectToPage();
    }
}