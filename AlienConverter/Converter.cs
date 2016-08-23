using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlienConverter
{
    /****************************************************************
     * Class Name: Converter                                        *
     * Purpose: Provides functions to convert alien, roman, and     *
     *            arabic numerals.                                  *
     ****************************************************************/

    class Converter
    {
        private Dictionary<char, string> numerals;
        private Dictionary<string, int> resources;

        /****************************************************************
         * Name: Converter                                              *
         * Inputs: None                                                 *
         * Output: None                                                 *
         * Purpose: The class constructor used to instantiate an object *
         *             instance. Initializes the dictionarys, numerals  *
         *             and resources, for use later in the class.       *
         ****************************************************************/

        public Converter()
        {
            //Create a dictionary of roman numerals without currently known translation
            numerals = new Dictionary<char, string>()
            {
                {'I', null},
                {'V', null},
                {'X', null},
                {'L', null},
                {'C', null},
                {'D', null},
                {'M', null}
            };
            resources = new Dictionary<string, int>();
        }

        /****************************************************************
         * Name: addAlienTranslation                                    *
         * Inputs: A character representing a roman numeral and a string*
         *           representing an alien word which corresponds to a  *
         *           roman numeral.                                     *
         * Output: None                                                 *
         * Purpose: To take a roman numeral and assign it a given alien *
         *            translation if the roman numeral given is valid.  *
         ****************************************************************/

        public void addAlienTranslation(char romanNumeral, string alienTranslation)
        {
            if (numerals.ContainsKey(romanNumeral))
                numerals[romanNumeral] = alienTranslation;
            else
                throw new InvalidOperationException("The Roman numeral, " + romanNumeral + ", does not exist.");
        }

        /****************************************************************
         * Name: addOrUpdateResource                                    *
         * Inputs: A string representing the name of a resource and an  *
         *           integer representing the value of a resource in    *
         *           alien credits.                                     *
         * Output: None                                                 *
         * Purpose: To find if a given resource has been assigned a     *
         *            credit value, if so update the value and if not   *
         *            add the resource and its value to the resource    *
         *            dictionary.                                       *
         ****************************************************************/

        public void addOrUpdateResource(string resource, int credits)
        {
            if (!resources.ContainsKey(resource))
            {
                resources.Add(resource, credits);
            }
            else
            {
                resources[resource] = credits;
            }
        }

        /****************************************************************
         * Name: getResourceValue                                       *
         * Input: A string representing the name of a resource          *
         * Purpose: To find if a resource exists in the resource        *
         *            dictionary and if so return the credit value of   *
         *            the given resource.                               *
         ****************************************************************/

        public int getResourceValue(string resource)
        {
            return resources.ContainsKey(resource) ? resources[resource] : 0;
        }

        /****************************************************************
         * Name: romanToArabic                                          *
         * Inputs: A string representing a set of Roman numerals        *
         * Output: An integer representing the Arabic equivalent of the *
         *           given set of Roman numerals                        *
         * Purpose: To take a set of Roman numerals and calculate the   *
         *            equivalent arabic number. It also ensures that the*
         *            Roman numeral given does not break the rules of   *
         *            roman numerals such as having more than three of  *
         *            one numeral in a row.                             *
         ****************************************************************/

        public int romanToArabic(string romanNumerals)
        {
            //Converts a Roman string to an Arabic intger
            int arabicNumber = 0;

            for(int ii = 0; ii < romanNumerals.Length; ii++)
            {
                switch (romanNumerals[ii])
                {
                    case 'M':
                        if(ii < 3)
                        {
                            arabicNumber += 1000;
                        }
                        else if((romanNumerals[ii - 1] != 'M') || (romanNumerals[ii - 2] != 'M') || (romanNumerals[ii - 3] != 'M'))
                        {
                            arabicNumber += 1000;
                        }
                        else
                        {
                            //Three or more letters in a row, invalid
                            throw new ArgumentException("Three or more of the same letter in a row is invalid.");
                        }
                        break;
                    case 'D':
                        if (ii < 3)
                        {
                            arabicNumber += 500;
                        }
                        else if ((romanNumerals[ii - 1] != 'D') || (romanNumerals[ii - 2] != 'D') || (romanNumerals[ii - 3] != 'D'))
                        {
                            arabicNumber += 500;
                        }
                        else
                        {
                            //Three or more letters in a row, invalid
                            throw new ArgumentException("Three or more of the same letter in a row is invalid.");
                        }
                        break;
                    case 'C':
                        if (ii < 3)
                        {
                            if (ii != romanNumerals.Length - 1)
                            {
                                if (romanNumerals[ii + 1] == 'M' || romanNumerals[ii + 1] == 'D')
                                {
                                    arabicNumber -= 100;
                                }
                                else
                                {
                                    arabicNumber += 100;
                                }
                            }
                            else
                            {
                                arabicNumber += 100;
                            }
                        }
                        else if ((romanNumerals[ii - 1] != 'C') || (romanNumerals[ii - 2] != 'C') || (romanNumerals[ii - 3] != 'C'))
                        {
                            if (ii != romanNumerals.Length - 1)
                            {
                                if (romanNumerals[ii + 1] == 'M' || romanNumerals[ii + 1] == 'D')
                                {
                                    arabicNumber -= 100;
                                }
                                else
                                {
                                    arabicNumber += 100;
                                }
                            }
                            else
                            {
                                arabicNumber += 100;
                            }
                        }
                        else
                        {
                            //Three or more letters in a row, invalid
                            throw new ArgumentException("Three or more of the same letter in a row is invalid.");
                        }
                        break;
                    case 'L':
                        if (ii < 3)
                        {
                            arabicNumber += 50;
                        }
                        else if ((romanNumerals[ii - 1] != 'L') || (romanNumerals[ii - 2] != 'L') || (romanNumerals[ii - 3] != 'L'))
                        {
                            arabicNumber += 50;
                        }
                        else
                        {
                            //Three or more letters in a row, invalid
                            throw new ArgumentException("Three or more of the same letter in a row is invalid.");
                        }
                        break;
                    case 'X':
                        if (ii < 3)
                        {
                            if (ii != romanNumerals.Length - 1)
                            {
                                if (romanNumerals[ii + 1] == 'M' || romanNumerals[ii + 1] == 'D')
                                {
                                    //invalid, throw exception
                                    throw new ArgumentOutOfRangeException("Invalid numeral");
                                }
                                else if (romanNumerals[ii + 1] == 'L' || romanNumerals[ii + 1] == 'C')
                                {
                                    arabicNumber -= 10;
                                }
                                else
                                {
                                    arabicNumber += 10;
                                }
                            }
                            else
                            {
                                arabicNumber += 10;
                            }
                        }
                        else if ((romanNumerals[ii - 1] != 'X') || (romanNumerals[ii - 2] != 'X') || (romanNumerals[ii - 3] != 'X'))
                        {
                            if (ii != romanNumerals.Length - 1)
                            {
                                if (romanNumerals[ii + 1] == 'M' || romanNumerals[ii + 1] == 'D')
                                {
                                    //invalid, throw exception
                                    throw new ArgumentOutOfRangeException("Invalid numeral");
                                }
                                else if (romanNumerals[ii + 1] == 'L' || romanNumerals[ii + 1] == 'C')
                                {
                                    arabicNumber -= 10;
                                }
                            }
                            else
                            {
                                arabicNumber += 10;
                            }
                        }
                        else
                        {
                            //Three or more letters in a row, invalid
                            throw new ArgumentException("Three or more of the same letter in a row is invalid.");
                        }
                        break;
                    case 'V':
                        if (ii < 3)
                        {
                            arabicNumber += 5;
                        }
                        else if ((romanNumerals[ii - 1] != 'V') || (romanNumerals[ii - 2] != 'V') || (romanNumerals[ii - 3] != 'V'))
                        {
                            arabicNumber += 5;
                        }
                        else
                        {
                            //Three or more letters in a row, invalid
                            throw new ArgumentException("Three or more of the same letter in a row is invalid.");
                        }
                        break;
                    case 'I':
                        if (ii < 3)
                        {
                            if (ii != romanNumerals.Length - 1)
                            {
                                if (romanNumerals[ii + 1] == 'X' || romanNumerals[ii + 1] == 'V')
                                {
                                    arabicNumber -= 1;
                                }
                                else if (romanNumerals[ii + 1] == 'I')
                                {
                                    arabicNumber += 1;
                                }
                                else
                                {
                                    //Invalid, throw exception?
                                    throw new ArgumentOutOfRangeException("Invalid Numeral");
                                }
                            }
                            else
                            {
                                arabicNumber += 1;
                            }
                        }
                        else if ((romanNumerals[ii - 1] != 'I') || (romanNumerals[ii - 2] != 'I') || (romanNumerals[ii - 3] != 'I'))
                        {
                            if (ii != romanNumerals.Length - 1)
                            {
                                if (romanNumerals[ii + 1] == 'X' || romanNumerals[ii + 1] == 'V')
                                {
                                    arabicNumber -= 1;
                                }
                                else if (romanNumerals[ii + 1] == 'I')
                                {
                                    arabicNumber += 1;
                                }
                                else
                                {
                                    //Invalid, throw exception?
                                    throw new ArgumentOutOfRangeException("Invalid Numeral");
                                }
                            }
                            else
                            {
                                arabicNumber += 1;
                            }
                        }
                        else
                        {
                            //Three or more letters in a row, invalid
                            throw new ArgumentException("Three or more of the same letter in a row is invalid.");
                        }
                        break;
                    default:
                        //invalid letter, throw exception
                        throw new ArgumentOutOfRangeException("Incorrect Numeral given.");
                }
            }

            return arabicNumber;
        }

        /****************************************************************
         * Name: romanToAlien                                           *
         * Inputs: A string representing a set of roman numerals        *
         * Output: A string representing the alien equivalent of the    *
         *           given set of roman numerals                        *
         * Purpose: To take a set of roman numerals and for each one    *
         *            check if it has been assigned a translation in the*
         *            numerals dictionary and if so add the value to a  *
         *            string.                                           *
         * Notes: This function is not currently necessary based on the *
         *          specifications of the program but is available for  *
         *          further development if desired.                     *
         ****************************************************************/

        public string romanToAlien(string romanNumerals)
        {
            //Convert roman numerals to alien numerals
            string alienNumerals;
            alienNumerals = "";

            foreach (char numeral in romanNumerals)
            {
                if (numerals.ContainsKey(numeral) && numerals[numeral] != null)
                {
                    alienNumerals = alienNumerals + numerals[numeral] + " ";
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            return alienNumerals;
        }

        /****************************************************************
         * Name: arabicToRoman                                          *
         * Inputs: An integer representing a given arabic number        *
         * Output: A string representing the equivalent set of roman    *
         *           numerals for the given arabic number               *
         * Purpose: Take an arabic number and calculate the equivalent  *
         *            set of Roman numerals. It loops until the arabic  *
         *            is no longer within a valid range (<=0) and adds  *
         *            characters to the set of roman numerals based on  *
         *            the current value before subtracting              *
         *            the associated value.                             *
         * Notes: Because we are only dealing with Roman numerals from  *
         *          I to M the maximum possible value is 3999 and the   *
         *          minimum is 1. If it is not within this scope an     *
         *          exception is thrown.                                *
         *        This could also be done with recursion by calling the *
         *          arabicToRoman method within each if statement.      *
         ****************************************************************/

        public string arabicToRoman(int arabicNumber)
        {
            //Convert Arabic integer to a string of Roman numbers.
            string romanNumerals;

            romanNumerals = "";

            
            //Since we are only dealing with letters up to M the maximum is 3999 and the minimum is 0
            if ((arabicNumber <= 3999) && (arabicNumber >= 0))
            {
                while (arabicNumber > 0)
                {
                    if (arabicNumber >= 1000)
                    {
                        romanNumerals = romanNumerals + "M";
                        arabicNumber -= 1000;
                    }
                    else if (arabicNumber >= 900)
                    {
                        romanNumerals = romanNumerals + "CM";
                        arabicNumber -= 900;
                    }
                    else if (arabicNumber >= 500)
                    {
                        romanNumerals = romanNumerals + "D";
                        arabicNumber -= 500;
                    }
                    else if (arabicNumber >= 400)
                    {
                        romanNumerals = romanNumerals + "CD";
                        arabicNumber -= 400;
                    }
                    else if (arabicNumber >= 100)
                    {
                        romanNumerals = romanNumerals + "C";
                        arabicNumber -= 100;
                    }
                    else if (arabicNumber >= 90)
                    {
                        romanNumerals = romanNumerals + "XC";
                        arabicNumber -= 90;
                    }
                    else if (arabicNumber >= 50)
                    {
                        romanNumerals = romanNumerals + "L";
                        arabicNumber -= 50;
                    }
                    else if (arabicNumber >= 40)
                    {
                        romanNumerals = romanNumerals + "XL";
                        arabicNumber -= 40;
                    }
                    else if (arabicNumber >= 10)
                    {
                        romanNumerals = romanNumerals + "X";
                        arabicNumber -= 10;
                    }
                    else if (arabicNumber >= 9)
                    {
                        romanNumerals = romanNumerals + "IX";
                        arabicNumber -= 9;
                    }
                    else if (arabicNumber >= 5)
                    {
                        romanNumerals = romanNumerals + "V";
                        arabicNumber -= 5;
                    }
                    else if (arabicNumber >= 4)
                    {
                        romanNumerals = romanNumerals + "IV";
                        arabicNumber -= 4;
                    }
                    else if (arabicNumber >= 1)
                    {
                        romanNumerals = romanNumerals + "I";
                        arabicNumber -= 1;
                    }
                }
            }
            else
                throw new ArgumentOutOfRangeException("out of range.");

            return romanNumerals;
        }

        /****************************************************************
         * Name: alienToRoman                                           *
         * Inputs: A string representing a set of alien numerals        *
         * Output: A string representing the the set of Roman numerals  *
         *           which are equivalent to the set of given alien     *
         *           numerals.                                          *
         * Purpose: To take a set of alien numerals and for each one    *
         *            check if it has a translation to a roman numeral  *
         *            in the numerals dictionary. If so concatenate that*
         *            roman numeral to a string of roman numerals.      *
         * Notes: This function assumes that each alien word is unique  *
         *          in the numerals dictionary. If this is not the case *
         *          it may produce an incorrect value.                  *
         ****************************************************************/

        public string alienToRoman(string alienNumerals)
        {
            //Convert the alien numerals into roman numerals
            string romanNumeral = "";
            string[] alienNumeral = alienNumerals.Split(' ');

            foreach (string alien in alienNumeral)
            {
                foreach (KeyValuePair<char, string> numeral in numerals)
                {
                    if (numeral.Value != null && numeral.Value.Equals(alien))
                    {
                        romanNumeral += numeral.Key;
                    }
                }
                if(romanNumeral.Equals(""))
                {
                    throw new ArgumentException("The alien word has no accurate translation");
                }
            }

            return romanNumeral;
        }
    }
}