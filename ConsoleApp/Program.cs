using System;

namespace TextFileProcessor {
  class Program {
    static void Main(string[] args) {
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("==========================================\n     TEXT FILE PROCESSOR\n     (Spelling & Phone Numbers Corrector)\n==========================================");
      Console.ResetColor();

      TextFileProcessorApp app = new TextFileProcessorApp();
      app.Run();
    }
  }
}