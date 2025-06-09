namespace MyHouse;

public readonly struct CPF {
    private readonly string _CPF;
    public CPF(string cpf) {
        _CPF = cpf;
    }
    public bool Valido {
        get {
            if (_CPF.Length != 11) return false;
            int total1 = 0;
            int total2 = 0;
            for (int i = 0; i < 9; i++) {
                int c = _CPF[i] - '0';
                if (c < 0 || c > 9) return false;
                total1 += c * (i + 1);
                total2 += c * i;
            }
            total1 = total1 % 11 % 10;
            if (total1 != _CPF[9] - '0') return false;
            total2 = (total2 + total1 * 9) % 11 % 10;

            return total2 == _CPF[10] - '0';
        }
    }
    public static implicit operator CPF(string cpf) => new(cpf);
    public static implicit operator string(CPF cpf) => $"{cpf._CPF[0..2]}";
}