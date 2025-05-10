using System;
using System.Collections.Generic;
using System.Windows.Forms;

class Program
{
    static Dictionary<string, string> substitutions = new()
    {
        { "x.com", "girlcockx.com" },
        { "www.tiktok.com", "vxtiktok.com" },
        { "youtube.com/shorts", "youtu.be" }
    };

    [STAThread]
    static void Main()
    {
        Console.WriteLine("FixLink running...");

        Timer timer = new();
        timer.Interval = 100;
        timer.Tick += (s, e) => CheckClipboard();
        timer.Start();

        Application.Run();
    }

    static void CheckClipboard()
    {
        try
        {
            if (Clipboard.ContainsText())
            {
                string text = Clipboard.GetText();
                string originalText = text;

                foreach (var pair in substitutions)
                {
                    if (text.StartsWith("https://"))
                    {
                        string key = $"//{pair.Key}";

                        if (text.Contains(key, StringComparison.OrdinalIgnoreCase))
                        {
                            text = text.Replace(key, $"//{pair.Value}", StringComparison.OrdinalIgnoreCase);
                        }
                    }
                }

                if (text != originalText)
                {
                    Clipboard.SetText(text);
                    Console.WriteLine($"Updated clipboard: {text}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Clipboard error: {ex.Message}");
        }
    }
}
