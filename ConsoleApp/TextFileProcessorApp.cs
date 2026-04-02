using System;
using System.IO;

namespace TextFileProcessor {
  public class TextFileProcessorApp {
    private SpellingCorrector _spellingCorrector;
    private PhoneNumberCorrector _phoneCorrector;

    public TextFileProcessorApp() {
      _spellingCorrector = new SpellingCorrector();
      _phoneCorrector = new PhoneNumberCorrector();
    }

    public void Run() {
      bool isRunning = true;

      while (isRunning) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n┌─────────────────────────────────────────┐\n│            PROCESSOR MENU               │\n├─────────────────────────────────────────┤\n│  1. Process single file                 │\n│  2. Process entire directory            │\n│  3. Show error dictionary               │\n│  4. Add custom error word               │\n│  5. Test correction on sample text      │\n│  6. Find phone numbers in text          │\n│  7. Exit                                │\n└─────────────────────────────────────────┘\nYour choice: ");
        Console.ResetColor();

        string choice = Console.ReadLine();

        switch (choice) {
          case "1":
            ProcessSingleFile();
            break;

          case "2":
            ProcessDirectory();
            break;

          case "3":
            _spellingCorrector.ShowErrorDictionary();
            break;

          case "4":
            AddCustomErrorWord();
            break;

          case "5":
            TestCorrection();
            break;

          case "6":
            FindPhoneNumbers();
            break;

          case "7":
            isRunning = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nGoodbye!");
            Console.ResetColor();
            break;

          default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid choice. Please try again.");
            Console.ResetColor();
            break;
        }
      }
    }

    private void ProcessSingleFile() {
      Console.Write("\nEnter file path to process: ");
      string filePath = Console.ReadLine();

      if (!File.Exists(filePath)) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"File not found: {filePath}");
        Console.ResetColor();
        return;
      }

      Console.Write("Correct spelling errors? (yes/no): ");
      string spellingChoice = Console.ReadLine();
      bool correctSpelling = spellingChoice.ToLower() == "yes" || spellingChoice.ToLower() == "y";

      Console.Write("Correct phone numbers? (yes/no): ");
      string phoneChoice = Console.ReadLine();
      bool correctPhoneNumbers = phoneChoice.ToLower() == "yes" || phoneChoice.ToLower() == "y";

      _spellingCorrector.ProcessFile(filePath, correctSpelling, correctPhoneNumbers);
    }

    private void ProcessDirectory() {
      Console.Write("\nEnter directory path to process: ");
      string directoryPath = Console.ReadLine();

      if (!Directory.Exists(directoryPath)) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Directory not found: {directoryPath}");
        Console.ResetColor();
        return;
      }

      Console.Write("Process subdirectories? (yes/no): ");
      string recursiveChoice = Console.ReadLine();
      bool recursive = recursiveChoice.ToLower() == "yes" || recursiveChoice.ToLower() == "y";

      Console.Write("Correct spelling errors? (yes/no): ");
      string spellingChoice = Console.ReadLine();
      bool correctSpelling = spellingChoice.ToLower() == "yes" || spellingChoice.ToLower() == "y";

      Console.Write("Correct phone numbers? (yes/no): ");
      string phoneChoice = Console.ReadLine();
      bool correctPhoneNumbers = phoneChoice.ToLower() == "yes" || phoneChoice.ToLower() == "y";

      _spellingCorrector.ProcessDirectory(directoryPath, correctSpelling, correctPhoneNumbers, recursive);
    }

    private void AddCustomErrorWord() {
      Console.Write("\nEnter misspelled word: ");
      string wrongWord = Console.ReadLine();

      Console.Write("Enter correct word: ");
      string correctWord = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(wrongWord) && !string.IsNullOrWhiteSpace(correctWord)) {
        _spellingCorrector.AddErrorWord(wrongWord, correctWord);
      } else {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid input. Both words are required.");
        Console.ResetColor();
      }
    }

    private void TestCorrection() {
      Console.WriteLine("\n--- TEST CORRECTION ---\nEnter a sample text to test correction:");
      Console.Write("> ");
      string sampleText = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(sampleText)) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No text entered.");
        Console.ResetColor();
        return;
      }

      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("\n--- ORIGINAL TEXT ---");
      Console.ResetColor();
      Console.WriteLine(sampleText);

      string corrected = _spellingCorrector.TestCorrection(sampleText);

      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("\n--- CORRECTED TEXT ---");
      Console.ResetColor();
      Console.WriteLine(corrected);
    }

    private void FindPhoneNumbers() {
      Console.WriteLine("\n--- FIND PHONE NUMBERS ---\nEnter text to search for phone numbers:");
      Console.Write("> ");
      string sampleText = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(sampleText)) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No text entered.");
        Console.ResetColor();
        return;
      }

      string result = _phoneCorrector.FindPhoneNumbers(sampleText);
      Console.WriteLine(result);
    }
  }
}