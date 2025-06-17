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

//esse arquito ta quebrado
// Mock simples para o JsonDict<T>
public class JsonDictMock<TKey, TValue> : IJsonDictServices<TKey, TValue> where TKey : notnull {
    public Dictionary<TKey, TValue> Data { get; set; } = new Dictionary<TKey, TValue>();
    public void CarregarDados(){}
    public void Save() { /* não faz nada no teste */ }
}

public class PergunteAquiModelTests
{
    // private DefaultHttpContext CriarHttpContextComSession(string? usuarioEmail)
    private DefaultHttpContext CriarHttpContextComSession(string usuarioEmail)
    {
        var context = new DefaultHttpContext();

        var sessionMock = new Mock<ISession>();
        var store = new Dictionary<string, byte[]>();

        sessionMock.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
            .Callback<string, byte[]>((key, value) => store[key] = value);

        sessionMock.Setup(s => s.TryGetValue(It.IsAny<string>(), out It.Ref<byte[]>.IsAny))
            .Returns((string key, out byte[] value) =>
            {
                var found = store.TryGetValue(key, out var val);
                value = val;
                return found;
            });

        if (usuarioEmail != null)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(usuarioEmail);
            store["UsuarioEmail"] = bytes;
        }

        sessionMock.Setup(s => s.TryGetValue(It.IsAny<string>(), out It.Ref<byte[]>.IsAny))
            .Returns((string key, out byte[] value) =>
            {
                var found = store.TryGetValue(key, out var val);
                value = val;
                return found;
            });


        sessionMock.Setup(s => s.IsAvailable).Returns(true);

        context.Features.Set<ISessionFeature>(new SessionFeature { Session = sessionMock.Object });
        context.Session = sessionMock.Object;

        return context;
    }

    private class SessionFeature : ISessionFeature
    {
        public ISession Session { get; set; }
    }

    [Fact]
    public void OnPostPergunta_ComUsuarioLogado_DeveAdicionarPerguntaERedirecionar()
    {
        // Arrange
        string email = "teste@teste.com";
        var httpContext = CriarHttpContextComSession(email);

        var model = new PergunteAquiModel
        {
            NovaPergunta = "Qual o horário de atendimento?",
            PageContext = new PageContext
            {
                HttpContext = httpContext
            }
        };

        // Substituir JsonDict por mock para controlar os dados
        // Como Perguntas é public e não interface, usaremos reflection para injetar
        IJsonDictServices<string, Pergunta> perguntasMock = new JsonDictMock<string, Pergunta>();

        model.Perguntas = perguntasMock;

        // Act
        var result = model.OnPostPergunta();

        // Assert
        var redirect = Assert.IsType<RedirectToPageResult>(result);
        Assert.Null(redirect.PageName); // Redireciona para a mesma página

        // Verificar se a pergunta foi adicionada ao dicionário
        Assert.Single(perguntasMock.Data);
        var perguntaAdicionada = Assert.Single(perguntasMock.Data.Values);

        Assert.Equal("Qual o horário de atendimento?", perguntaAdicionada.Texto);
        Assert.NotEqual(default, perguntaAdicionada.Hora); // Verifica que a data foi setada
        // Como CriarPergunta depende do email, testar se o usuário está associado, etc, depende da implementação de Pergunta, 
        // mas pelo menos aqui verificamos a pergunta criada com o texto correto
    }

    [Fact]
    public void OnPostPergunta_SemUsuarioLogado_DeveRedirecionarParaLogin()
    {
        // Arrange
        var httpContext = CriarHttpContextComSession(null);

        var model = new PergunteAquiModel
        {
            NovaPergunta = "Qual o horário de atendimento?",
            PageContext = new PageContext
            {
                HttpContext = httpContext
            }
        };

        // Act
        var result = model.OnPostPergunta();

        // Assert
        var redirect = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Login", redirect.PageName);
    }
}
