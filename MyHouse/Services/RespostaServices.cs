using MyHouse.Data;

namespace MyHouse.Services
{
    public class RespostaService : IRespostaService
    {
        private readonly JsonDict<long, Pergunta> _perguntas;

        public RespostaService(JsonDict<long, Pergunta> perguntas)
        {
            _perguntas = perguntas;
        }

        public bool ResponderPergunta(long perguntaId, string textoResposta, string suporteEmail)
        {
            if (!_perguntas.Data.ContainsKey(perguntaId)) return false;

            var resposta = new Resposta(textoResposta, DateTime.Now, suporteEmail);
            _perguntas.Data[perguntaId].PreencherResposta(resposta);
            _perguntas.Save();

            return true;
        }
    }
}
