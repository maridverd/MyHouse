using Xunit;
using Moq;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyHouse.Pages;
using MyHouse.Services;
using System.Collections.Generic;

public class LoginModelTests{
    private DefaultHttpContext CriarHttpContextComSession(){
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

        sessionMock.Setup(s => s.IsAvailable).Returns(true);

        context.Features.Set<ISessionFeature>(new SessionFeature { Session = sessionMock.Object });
        context.Session = sessionMock.Object;

        return context;
    }
    private class SessionFeature : ISessionFeature {
    public ISession Session { get; set; }
    }
    [Fact]
    public void OnPost_LoginBemSucedido_DeveRedirecionarParaPainel() {
        var mockService = new Mock<ILoginService>();
        var httpContext = CriarHttpContextComSession();

        mockService.Setup(s => s.AutenticaUsuario("teste@email.com", "senha123", httpContext))
                   .Returns(true);

        var model = new LoginModel(mockService.Object) {
            Email = "teste@email.com",
            Senha = "senha123",
            PageContext = new PageContext {
                HttpContext = httpContext
            }
        };

        var resultado = model.OnPost();

        var redirect = Assert.IsType<RedirectToPageResult>(resultado);
        Assert.Equal("/PainelUsuario", redirect.PageName);
    }

    [Fact]
    public void OnPost_LoginFalhou_DeveMostrarMensagemErro() {
        var mockService = new Mock<ILoginService>();
        var httpContext = CriarHttpContextComSession();

        mockService.Setup(s => s.AutenticaUsuario("usuario@teste.com", "errada", httpContext))
                   .Returns(false);

        var model = new LoginModel(mockService.Object) {
            Email = "usuario@teste.com",
            Senha = "errada",
            PageContext = new PageContext {
                HttpContext = httpContext
            }
        };

        var resultado = model.OnPost();

        Assert.IsType<PageResult>(resultado);
        Assert.Single(model.Mensagens);
        Assert.Equal(1, model.Mensagens[0].Codigo);
        Assert.Equal("Email e/ou senha incorretos", model.Mensagens[0].Mensagem);
    }
}