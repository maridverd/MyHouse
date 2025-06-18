using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyHouse.Pages;
using MyHouse;
using System;
using System.Collections.Generic;

public class PergunteAquiModelTests {
    private DefaultHttpContext CriarHttpContextComSession(string usuarioEmail) {
        var context = new DefaultHttpContext();

        var sessionMock = new Mock<ISession>();
        var store = new Dictionary<string, byte[]>();

        sessionMock.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
            .Callback<string, byte[]>((key, value) => store[key] = value);

        sessionMock.Setup(s => s.TryGetValue(It.IsAny<string>(), out It.Ref<byte[]>.IsAny))
            .Returns((string key, out byte[] value) => {
                var found = store.TryGetValue(key, out var val);
                value = val;
                return found;
            });

        if (usuarioEmail != null) {
            var bytes = System.Text.Encoding.UTF8.GetBytes(usuarioEmail);
            store["UsuarioEmail"] = bytes;
        }

        sessionMock.Setup(s => s.TryGetValue(It.IsAny<string>(), out It.Ref<byte[]>.IsAny))
            .Returns((string key, out byte[] value) => {
                var found = store.TryGetValue(key, out var val);
                value = val;
                return found;
            });


        sessionMock.Setup(s => s.IsAvailable).Returns(true);

        context.Features.Set<ISessionFeature>(new SessionFeature { Session = sessionMock.Object });
        context.Session = sessionMock.Object;

        return context;
    }

    private class SessionFeature : ISessionFeature {
        public ISession Session { get; set; }
    }

    [Fact]
    public void OnPostPergunta_ComUsuarioLogado_DeveAdicionarPerguntaERedirecionar() {
        string email = "teste@teste.com";
        var httpContext = CriarHttpContextComSession(email);

        var model = new PergunteAquiModel {
            NovaPergunta = "Qual o horário de atendimento?",
            PageContext = new PageContext {
                HttpContext = httpContext
            }
        };

        IJsonDictServices<long, Pergunta> perguntasMock = new JsonDictMock<long, Pergunta>();

        model.Perguntas = perguntasMock;
        if (!Cadastro.Usuarios.Data.ContainsKey("teste@teste.com"))
            Cadastro.Usuarios.Data.Add("teste@teste.com", new Usuario("teste@teste.com", new Senha("SenhaMuitoSegura"), new Nome("John", "Teste"), "65204204091", "Testelândia"));

        var result = model.OnPostPergunta();

        var redirect = Assert.IsType<RedirectToPageResult>(result);
        Assert.Null(redirect.PageName);

        Assert.Single(perguntasMock.Data);
        var perguntaAdicionada = Assert.Single(perguntasMock.Data.Values);

        Assert.Equal("Qual o horário de atendimento?", perguntaAdicionada.Texto);
        Assert.NotEqual(default, perguntaAdicionada.Hora);
    }

    [Fact]
    public void OnPostPergunta_SemUsuarioLogado_DeveRedirecionarParaLogin() {
        var httpContext = CriarHttpContextComSession(null);

        var model = new PergunteAquiModel {
            NovaPergunta = "Qual o horário de atendimento?",
            PageContext = new PageContext {
                HttpContext = httpContext
            }
        };

        var result = model.OnPostPergunta();

        var redirect = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Login", redirect.PageName);
    }
}
