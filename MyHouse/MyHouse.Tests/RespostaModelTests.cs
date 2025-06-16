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

            var service = new RespostaServiceFake(mockPerguntas);

            // Act
            bool resultado = service.ResponderPergunta(pergunta.Id, "Das 8h às 18h.", "suporte@example.com");

            // Assert
            Assert.True(resultado);
            Assert.NotNull(pergunta.Resposta);
            Assert.Equal("Das 8h às 18h.", pergunta.Resposta!.Texto);
            Assert.Equal("suporte@example.com", pergunta.Resposta.SuporteEmail);
        }
    }

    // Implementação Fake só pra usar o MockJsonDict
    public class RespostaServiceFake : IRespostaService
    {
        private readonly MockJsonDict<long, Pergunta> _mockPerguntas;

        public RespostaServiceFake(MockJsonDict<long, Pergunta> mockPerguntas)
        {
            _mockPerguntas = mockPerguntas;
        }

        public bool ResponderPergunta(long perguntaId, string textoResposta, string suporteEmail)
        {
            if (!_mockPerguntas.Data.ContainsKey(perguntaId)) return false;

            var resposta = new Resposta(textoResposta, DateTime.Now, suporteEmail);
            _mockPerguntas.Data[perguntaId].PreencherResposta(resposta);
            _mockPerguntas.Save();
            return true;
        }
    }
}
