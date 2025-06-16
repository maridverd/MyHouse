using System.Collections.Generic;

namespace MyHouse.Tests.Mocks
{
    public class MockJsonDict<TKey, TValue> where TKey : notnull
    {
        public Dictionary<TKey, TValue> Data { get; set; } = new Dictionary<TKey, TValue>();

        public void Save()
        {
            // N�o faz nada, s� para simular o m�todo real
        }
    }
}
