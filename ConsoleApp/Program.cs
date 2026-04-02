using System;

namespace TextFileProcessor {

  class Program {

    static void Main(string[] args) {
      Console.ForegroundColor = ConsoleColor.Cyan;
      string headerMessage = "==========================================\n" +
                           "     TEXT FILE PROCESSOR\n" +
                           "     (Spelling & Phone Numbers Corrector)\n" +
                           "==========================================";
      Console.WriteLine(headerMessage);
      Console.ResetColor();

      TextFileProcessorApp app = new TextFileProcessorApp();
      app.Run();
    }
  }
}