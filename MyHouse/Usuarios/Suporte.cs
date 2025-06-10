namespace MyHouse;

public class Suporte : Usuario {
    public Suporte(string email, Senha senha, Nome nome, string cpf, string endereco) : base(email, senha, nome, cpf, endereco) { }
    public int PerguntasRespondidas { get; private set; } = 0;
    // void ResponderPergunta(Pergunta pergunta) {
        
    // }
}