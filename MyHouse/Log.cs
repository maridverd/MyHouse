using System.IO;

namespace MyHouse;

public class Log {
    readonly string LogPath;
    private Log(string logPath) {
        LogPath = logPath;
        File.Delete($"{LogPath}");
    }
    public void Write(string text) {
        using StreamWriter S = File.AppendText($"{LogPath}");
        S.Write(text);
    }
    public void WriteLine(string text) {
        using StreamWriter S = File.AppendText($"{LogPath}");
        S.Write(text + '\n');
    }
    public static Log Instance{ get; private set; } = new Log("log.txt");
}