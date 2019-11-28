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
            public static string locationOnDisc = @"C:\Users\jarek.magulski\source\repos\TekstRepo\TextRepo";//COAS
            //public static string locationOnDisc = @"C:\Users\jarek\source\repos\TekstRepo\TextRepo";//HOME

            //Name of the text file that will get processed (for separation of albums / artists etc)
            public static string fileToProcess = "tekst";
        }

        static void Main(string[] args)
        {
            //string fromFile = "";
            //string toFile = "";
            //MergeFiles(fromFile, toFile);

            string text = readText(Globals.fileToProcess);
            SaveRhymes(text, "u", "e");
            //SaveWordOccurence(text, Globals.fileToProcess);
        }

        public static string readText(string inputFileName)
        {
            //Parse new lines into spaces
            string text;
            StringBuilder sb = new StringBuilder();
            string filename =  inputFileName + ".txt";
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

        public static void MergeFiles(string mergeFromFile, string mergeToFile)
        {
            string AggregatedFile = readText(mergeFromFile) + " " + readText(mergeToFile);

            SaveWordOccurence(AggregatedFile, mergeToFile);
        }

        public static void SaveRhymes(string text, string firstLetter, string secondLetter)
        {
            List<string> rhymeList;

            if (firstLetter == "e" && secondLetter == "e")//First letter E
            {
                List<string> list1 = FindRhymes(text, "e", "e");
                List<string> list2 = FindRhymes(text, "ę", "e");
                List<string> list3 = FindRhymes(text, "e", "ę");
                List<string> list4 = FindRhymes(text, "ę", "ę");
                rhymeList = list1.Concat(list2).Concat(list3).Concat(list4).ToList();
            }else if (firstLetter == "e" && secondLetter == "u")
            {
                List<string> list1 = FindRhymes(text, "e", "u");
                List<string> list2 = FindRhymes(text, "ę", "u");
                List<string> list3 = FindRhymes(text, "e", "ó");
                List<string> list4 = FindRhymes(text, "ę", "ó");
                rhymeList = list1.Concat(list2).Concat(list3).Concat(list4).ToList();
            }
            else if (firstLetter == "e" && secondLetter == "o")
            {
                List<string> list1 = FindRhymes(text, "e", "o");
                List<string> list2 = FindRhymes(text, "ę", "o");
                List<string> list3 = FindRhymes(text, "e", "ą");
                List<string> list4 = FindRhymes(text, "ę", "ą");
                rhymeList = list1.Concat(list2).Concat(list3).Concat(list4).ToList();
            }
            else if (firstLetter == "e")
            {
                List<string> list1 = FindRhymes(text, "e", secondLetter);
                List<string> list2 = FindRhymes(text, "ę", secondLetter);
                rhymeList = list1.Concat(list2).ToList();//End e
            }else if (firstLetter == "u" && secondLetter == "u") //First letter u
            {
                List<string> list1 = FindRhymes(text, "u", "u");
                List<string> list2 = FindRhymes(text, "ó", "u");
                List<string> list3 = FindRhymes(text, "u", "ó");
                List<string> list4 = FindRhymes(text, "ó", "ó");
                rhymeList = list1.Concat(list2).Concat(list3).Concat(list4).ToList();
            }
            else if (firstLetter == "u" && secondLetter == "e")
            {
                List<string> list1 = FindRhymes(text, "u", "e");
                List<string> list2 = FindRhymes(text, "ó", "e");
                List<string> list3 = FindRhymes(text, "u", "ę");
                List<string> list4 = FindRhymes(text, "ó", "ę");
                rhymeList = list1.Concat(list2).Concat(list3).Concat(list4).ToList();
            }
            else if (firstLetter == "u" && secondLetter == "o")
            {
                List<string> list1 = FindRhymes(text, "u", "o");
                List<string> list2 = FindRhymes(text, "ó", "o");
                List<string> list3 = FindRhymes(text, "u", "ą");
                List<string> list4 = FindRhymes(text, "ó", "ą");
                rhymeList = list1.Concat(list2).Concat(list3).Concat(list4).ToList();
            }
            else if (firstLetter == "u")
            {
                List<string> list1 = FindRhymes(text, "u", secondLetter);
                List<string> list2 = FindRhymes(text, "ó", secondLetter);
                rhymeList = list1.Concat(list2).ToList();//End u
            }
            else if (firstLetter == "o" && secondLetter == "u") //First letter o
            {
                List<string> list1 = FindRhymes(text, "o", "u");
                List<string> list2 = FindRhymes(text, "ą", "u");
                List<string> list3 = FindRhymes(text, "o", "ó");
                List<string> list4 = FindRhymes(text, "ą", "ó");
                rhymeList = list1.Concat(list2).Concat(list3).Concat(list4).ToList();
            }
            else if (firstLetter == "o" && secondLetter == "e")
            {
                List<string> list1 = FindRhymes(text, "o", "e");
                List<string> list2 = FindRhymes(text, "ą", "e");
                List<string> list3 = FindRhymes(text, "o", "ę");
                List<string> list4 = FindRhymes(text, "ą", "ę");
                rhymeList = list1.Concat(list2).Concat(list3).Concat(list4).ToList();
            }
            else if (firstLetter == "o" && secondLetter == "o")
            {
                List<string> list1 = FindRhymes(text, "o", "o");
                List<string> list2 = FindRhymes(text, "ą", "o");
                List<string> list3 = FindRhymes(text, "o", "ą");
                List<string> list4 = FindRhymes(text, "ą", "ą");
                rhymeList = list1.Concat(list2).Concat(list3).Concat(list4).ToList();
            }
            else if (firstLetter == "o")
            {
                List<string> list1 = FindRhymes(text, "o", secondLetter);
                List<string> list2 = FindRhymes(text, "ą", secondLetter);
                rhymeList = list1.Concat(list2).ToList();//End o
            }
            else if (secondLetter == "o")//Secondletter o
            {
                List<string> list1 = FindRhymes(text, firstLetter, "o");
                List<string> list2 = FindRhymes(text, firstLetter, "ą");
                rhymeList = list1.Concat(list2).ToList();//End o
            }
            else if (secondLetter == "u")//Secondletter u
            {
                List<string> list1 = FindRhymes(text, firstLetter, "u");
                List<string> list2 = FindRhymes(text, firstLetter, "ó");
                rhymeList = list1.Concat(list2).ToList();//End u
            }
            else if (secondLetter == "e")//Secondletter e
            {
                List<string> list1 = FindRhymes(text, firstLetter, "e");
                List<string> list2 = FindRhymes(text, firstLetter, "ę");
                rhymeList = list1.Concat(list2).ToList();//End e
            }
            else
            {
                rhymeList = FindRhymes(text, firstLetter, secondLetter);
            }

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

            //Make new list with just the words. No counts.
            foreach (var word in wordCount)
            {
                allWords.Add(word.Key);
            }

            //For each word in the List, check if it rhymes
            foreach (var word in allWords)
            {
                bool sameTwoLetters = secondLetter == firstLetter;
                bool mayAdd;
                if (sameTwoLetters)
                {
                    mayAdd = CheckIfWordRhymesSameTwoLetters(word, firstLetter);
                }
                else
                {
                    mayAdd = CheckIfWordRhymes(word, secondLetter, firstLetter);
                }
                if (mayAdd)
                {
                    onlyRhymes.Add(word);
                    Console.WriteLine(word);
                }
            }
            return onlyRhymes;
        }

        public static bool CheckIfWordRhymesSameTwoLetters(string word, string letter)
        {
            bool mayAdd = false;
            string letters = RemoveFirstAndSecondLetter(letter);
            char[] charArray = letters.ToCharArray();
            string reversed = ReverseWord(word);
            int smallestUnwantedIndex = reversed.Length - 1; ;
            bool IValidation = ValidateForLetterI(letter, letter, reversed);
            foreach (var item in charArray)
            {
                reversed = ReverseWord(word);
                if (reversed.Contains(item))
                {
                    int index = reversed.IndexOf(item);
                    if (index < smallestUnwantedIndex)
                    {
                        smallestUnwantedIndex = index;
                    }
                }
            }
            //Console.WriteLine("Word: {2}, smallest unwanted Index: {0}. Letter corresponding with this index: {1}", smallestUnwantedIndex, reversed.ElementAt(smallestUnwantedIndex), reversed);
            if (reversed.Contains(letter))
            {
                //Console.WriteLine("reversed contains the letter {0}", letter);
                int desiredLetterIndexOne = reversed.IndexOf(letter);
                reversed = reversed.Remove(desiredLetterIndexOne, 1);
            }
            else { return false; }
            if (reversed.Contains(letter))
            {
                //Console.WriteLine("reversed contains the letter {0}", letter);
                int desiredLetterIndexTwo = reversed.IndexOf(letter) + 1;
                //Console.WriteLine("desiredLetterIndexTwo: {0}, smallestUnwantedIndex: {1}, IValidation: {2}", desiredLetterIndexTwo, smallestUnwantedIndex, IValidation.ToString());
                if ((desiredLetterIndexTwo < smallestUnwantedIndex) && IValidation)
                {
                    mayAdd = true;
                }
            }
            else { return false; }
            return mayAdd;
        }
        public static bool CheckIfWordRhymes(string word, string secondLetter, string firstLetter)
        {
            bool mayAdd = true;
            string letters = RemoveFirstAndSecondLetter(firstLetter, secondLetter);
            char[] charArray = letters.ToCharArray();
            string reversed = ReverseWord(word);
            if (reversed.Contains(secondLetter) && reversed.Contains(firstLetter) && (reversed.IndexOf(secondLetter) < reversed.IndexOf(firstLetter)))
            {
                mayAdd = ValidateForLetterI(firstLetter, secondLetter, reversed);
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
            }else { mayAdd = false; }
            return mayAdd;
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
            string allForbidden = "aąeęoóuy";
            char[] charArray = allForbidden.ToCharArray();
            allForbidden = allForbidden.Remove(allForbidden.IndexOf(firstLetter), 1);
            allForbidden = allForbidden.Remove(allForbidden.IndexOf(secondLetter), 1);
            return allForbidden;
        }

        public static string RemoveFirstAndSecondLetter(string firstLetter)
        {
            string allForbidden = "aąeęoóuy";
            char[] charArray = allForbidden.ToCharArray();
            allForbidden = allForbidden.Remove(allForbidden.IndexOf(firstLetter), 1);
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

        public static void SaveWordOccurence(string text, string outputFile)
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
            string filename = outputFile + " word count.txt";
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
