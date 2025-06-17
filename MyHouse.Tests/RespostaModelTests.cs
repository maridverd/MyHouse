using System;
using Xunit;
using MyHouse;
using MyHouse.Services;
using MyHouse.Tests.Mocks;

namespace MyHouse.Tests.Services
{
    public class RespostaServiceTests
    {
        [Fact]
        public void ResponderPergunta_DeveAdicionarRespostaCorretamente()
        {
            // Arrange
            var mockPerguntas = new MockJsonDict<long, Pergunta>();
            var pergunta = new Pergunta("Qual o horário de funcionamento?", DateTime.Now, new Usuario("user@example.com", "User"));

            mockPerguntas.Data[pergunta.Id] = pergunta;

            var service = new RespostaService(mockPerguntas);

            // Act
            bool resultado = service.ResponderPergunta(pergunta.Id, "Das 8h às 18h.", "suporte@example.com");

            // Assert
            Assert.True(resultado);
            Assert.NotNull(pergunta.Resposta);
            Assert.Equal("Das 8h às 18h.", pergunta.Resposta!.Texto);
            Assert.Equal("suporte@example.com", pergunta.Resposta.SuporteEmail);
        }
    }
}
