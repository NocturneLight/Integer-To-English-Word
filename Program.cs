/*
Name: Antonio Luis Rios

Note: Program designed with the SOLID principles of object-oriented programming in mind.
*/

using System;
using System.Collections.Generic;


namespace Integer_To_English_Word
{
    class Program
    {
        static void Main(string[] args)
        {
            if (IsValidInput(args))
            {
                string number = args[0];
                List<string> englishNumber = new List<String>();
                StandardDictionaryNumbers numberLibrary = new StandardDictionaryNumbers();


                for (int i = 0; i < number.Length; i++)
                {
                    char digit = number[i];

                    if (digit.Equals('-'))
                    {
                        englishNumber.Add("negative ");

                        numberLibrary.isNegative = true;
                        
                        continue;
                    }
                    else
                    {
                        string numberFragment = numberLibrary.numberToEnglish(i, number.Length, digit);

                        if (!numberFragment.Equals(""))
                        {
                            englishNumber.Add(numberFragment + " ");   
                        }
                    }
                }                

                foreach (var item in englishNumber)
                {
                    Console.Write(item);
                }
            }
        }

        // A function which will exit the program and inform the user if there are too many
        // command line arguments or if a non-number is given. 
        private static bool IsValidInput(string[] arguments)
        {
            if (arguments.Length != 1)
            {
                Console.WriteLine("Please supply only 1 command line argument. Try again.");
                System.Environment.Exit(0);
            }

            for (int i = 0; i < arguments[0].Length; i++)
            {
                char chara = arguments[0][i];

                if (i == 0 && (!Char.Equals(chara, '-') && !Char.IsNumber(chara)))
                {
                    Console.WriteLine("Please supply only integers (0-9) with or without the negative sign (-) in your command line argument. Try again.");
                    System.Environment.Exit(0);
                }
                else if (i != 0 && !Char.IsNumber(chara))
                {
                    Console.WriteLine("Please supply only integers (0-9) with or without the negative sign (-) in your command line argument. Try again.");
                    System.Environment.Exit(0);
                }
            }

            return true;
        }
    }

    class StandardDictionaryNumbers
    {
        /*
        private string[] placeValue = { 
                                        "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety", "hundred",
                                        "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", 
                                        "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", 
                                        "duodecillion", "tredecillion", "quattuordecillion", "quindecillion", "sexdecillion", "septendecillion", 
                                        "octodecillion", "novemdecillion", "vigintillion", "centillion"
        };
        */
        char tensPlaceValue = ' ';
        public bool isNegative = false;

        private string[] placeValue = {"thousand", "hundred", "tens", "ones"};

        
        private Dictionary<string, string> NumberEnglishEquivalencies = new Dictionary<string, string> {
            {"0", "zero"}, {"1","one"}, {"2", "two"}, {"3", "three"}, {"4", "four"}, {"5", "five"}, {"6", "six"}, {"7", "seven"}, {"8", "eight"}, {"9", "nine"},
            {"10", "ten"}, {"11", "eleven"}, {"12", "twelve"}, {"13", "thirteen"}, {"14", "fourteen"}, {"15", "fifteen"}, {"16", "sixteen"}, {"17", "seventeen"},
            {"18", "eighteen"}, {"19", "nineteen"}, {"20", "twenty"}
        };

        private Dictionary<string, string> TensPlaceEquivalencies = new Dictionary<string, string> {
            {"2", "twenty"}, {"3", "thirty"}, {"4", "forty"}, {"5", "fifty"}, {"6", "sixty"}, {"7", "seventy"}, {"8", "eighty"}, {"9", "ninety"}
        };


        // A function which converts a digit to the appropriate english
        // form depending on its place in the number. Example: "10" becomes "ten"
        public string numberToEnglish(int digitPlace, int size, char number)
        {
            // Finds the place in relation to the size of the number.
            int place = digitPlace + (placeValue.Length - size);


            if (size > 1 && !number.Equals('0'))
            {
                // If in the tens place, store the value in that spot for later if 1.
                // Otherwise, return a string fragment of the corresponding naming convention.
                // Example: "2" will return "twenty".   
                if (placeValue[place] == "tens")
                {
                    if (number.Equals('1'))
                    {
                        tensPlaceValue = number;
                    }
                    else
                    {
                        return TensPlaceEquivalencies[number.ToString()];
                    }
                }
                // If in the ones place, return a string fragment of the corresponding number between 10-19 by 
                // combining the tens and ones place numbers. Otherwise, return a string fragment of the corresponding ones place value.
                // Example: "15" will return "fifteen" and "5" will return "five".
                else if (placeValue[place] == "ones")
                {
                    if (tensPlaceValue.Equals('1'))
                    {
                        return NumberEnglishEquivalencies[tensPlaceValue.ToString() + number.ToString()];
                    }
                    else
                    {
                        return NumberEnglishEquivalencies[number.ToString()] + " ";
                    }
                }
                // Returns the corresponding number naming conventions for the hundreds and thousands place.
                else
                {
                    return NumberEnglishEquivalencies[number.ToString()] + " " + placeValue[place];
                }
            }
            // Special case for catching single digit numbers or single digit negative numbers.
            else if (size == 1 || (size == 2 && isNegative))
            {
                return NumberEnglishEquivalencies[number.ToString()];
            }
            // Special case for catching the number 10.
            else if (tensPlaceValue.Equals('1'))
            {
                return NumberEnglishEquivalencies[tensPlaceValue.ToString() + number.ToString()];
            }

            return "";
        }
    }
}
