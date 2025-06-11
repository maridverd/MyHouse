namespace MyHouse;

public static class CadastroSistema {
    private static readonly string ArquivoUsuarios = "usuarios.json";
    public static JsonDict<string, Usuario> Usuarios { get; private set; }

    static CadastroSistema() {
        Usuarios = new(ArquivoUsuarios);
    }

    public static List<CodigoRetorno> CadastrarUsuario(string email, string senha, Nome nome, string cpf, string endereco) {
        List<CodigoRetorno> codigos = new();
        if (email != null && senha != null && cpf != null && endereco != null) {
            if (!Usuario.ValidarEmail(email)) {
                codigos.Add(new(1, "Email inválido."));
            }
            if (!Usuario.ValidarCPF(cpf)) {
                codigos.Add(new(2, "CPF inválido."));
            }
            if (codigos.Count == 0) {
                if (Usuarios.Data.ContainsKey(email)) {
                    codigos.Add(new(3, "Usuário já cadastrado."));
                }
                else {
                    Usuarios.Data.Add(email, new Usuario(email, new Senha(senha), nome, cpf, endereco));
                    Usuarios.Save();
                    codigos.Add(new(0, "Usuário cadastrado com sucesso."));
                }
            }
        }
        else {
            codigos.Add(new(4, "Informações nulas."));
        }
        return codigos;
    }

    public static bool AutenticaUsuario(string email, string senha) {
        Usuario usuario;
        if (!Usuarios.Data.TryGetValue(email, out usuario!)) {
            return false;
        }
        return usuario.Senha.Validate(senha);
    }
}
