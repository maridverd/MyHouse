using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyHouse.Data;
using System;

namespace MyHouse.Pages;

public class FaqModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public FaqModel(ApplicationDbContext context) => _context = context;

    [BindProperty]
    public string NovaPergunta { get; set; }

    [BindProperty]
    public string NovaResposta { get; set; }

    [BindProperty]
    public int PerguntaId { get; set; }

    public List<Pergunta> Perguntas { get; set; }

    public void OnGet()
    {
        Perguntas = _context.Perguntas
            .Include(p => p.Usuario)
            .Include(p => p.Resposta)
                .ThenInclude(r => r.Usuario)
            .OrderByDescending(p => p.Hora)
            .ToList();
    }

    public IActionResult OnPostPergunta()
    {
        var usuario = _context.Usuarios.First(); // ou autenticar
        _context.Perguntas.Add(new Pergunta
        {
            Texto = NovaPergunta,
            Usuario = usuario,
            Hora = DateTime.Now
        });
        _context.SaveChanges();
        return RedirectToPage();
    }

    public IActionResult OnPostResposta()
    {
        var usuario = _context.Usuarios.First(); // ou autenticar
        var pergunta = _context.Perguntas.Include(p => p.Resposta).First(p => p.Id == PerguntaId);
        if (pergunta.Resposta == null)
        {
            var resposta = new Resposta { Texto = NovaResposta, Pergunta = pergunta, Usuario = usuario };
            pergunta.PreencherResposta(resposta);
            _context.Respostas.Add(resposta);
            _context.SaveChanges();
        }
        return RedirectToPage();
    }
}