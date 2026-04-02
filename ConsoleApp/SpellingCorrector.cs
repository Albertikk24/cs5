using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TextFileProcessor {

  public class SpellingCorrector {

    private Dictionary<string, string> _errorWords;

    public SpellingCorrector() {
      _errorWords = new Dictionary<string, string>();
      InitializeErrorDictionary();
    }

    private void InitializeErrorDictionary() {
      // Common misspellings in Russian
      _errorWords["превед"] = "привет";
      _errorWords["пирвет"] = "привет";
      _errorWords["привт"] = "привет";
      _errorWords["превет"] = "привет";
      
      _errorWords["каг"] = "как";
      _errorWords["какк"] = "как";
      
      _errorWords["дила"] = "дела";
      _errorWords["делаа"] = "дела";
      
      _errorWords["хорашо"] = "хорошо";
      _errorWords["хорошоо"] = "хорошо";
      _errorWords["хорощо"] = "хорошо";
      
      _errorWords["спасиба"] = "спасибо";
      _errorWords["спс"] = "спасибо";
      
      _errorWords["пажалуйста"] = "пожалуйста";
      _errorWords["пожалуста"] = "пожалуйста";
      
      _errorWords["извените"] = "извините";
      _errorWords["извенити"] = "извините";
      
      _errorWords["здраствуйте"] = "здравствуйте";
      _errorWords["здраствуйти"] = "здравствуйте";
      _errorWords["здравствуйти"] = "здравствуйте";
      
      _errorWords["досвидания"] = "до свидания";
      _errorWords["дасвидания"] = "до свидания";
    }

    public void AddErrorWord(string wrongWord, string correctWord) {
      if (!string.IsNullOrWhiteSpace(wrongWord) && !string.IsNullOrWhiteSpace(correctWord)) {
        string key = wrongWord.ToLower().Trim();
        string value = correctWord.ToLower().Trim();
        
        if (!_errorWords.ContainsKey(key)) {
          _errorWords.Add(key, value);
        } else {
          _errorWords[key] = value;
        }
        
        Console.WriteLine($"Added: '{key}' -> '{value}'");
      }
    }

    public string CorrectText(string text) {
      if (string.IsNullOrWhiteSpace(text)) {
        return text;
      }

      string result = text;
      
      foreach (KeyValuePair<string, string> errorPair in _errorWords) {
        string pattern = @"\b" + Regex.Escape(errorPair.Key) + @"\b";
        result = Regex.Replace(result, pattern, errorPair.Value, RegexOptions.IgnoreCase);
      }
      
      return result;
    }

    public string CorrectPhoneNumbers(string text) {
      if (string.IsNullOrWhiteSpace(text)) {
        return text;
      }
      
      // Pattern for phone numbers like (012) 345-67-89
      string phonePattern = @"\((\d{3})\)\s*(\d{3})-(\d{2})-(\d{2})";
      string replacement = "+380 $1 $2 $3 $4";
      
      string result = Regex.Replace(text, phonePattern, replacement);
      
      // Pattern for phone numbers like 012-345-67-89
      string phonePattern2 = @"(\d{3})-(\d{3})-(\d{2})-(\d{2})";
      string replacement2 = "+380 $1 $2 $3 $4";
      result = Regex.Replace(result, phonePattern2, replacement2);
      
      // Pattern for phone numbers like 012 345 67 89
      string phonePattern3 = @"(\d{3})\s+(\d{3})\s+(\d{2})\s+(\d{2})";
      string replacement3 = "+380 $1 $2 $3 $4";
      result = Regex.Replace(result, phonePattern3, replacement3);
      
      return result;
    }

    public void ProcessFile(string filePath, bool correctSpelling = true, bool correctPhoneNumbers = true) {
      if (!File.Exists(filePath)) {
        Console.WriteLine($"File not found: {filePath}");
        return;
      }

      Console.WriteLine($"\nProcessing: {filePath}");
      
      string content = File.ReadAllText(filePath, Encoding.UTF8);
      string originalContent = content;
      
      if (correctSpelling) {
        content = CorrectText(content);
        Console.WriteLine("  - Spelling correction applied");
      }
      
      if (correctPhoneNumbers) {
        content = CorrectPhoneNumbers(content);
        Console.WriteLine("  - Phone number correction applied");
      }
      
      if (content != originalContent) {
        File.WriteAllText(filePath, content, Encoding.UTF8);
        Console.WriteLine("  ✓ File updated successfully");
      } else {
        Console.WriteLine("  - No changes needed");
      }
    }

    public void ProcessDirectory(string directoryPath, bool correctSpelling = true, bool correctPhoneNumbers = true, bool recursive = true) {
      if (!Directory.Exists(directoryPath)) {
        Console.WriteLine($"Directory not found: {directoryPath}");
        return;
      }

      SearchOption searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
      string[] files = Directory.GetFiles(directoryPath, "*.txt", searchOption);
      
      Console.WriteLine($"\nFound {files.Length} text files to process...");
      
      int processedCount = 0;
      foreach (string file in files) {
        try {
          ProcessFile(file, correctSpelling, correctPhoneNumbers);
          processedCount++;
        } catch (Exception exception) {
          Console.WriteLine($"  Error: {exception.Message}");
        }
      }
      
      Console.WriteLine($"\n=== SUMMARY ===");
      Console.WriteLine($"Total files found: {files.Length}");
      Console.WriteLine($"Successfully processed: {processedCount}");
    }

    public void ShowErrorDictionary() {
      Console.WriteLine("\n=== ERROR DICTIONARY ===");
      Console.WriteLine($"{"Wrong Word",-20} -> {"Correct Word",-20}");
      Console.WriteLine(new string('-', 45));
      
      foreach (KeyValuePair<string, string> entry in _errorWords) {
        Console.WriteLine($"{entry.Key,-20} -> {entry.Value,-20}");
      }
      
      Console.WriteLine(new string('-', 45));
      Console.WriteLine($"Total entries: {_errorWords.Count}");
    }

    public string TestCorrection(string sampleText) {
      if (string.IsNullOrWhiteSpace(sampleText)) {
        return string.Empty;
      }
      
      string result = CorrectText(sampleText);
      result = CorrectPhoneNumbers(result);
      return result;
    }
  }
}