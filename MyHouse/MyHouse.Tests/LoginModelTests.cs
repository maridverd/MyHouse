using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyHouse.Pages;
using MyHouse.Services;
using System.Collections.Generic;

public class LoginModelTests
{
    private DefaultHttpContext CriarHttpContextComSession()
    {
        var context = new DefaultHttpContext();

        // Mock simples de ISession
        var sessionMock = new Mock<ISession>();
        var store = new Dictionary<string, byte[]>();

        sessionMock.Setup(s => s.SetString(It.IsAny<string>(), It.IsAny<string>()))
            .Callback<string, string>((key, value) =>
            {
                store[key] = System.Text.Encoding.UTF8.GetBytes(value);
            });

        sessionMock.Setup(s => s.GetString(It.IsAny<string>()))
            .Returns<string>(key =>
            {
                return store.ContainsKey(key) ? System.Text.Encoding.UTF8.GetString(store[key]) : null;
            });

        context.Session = sessionMock.Object;
        return context;
    }

    [Fact]
    public void OnPost_LoginBemSucedido_DeveRedirecionarParaPainel()
    {
        // Arrange
        var mockService = new Mock<ILoginService>();
        var httpContext = CriarHttpContextComSession();

        mockService.Setup(s => s.AutenticaUsuario("teste@email.com", "senha123", httpContext))
                   .Returns(true);

        var model = new LoginModel(mockService.Object)
        {
            Email = "teste@email.com",
            Senha = "senha123",
            PageContext = new PageContext
            {
                HttpContext = httpContext
            }
        };

        // Act
        var resultado = model.OnPost();

        // Assert
        var redirect = Assert.IsType<RedirectToPageResult>(resultado);
        Assert.Equal("/PainelUsuario", redirect.PageName);
    }

    [Fact]
    public void OnPost_LoginFalhou_DeveMostrarMensagemErro()
    {
        // Arrange
        var mockService = new Mock<ILoginService>();
        var httpContext = CriarHttpContextComSession();

        mockService.Setup(s => s.AutenticaUsuario("usuario@teste.com", "errada", httpContext))
                   .Returns(false);

        var model = new LoginModel(mockService.Object)
        {
            Email = "usuario@teste.com",
            Senha = "errada",
            PageContext = new PageContext
            {
                HttpContext = httpContext
            }
        };

        // Act
        var resultado = model.OnPost();

        // Assert
        Assert.IsType<PageResult>(resultado);
        Assert.Single(model.Mensagens);
        Assert.Equal(1, model.Mensagens[0].Codigo);
        Assert.Equal("Email e/ou senha incorretos", model.Mensagens[0].Mensagem);
    }
}