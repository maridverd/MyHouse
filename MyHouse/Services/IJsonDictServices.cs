namespace MyHouse;

public interface IJsonDictServices<TKey, TValue> where TKey : notnull {
    public Dictionary<TKey, TValue> Data { get; set; }
    public abstract void Save();
    public abstract void CarregarDados();
}