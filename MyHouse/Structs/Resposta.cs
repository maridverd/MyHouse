namespace MyHouse;

public class Resposta {
    public readonly string Texto;
    public readonly DateTime Hora;
    public Usuario Suporte { get; private set; }
    public Resposta(string texto, DateTime hora, Usuario suporte) {
        Texto = texto;
        Hora = hora;
        Suporte = suporte;
    }
}