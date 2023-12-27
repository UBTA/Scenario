using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace EblanDev.ScenarioCore.UtilsFramework.Extensions
{
	public static class StringExtensions
	{
		public static string ToMD5Hash(this string input) => input.ToHash(MD5.Create());
		public static string ToSHA256Hash(this string input) => input.ToHash(SHA256.Create());
		public static string ToHash(this string input) => input.ToHash(HashAlgorithm.Create());

		public static void ToVersion(this string version, out int major, out int minor, out int patch)
		{
			var versions = Application.version.Split('.');
			
			major = 0;
			if (versions.Length > 0 && int.TryParse(versions[0], out var majorNumber))
			{
				major = majorNumber;
			}
			minor = 0;
			if (versions.Length > 1 && int.TryParse(versions[1], out var minorNumber))
			{
				minor = minorNumber;
			}
			patch = 0;
			if (versions.Length > 2 && int.TryParse(versions[2], out var patchNumber))
			{
				patch = patchNumber;
			}
		}

		private static string ToHash<TAlgorithm>(this string input, TAlgorithm algorithm)
			where TAlgorithm : HashAlgorithm
		{
			using (algorithm)
			{
				var inputBytes = Encoding.ASCII.GetBytes(input);
				var hashBytes = algorithm.ComputeHash(inputBytes);
				
				var sb = new StringBuilder();
				for (var i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("X2"));
				}
				
				return sb.ToString();
			}
		}

		public static List<string> ToWords(this string name)
		{
			var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
			var words = pattern
				.Matches(name)
				.Cast<Match>()
				.Select(m => m.Value);
			
			return words.ToList();
		}
		
		public static string ToSnakeCase(this string name)
		{
			var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
			var words = pattern
				.Matches(name)
				.Cast<Match>()
				.Select(m => m.Value);
			
			return string.Join("_", words).ToLower();
		}

		public static string ToPascalCase(this string name)
		{
			var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
			var words = pattern
				.Matches(name)
				.Cast<Match>()
				.Select(m => m.Value.ToTitleCase());

			return string.Join("", words);
		}

		public static string ToSpacedCase(this string name)
		{
			var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
			var words = pattern
				.Matches(name)
				.Cast<Match>()
				.Select(m => m.Value.ToTitleCase());

			return string.Join(" ", words);
		}
		
		public static string ToSpacedDashCase(this string name)
		{
			var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z,-][a-z,-]+[0-9,-]*|\b)|[A-Z,-]?[a-z,-]+[0-9,-]*|[A-Z,-]|[0-9,-]+");
			var words = pattern
				.Matches(name)
				.Cast<Match>()
				.Select(m => m.Value.ToTitleCase());

			return string.Join(" ", words);
		}

		public static string ToTitleCase(this string word)
		{
			var firstLetter = word[0];

			return word.Remove(0, 1).Insert(0, firstLetter.ToString().ToUpper());
		}
		
		public static string ToRelativePath(this string absolutePath)
		{
			var relativePath = absolutePath.Substring(Application.dataPath.Length + 1);

			return Path.Combine("Assets", relativePath);
		}
		
		public static string ToAbsolutePath(this string relativePath)
		{
			var projectRootPath = Path.GetFullPath(Application.dataPath
				.Substring(0, Application.dataPath.Length - 7));

			var folders = relativePath.Split('/', '\\');
			
			return Path.Combine(projectRootPath, Path.Combine(folders));
		}

		public static string ToFolderPath(this string path)
		{
			var folders = path.Split('/', '\\').ToList();
			
			folders.RemoveAt(folders.Count - 1);

			return Path.Combine(folders.ToArray());
		}
		
		public static string RemoveDigits(this string input)
		{
			return new string(input.Where(c => !char.IsDigit(c)).ToArray());
		}
	}
}