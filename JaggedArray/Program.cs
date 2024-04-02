using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JaggedArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //The program will use the English Alphabet, meaning words that start with 
            //diacritics (ex. început, țânțar, șapte) or numbers will not be recognized and classified.
            //A sample text file is provided.


            string filePath = "WordsAndSentences.txt";  //The file path for the file will be reading from

            //In case of sentences the main file separator might be a simple space,
            //but in case of a random list of words, another separator can be entered.
            char separator;
            Console.Write("Enter the main file separator: ");
            separator = Console.ReadLine()[0];
            

            
            //Jagged array initialization
            string[][] jaggedArray = new string[26][];
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                jaggedArray[i] = new string[0];
            }

            try
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    string line;
                    while((line = streamReader.ReadLine()) != null)
                    {
                        string[] words = line.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);   //Empty substrings are omitted from the resulting array
                        foreach (string word in words) 
                        { 
                            if(!string.IsNullOrEmpty(word))
                            {
                                char firstLetter = char.ToLower(word[0]);                       // Takes the first letter of the word and converts it to lowercase
                                if (firstLetter >= 'a' && firstLetter <= 'z')                         //Checks for words that don't start with letters
                                {
                                    int index = firstLetter - 'a';                                        // Finds the index in the jagged array by substracting the letter 'a'
                                    Array.Resize(ref jaggedArray[index], jaggedArray[index].Length + 1);  // Adds space for one more element at the index location
                                    jaggedArray[index][jaggedArray[index].Length - 1] = word;             // Adds the word at that location
                                }
                            }
                        }
                    }
                }

                //Jagged array display
                for (int i = 0; i < jaggedArray.Length; i++)
                {
                    Console.WriteLine($"{char.ToUpper((char)(i + 'a'))}:");
                    foreach (string word in jaggedArray[i])
                    {
                        Console.WriteLine($"\t{word}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error at file reading: {ex.Message}");
            }
                                                                         

            Console.ReadKey();

        }
    }
}
