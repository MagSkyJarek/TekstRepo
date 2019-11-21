using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextRepo
{
    class Program
    {
        public static class Globals
        {
            //CHANGE DESTINATION LOCATION ON DISC**************************************************************************************
            //COAS LAPTOP DESTINATION:
            public static string locationOnDisc = @"C:\Users\jarek.magulski\source\repos\TekstRepo\TextRepo";
            //MY LAPTOP DESTINATION:
            //public static string locationOnDisc = @"C:\Users\jarek\source\repos\TekstRepo\TextRepo";
            //Name of the text file that will get processed (for separation of albums / artists etc)
            public static string fileToProcess = "tekst";
        }

        static void Main(string[] args)
        {
            string text = readText();
            SaveRhymes(text, "a", "e");
            //SaveWordOccurence(text);
        }

        public static string readText()
        {
            //Parse new lines into spaces
            string text;
            StringBuilder sb = new StringBuilder();
            string filename = Globals.fileToProcess + ".txt";
            string fullPath = Path.Combine(Globals.locationOnDisc, filename);
            foreach (string line in File.ReadLines(fullPath, Encoding.UTF8))
            {
                sb.Append(line + " ");
            }
            text = sb.ToString();

            //Remove special characters including dots and commas, but not spaces.
            text = Helper.RemoveSpecialCharacters(text);
            return text;
        }

        public static void SaveRhymes(string text, string firstLetter, string secondLetter)
        {
            List<string> rhymeList = FindRhymes(text, firstLetter, secondLetter);

            //Save list with words including wordcount in a file
            string filename = Globals.fileToProcess + " rhymes with " + firstLetter + " " + secondLetter + ".txt";
            string fullPath = Path.Combine(Globals.locationOnDisc, filename);
            Console.ReadKey();
            File.WriteAllLines(fullPath, rhymeList);
        }
        
        public static List<string> FindRhymes(string text, string firstLetter, string secondLetter)
        {
            //Get Dictionary of words and counts, so no double words will occur
            Dictionary<string, int> wordCount = CheckWordOccurence(text);
            List<string> allWords = new List<string>();
            List<string> onlyRhymes = new List<string>();
            string letters = RemoveFirstAndSecondLetter(firstLetter, secondLetter);
            char[] charArray = letters.ToCharArray();

            //Make new list with just the words. No counts.
            foreach (var word in wordCount)
            {
                allWords.Add(word.Key);
            }

            //For each word in the List, check if it rhymes
            foreach (var word in allWords)
            {
                string reversed = ReverseWord(word);
                if (reversed.Contains(secondLetter) && reversed.Contains(firstLetter) && (reversed.IndexOf(secondLetter) < reversed.IndexOf(firstLetter)))
                {
                    bool mayAdd = ValidateForLetterI(firstLetter, secondLetter, reversed);
                    if (mayAdd)
                    {
                        foreach (var item in charArray)
                        {
                            mayAdd = ValidateCharIndex(item, reversed, firstLetter);
                            if (mayAdd == false)
                            {
                                break;
                            }
                        }
                    }
                    if (mayAdd)
                    {
                        onlyRhymes.Add(word);
                        Console.WriteLine(word);
                    }
                }
            }
            return onlyRhymes;
        }

        public static bool ValidateForLetterI(string firstLetter, string secondLetter, string reversed)
        {
            bool mayAdd = true;
            if (reversed.Contains('i'))
            {
                if ((reversed.IndexOf('i') - 1) == reversed.IndexOf(firstLetter) || (reversed.IndexOf('i') - 1) == reversed.IndexOf(secondLetter))
                {
                    mayAdd = true;
                }
                else 
                {
                    mayAdd = false;
                }
            }
            return mayAdd;
        }

        public static bool ValidateCharIndex(char letter, string reversed, string firstLetter)
        {
            bool mayAdd = true;
            if (reversed.Contains(letter))
            {
                if (reversed.IndexOf(letter) < reversed.IndexOf(firstLetter))
                {
                    mayAdd = false;
                }
            }
            return mayAdd;
        }

        public static string RemoveFirstAndSecondLetter(string firstLetter, string secondLetter)
        {
            string allForbidden = "aeouy";
            char[] charArray = allForbidden.ToCharArray();
            allForbidden = allForbidden.Remove(allForbidden.IndexOf(firstLetter), 1);
            allForbidden = allForbidden.Remove(allForbidden.IndexOf(secondLetter), 1);
            return allForbidden;
        }

        public static string ReverseWord(string word)
        {
            char[] charArray = word.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static Dictionary<string, int> CheckWordOccurence(string text)
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

            return wordCount;
        }

        public static void SaveWordOccurence(string text)
        {
            //Fill the list with the words and occ
            Dictionary<string, int> wordCount = CheckWordOccurence(text);
            List<string> wordListWithCount = new List<string>();

            //Print every word with it's count in console
            foreach (var word in wordCount)
            {
                Console.WriteLine(word.Key + ": " + word.Value);

            }
            //Convert the dictionary to a sorted list
            var myList = wordCount.ToList();
            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            foreach (var word in myList)
            {
                wordListWithCount.Add(word.Key + " : " + word.Value);
            }
            wordListWithCount.Reverse();

            //Finished
            Console.WriteLine("\nDone!");
            Console.ReadLine();
            //Save list with words including wordcount in a file
            string filename = Globals.fileToProcess + " word count.txt";
            string fullPath = Path.Combine(Globals.locationOnDisc, filename);
            File.WriteAllLines(fullPath, wordListWithCount);
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
