/*
Name: Antonio Luis Rios

Note: Program designed with the SOLID principles of object-oriented programming in mind.
*/

using System;
using System.Linq;
using System.Collections.Generic;


namespace Integer_To_English_Word
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 userNumber = IsValidInput(args);
            StandardDictionaryNumbers numberLibrary = new StandardDictionaryNumbers(userNumber);
            

            /*
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
            */
        }

        // A function which will exit the program and inform the user if there are too many
        // command line arguments or if a non 32-bit signed number is given. The limits are
        // from -2,147,483,648 to 2,147,483,647.
        private static Int32 IsValidInput(string[] arguments)
        {
            if (arguments.Length != 1)
            {
                Console.WriteLine("Please supply only 1 command line argument. Try again.");
                System.Environment.Exit(0);
            }

            if (!Int32.TryParse(arguments[0], out Int32 number))
            {
                Console.WriteLine("Please supply only signed 32-bit integers with or without the negative sign (-) in your command line argument. Try again.");
                System.Environment.Exit(0);
            }

            return number;
        }
    }

    class StandardDictionaryNumbers
    {
        List<string> englishNumber = new List<string>(); 

        Dictionary<int, string> hundredsDictionary = new Dictionary<int, string> {
            {3, "hundred"}, {6, "thousand"}, {9, "million"}, {12, "billion"}
        };

        Dictionary<int, string> tensDictionary = new Dictionary<int, string> {
            {2, "twenty"}, {3, "thirty"}, {4, "forty"}, {5, "fifty"}, 
            {6, "sixty"}, {7, "seventy"}, {8, "eighty"}, {9, "ninety"}
        };

        Dictionary<int, string> numberDictionary = new Dictionary<int, string> {
            {0, "zero"}, {1, "one"}, {2, "two"}, {3, "three"}, {4, "four"}, 
            {5, "five"}, {6, "six"}, {7, "seven"}, {8, "eight"}, {9, "nine"}
        };

        Dictionary<int, string> specialNumberDictionary = new Dictionary<int, string> {
            {10, "ten"}, {11, "eleven"}, {12, "twelve"}, {13, "thirteen"}, {14, "fourteen"}, 
            {15, "fifteen"}, {16, "sixteen"}, {17, "seventeen"}, {18, "eighteen"}, {19, "nineteen"}
        };

        public StandardDictionaryNumbers(Int32 signedNumber)
        {
            string numberStringReversed = new string(signedNumber.ToString().ToCharArray().Reverse().ToArray());
            int sign = Math.Sign(signedNumber);
            int numberLength = sign >= 0 ? signedNumber.ToString().Length : signedNumber.ToString().Length - 1;


            // Formats the reversed number string such that its length becomes a multiple of 3.
            formatNumberLength(ref numberStringReversed, sign, ref numberLength);

            for (int i = 0; i < numberLength; i++)
            {
                if (signedNumber == 0)
                {
                    englishNumber.Add(numberDictionary[0]);

                    break;
                }

                // Every third number, we create the english equivalent of that 
                // portion of the overall number. 
                if ((i + 1) % 3 == 0)
                {
                    Int32 onesDigit = Int32.Parse(numberStringReversed[i - 2].ToString());
                    Int32 tensDigit = Int32.Parse(numberStringReversed[i - 1].ToString());
                    Int32 hundredsDigit = Int32.Parse(numberStringReversed[i].ToString());


                    if (onesDigit != 0 || tensDigit != 0 || hundredsDigit != 0)
                    {
                        addPlaceValue(i);
                    }

                    if (hundredsDigit > 0)
                    {
                        createDoubleDigitNumber(onesDigit, tensDigit);

                        englishNumber.Add(hundredsDictionary[3]);

                        englishNumber.Add(numberDictionary[hundredsDigit]);

                    }
                    else if (hundredsDigit == 0)
                    {
                        createDoubleDigitNumber(onesDigit, tensDigit);
                    }
                }
            }

            isNegative(sign);

            foreach (var item in englishNumber)
            {
                Console.WriteLine(item);
            }
        }

        private void isNegative(int sign)
        {
            if (sign < 0)
            {
                englishNumber.Add("negative ");
            }
        }

        private void createDoubleDigitNumber(int onesDigit, int tensDigit)
        {
            if (tensDigit > 1)
            {
                if (onesDigit > 0)
                {
                    englishNumber.Add(numberDictionary[onesDigit]);
                }

                englishNumber.Add(tensDictionary[tensDigit]);
            }
            else if (tensDigit == 1)
            {
                Int32 num = Int32.Parse(tensDigit.ToString() + onesDigit.ToString());

                englishNumber.Add(specialNumberDictionary[num]);
            }
            else if (tensDigit == 0)
            {
                englishNumber.Add(numberDictionary[onesDigit]);
            }
        }

        private static void formatNumberLength(ref string numberStringReversed, int sign, ref int numberLength)
        {
            while (numberLength % 3 != 0)
            {
                if (sign >= 0)
                {
                    numberStringReversed = numberStringReversed.Insert(numberStringReversed.Length, "0");
                    numberLength = numberStringReversed.Length;
                }
                else if (sign < 0)
                {
                    numberStringReversed = numberStringReversed.Insert(numberStringReversed.Length - 1, "0");
                    numberLength = numberStringReversed.Length - 1;
                }
            }
        }

        private void addPlaceValue(int i)
        {
            if (i != 2)
            {
                englishNumber.Add(hundredsDictionary[i + 1]);
            }
        }
    }

    /*
    class StandardDictionaryNumbers
    {
        
        private string[] placeValue = { 
                                        "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety", "hundred",
                                        "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", 
                                        "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", 
                                        "duodecillion", "tredecillion", "quattuordecillion", "quindecillion", "sexdecillion", "septendecillion", 
                                        "octodecillion", "novemdecillion", "vigintillion", "centillion"
        };
        
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
    */
    
}
