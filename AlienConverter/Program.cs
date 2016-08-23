using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AlienConverter
{
    class Program
    {
        //Essentially acts as a menu only
        static void Main(string[] args)
        {
            bool exit;
            string pathname;
            List<string> fileContents;
            
            //Setting the initial exit value to false
            exit = false;
            
            //Give the initial introduction to the AlienConverter
            Console.WriteLine("Welcome to this alien converter.");
            
            //Loop the menu until the exit command is given
            do
            {
                Console.Write("Please input the pathname to the text file containing the data to be translated or Q to quit: ");
                pathname = Console.ReadLine();

                if(pathname == "Q" || pathname == "q")
                {
                    exit = true;
                }
                else
                {
                    //Only begin operation if the pathname if valid otherwise theres no point in performing any other operations if not
                    if (File.Exists(pathname))
                    {
                        try
                        {
                            fileContents = readFile(pathname);

                            contentReader(fileContents);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("There was an error reading the file.\nReturning to the menu.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nUnfortunately the pathname you have given does not exist.\nReturning to the menu.\n");
                    }
                  
                }
            } while (exit != true);
        }

        /****************************************************************
         * Name: readFile                                               *
         * Inputs: A string representing the pathname of the text file  *
         *           to be read from.                                   *
         * Output: A list of strings representing the contents of the   *
         *           file that has been read separated by lines.        *
         * Purpose: To read the contents of a text file and store them  *
         *            in a list of strings.                             *
         ****************************************************************/

        private static List<String> readFile(string pathname)
        {
            List<string> fileContents;
            string line;

            fileContents = new List<string>();

            System.IO.StreamReader file = new System.IO.StreamReader(pathname);
            while ((line = file.ReadLine()) != null)
            {
                fileContents.Add(line);
            }

            file.Close();

            return fileContents;
        }

        /****************************************************************
         * Name: contentReader                                          *
         * Inputs: A list of strings representing the contents of a     *
         *           text file that has been read.                      *
         * Output: None                                                 *
         * Purpose: To take the contents of a file that has been read   *
         *            and preform conversion actions based on what is   *
         *            provided.                                         *
         ****************************************************************/

        private static void contentReader(List<string> fileContents)
        {
            Converter alienConverter = new Converter();
            string[] words;
            string alienWords;

            foreach (string line in fileContents)
            {
                words = line.Split(' ');
                
                if (words.Length == 3 && words[1].Equals("is") && words[2].Length == 1)
                {
                    try
                    {
                        alienConverter.addAlienTranslation(((words[2]).ToCharArray())[0], (words[0]));
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("The numeral, " + words[2] + ", is not a valid roman numeral. Please ensure the roman numerals are valid and are capitalized.");
                    }
                }
                else if ((words[0].Equals("How") || words[0].Equals("how")) && words[1].Equals("much") && words[2].Equals("is"))
                {
                    alienWords = "";

                    //Convert an alien language to the arabian credit value
                    for (int ii = 3; ii <= words.Length-1; ii++)
                    {
                        if(!(words[ii].Equals("?")))
                            alienWords += words[ii] + " ";
                    }
                    try
                    {
                        Console.WriteLine(alienWords + "is " + alienConverter.romanToArabic(alienConverter.alienToRoman(alienWords)));
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("The number given cannot be accurately represented with the current numerals.");
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("The alien numerals can not be translated.");
                    }
                }
                else if (words.Length >= 5 && (words[words.Length - 1].Equals("Credits") || words[words.Length - 1].Equals("credits")))
                {
                    //Assumes that each resource is given a numeral representing how much of the resource is given, even if that number is one
                    int resourceValue;

                    alienWords = "";

                    for (int ii = 0; ii < words.Length - 4; ii++)
                    {
                        alienWords = alienWords + words[ii] + " ";
                    }

                    if (Int32.TryParse(words[words.Length - 2], out resourceValue))
                    {
                        try
                        {
                            alienConverter.addOrUpdateResource(words[(words.Length - 4)], resourceValue / alienConverter.romanToArabic(alienConverter.alienToRoman(alienWords)));
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("The roman numerals given are invalid.");

                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("The alien numerals " + alienWords + "can not be translated.");
                        }
                    }
                    else
                    {
                        Console.WriteLine(words[words.Length - 2] + " is not a valid credit amount.");
                    }

                }
                else if (words.Length >= 6 && ((words[0].Equals("How") || words[0].Equals("how")) && words[1].Equals("many") && (words[2].Equals("Credits") || words[2].Equals("credits")) && words[3].Equals("is")))
                {
                    //Assumes that each resource is given a numeral representing how much of the resource is given, even if that number is one
                    //Find credit value
                    int value;
                    alienWords = "";

                    for (int ii = 4; ii < words.Length - 2; ii++)
                    {
                        alienWords = alienWords + words[ii] + " ";
                    }
                    try
                    {
                        value = alienConverter.getResourceValue(words[words.Length - 2]) * alienConverter.romanToArabic(alienConverter.alienToRoman(alienWords));
                        if (value > 0)
                        {
                            Console.WriteLine(alienWords + words[words.Length - 2] + " is " + value + " credits.");
                        }
                        else if (value == 0)
                        {
                            Console.WriteLine(words[words.Length - 2] + " is not a valid resource. Ensure that the name of the resource is correct and that it has been given a value.");
                        }
                        else
                        {
                            Console.WriteLine("There was an error attempting to get the resource value.");
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("The roman numerals could not be translated.");
                    }
                    catch(ArgumentException)
                    {
                        Console.WriteLine("The Alien numerals could not be translated.");
                    }
                }
                else if (words[0].Equals("") && words.Length == 1)
                {
                    //Do nothing in case there are some blank lines in the text file
                }
                else
                {
                    Console.WriteLine("I have no idea what you're talking about");
                }
            }
        }
    }
}