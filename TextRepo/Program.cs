using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextRepo
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = readText();
            CheckWordOccurence(text);
        }

        public static string readText()
        {
            //Parse new lines into spaces
            string text;
            StringBuilder sb = new StringBuilder();
            //Change file PATH*********************************************************************************************************
            foreach (string line in File.ReadLines(@"C:\Users\jarek.magulski\source\repos\TextRepo\TextRepo\tekst.txt", Encoding.UTF8))
            {
                sb.Append(line + " ");
            }
            text = sb.ToString();

            //Remove special characters including dots and commas, but not spaces.
            text = Helper.RemoveSpecialCharacters(text);
            return text;
        }

        public static void CheckWordOccurence(string text)
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            List<string> words = new List<string>();
            words = text.Split(" ").ToList();
            foreach (var word in words)
            {
                try
                {
                    wordCount[word] = wordCount[word] + 1;
                }
                catch (KeyNotFoundException)
                {
                    wordCount.Add(word, 1);
                }
            }
            //Remove the unintentional empty string from counted words
            wordCount.Remove("");

            //Print every word with it's count
            foreach (var word in wordCount)
            {
                Console.WriteLine(word.Key + ": " + word.Value);
            }

            Console.WriteLine("\nDone!");
            Console.ReadLine();
        }

        public static void FindRhyme(string text)
        {
            //To Be Continued
        }
    }
    static class Helper
    {
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (c == ',' || c == '<' || c == '.' || c == '>' || c == '/' || c == '?' || c == ';' || c == ':' || c == '\'' || c == '\"' || c == '|' || c == '\\' || c == '[' || c == '{' || c == ']' || c == '}' || c == '-' || c == '_' || c == '=' || c == '+' || c == '`')
                {
                    
                }
                else
                {
                    sb.Append(c);
                }
            }
            
            string result = sb.ToString().ToLower().Replace("\t", " ");
            return result;
        }
    }
}
