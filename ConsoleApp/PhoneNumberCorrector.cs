using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TextFileProcessor {
  public class PhoneNumberCorrector {
    public string CorrectPhoneNumbers(string text) {
      if (string.IsNullOrWhiteSpace(text)) {
        return text;
      }

      string result = text;

      string pattern1 = @"\((\d{3})\)\s*(\d{3})-(\d{2})-(\d{2})";
      string replacement1 = "+380 $1 $2 $3 $4";
      result = Regex.Replace(result, pattern1, replacement1);

      string pattern2 = @"(\d{3})-(\d{3})-(\d{2})-(\d{2})";
      string replacement2 = "+380 $1 $2 $3 $4";
      result = Regex.Replace(result, pattern2, replacement2);

      string pattern3 = @"(\d{3})\s+(\d{3})\s+(\d{2})\s+(\d{2})";
      string replacement3 = "+380 $1 $2 $3 $4";
      result = Regex.Replace(result, pattern3, replacement3);

      string pattern4 = @"(\d{3})(\d{3})(\d{2})(\d{2})";
      string replacement4 = "+380 $1 $2 $3 $4";
      result = Regex.Replace(result, pattern4, replacement4);

      return result;
    }

    public string FindPhoneNumbers(string text) {
      if (string.IsNullOrWhiteSpace(text)) {
        return string.Empty;
      }

      StringBuilder results = new StringBuilder();
      results.AppendLine("\n=== FOUND PHONE NUMBERS ===");

      string[] patterns = {
        @"\((\d{3})\)\s*(\d{3})-(\d{2})-(\d{2})",
        @"(\d{3})-(\d{3})-(\d{2})-(\d{2})",
        @"(\d{3})\s+(\d{3})\s+(\d{2})\s+(\d{2})",
        @"(\d{10})"
      };

      int foundCount = 0;

      foreach (string pattern in patterns) {
        MatchCollection matches = Regex.Matches(text, pattern);

        foreach (Match match in matches) {
          results.AppendLine($"  - {match.Value}");
          ++foundCount;
        }
      }

      if (foundCount == 0) {
        results.AppendLine("  No phone numbers found.");
      } else {
        results.AppendLine($"\nTotal found: {foundCount}");
      }

      return results.ToString();
    }
  }
}