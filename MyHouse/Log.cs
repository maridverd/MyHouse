using System.IO;

namespace MyHouse;

public static class Log{
    static Log(){
        File.Delete($"log.txt");
    }
    public static void Write(string text){
        using StreamWriter S = File.AppendText($"log.txt");
        S.Write(text);
    }
    public static void WriteLine(string text){
        using StreamWriter S = File.AppendText($"log.txt");
        S.Write(text + '\n');
    }
}