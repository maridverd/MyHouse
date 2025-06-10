namespace MyHouse;

public class Usuario {
    public Email Email{ get; private set; }
    public Senha Senha{ get; private set; }
    public Nome Nome{ get; private set; }
    public CPF CPF { get; private set; }
    public string Endereco { get; private set; }
    public Usuario(string email, Senha senha, Nome nome, string cpf, string endereco) {
        Email = email;
        Senha = senha;
        Nome = nome;
        CPF = cpf;
        Endereco = endereco;
    }
}