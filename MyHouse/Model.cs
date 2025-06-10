using System.Text.Json;
public class Model
{
    public string? Id { get; init; } // identificador do modelo
    private int _sales;
    public int Sales
    {
        get => _sales;
        set
        {
            if (value < _sales)
                throw new ArgumentException("O número de vendas só pode aumentar");
            _sales = value;
        }

    } // Número de vendas desse modelo
    public decimal Price { get; set; } // Preço da casa em reais
    public bool Avaiable { get; set; } // Se o modelo está disponível para venda

    // Construtor
    public Model(string? id, int sales, decimal price, bool avaiable)
    {
        Id = id;
        Sales = sales;
        Price = price;
        Avaiable = avaiable;
    }

    // Método para instanciar models a partir do json (passa o id que ele retorna o json)
    public static Model Create(string? id)
    {
        try
        {
            string jsonPath = $"Values/{id}.json";
            string jsonString = File.ReadAllText(jsonPath);
            Model? aux = JsonSerializer.Deserialize<Model>(jsonString);

            if (aux != null)
                return aux;
            else
                throw new Exception($"O arquivo {jsonPath} está vazio");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Arquivo não encontrado: {ex}");
            return new Model(String.Empty, 0, 0, false);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado: {ex}");
            return new Model(String.Empty, 0, 0, false);
        }
    }

    // Guarda as novas informações no json, deve ser chamado quando o Model atual não for mais modificado
    public void Store()
    {
        try
        {
            // Caminho do arquivo
            string jsonPath = $"Values/{Id}.json";

            // Serializa o objeto atual
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(this, options);

            // Escreve no arquivo
            File.WriteAllText(jsonPath, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar o modelo: {ex.Message}");
        }
    }
}