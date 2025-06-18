using System;
using Xunit;
using MyHouse;
using MyHouse.Services;

namespace MyHouse.Tests.Services {
    public class RespostaServiceTests {
        [Fact]
        public void ResponderPergunta_DeveAdicionarRespostaCorretamente() {
            var mockPerguntas = new JsonDictMock<long, Pergunta>();
            var pergunta = new Pergunta("Qual o horário de funcionamento?", DateTime.Now, new Usuario("user@example.com", new Senha("SenhaMuitoSegura"), new Nome("John", "Teste"), "65204204091", "Testelândia"));

            mockPerguntas.Data[pergunta.Id] = pergunta;

            var service = new RespostaServiceFake(mockPerguntas);

            bool resultado = service.ResponderPergunta(pergunta.Id, "Das 8h às 18h.", "suporte@example.com");

            Assert.True(resultado);
            Assert.NotNull(pergunta.Resposta);
            Assert.Equal("Das 8h às 18h.", pergunta.Resposta!.Texto);
            Assert.Equal("suporte@example.com", pergunta.Resposta.SuporteEmail);
        }
    }

    public class RespostaServiceFake : IRespostaService {
        private readonly JsonDictMock<long, Pergunta> _mockPerguntas;

        public RespostaServiceFake(JsonDictMock<long, Pergunta> mockPerguntas) {
            _mockPerguntas = mockPerguntas;
        }

        public bool ResponderPergunta(long perguntaId, string textoResposta, string suporteEmail) {
            if (!_mockPerguntas.Data.ContainsKey(perguntaId)) return false;

            var resposta = new Resposta(textoResposta, DateTime.Now, suporteEmail);
            _mockPerguntas.Data[perguntaId].PreencherResposta(resposta);
            _mockPerguntas.Save();
            return true;
        }
    }
}
