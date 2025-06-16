namespace MyHouse.Services;

public interface IRespostaService
{
    bool ResponderPergunta(long perguntaId, string textoResposta, string suporteEmail);
}
