using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyHouse.Pages;
using System.Collections.Generic;

using MyHouse;
public class PergunteAquiModelTests {
    private DefaultHttpContext CriarHttpContextComSession(string? email = null) {
        var context = new DefaultHttpContext();

        var sessionMock = new Mock<ISession>();
        var store = new Dictionary<string, byte[]>();

        sessionMock.Setup(s => s.SetString(It.IsAny<string>(), It.IsAny<string>()))
            .Callback<string, string>((key, value) => {
                store[key] = System.Text.Encoding.UTF8.GetBytes(value);
            });

        sessionMock.Setup(s => s.GetString(It.IsAny<string>()))
            .Returns<string>(key => {
                return store.ContainsKey(key) ? System.Text.Encoding.UTF8.GetString(store[key]) : null;
            });

        // Set initial email if provided
        if (email != null) {
            store["UsuarioEmail"] = System.Text.Encoding.UTF8.GetBytes(email);
        }

        context.Session = sessionMock.Object;
        return context;
    }

    [Fact]
    public void OnPostPergunta_ComUsuarioLogado_DeveAdicionarPerguntaERedirecionar() {
        // Arrange
        var httpContext = CriarHttpContextComSession("usuario@teste.com");
        var model = new PergunteAquiModel {
            NovaPergunta = "Qual o horário de funcionamento?",
            PageContext = new PageContext { HttpContext = httpContext }
        };

        // Mock das classes JsonDict usadas no model
        model.Perguntas = new JsonDict<long, Pergunta>("perguntas.json");
        model.Respostas = new JsonDict<long, Resposta>("respostas.json");

        int perguntasAntes = model.Perguntas.Data.Count;

        // Act
        var result = model.OnPostPergunta();

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        var redirect = (RedirectToPageResult)result;
        Assert.Null(redirect.PageName); // redireciona para a mesma página

        Assert.Equal(perguntasAntes + 1, model.Perguntas.Data.Count);
        Assert.Contains(model.Perguntas.Data.Values, p => p.Texto == "Qual o horário de funcionamento?");
    }

    [Fact]
    public void OnPostPergunta_SemUsuarioLogado_DeveRedirecionarParaLogin() {
        // Arrange
        var httpContext = CriarHttpContextComSession(); // sem email
        var model = new PergunteAquiModel {
            NovaPergunta = "Posso fazer visita?",
            PageContext = new PageContext { HttpContext = httpContext }
        };

        // Act
        var result = model.OnPostPergunta();

        // Assert
        var redirect = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Login", redirect.PageName);
    }
}
