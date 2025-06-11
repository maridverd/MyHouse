
public readonly struct Senha
{
    private readonly string _Senha;

    public Senha(string senha)
    {
        _Senha = senha;
    }

    public override string ToString() => _Senha;

    public override bool Equals(object? obj) => obj is Senha other && _Senha == other._Senha;
    public override int GetHashCode() => _Senha.GetHashCode();

    public static implicit operator Senha(string senha) => new(senha);
    public static implicit operator string(Senha senha) => senha._Senha;

    public bool Valida => _Senha.Length >= 6; // Exemplo de validação
}