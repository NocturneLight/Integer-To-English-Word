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
            // If the number is not a signed 32 bit integer, or if 
            // the user has provided too many arguments, inform them
            // and exit the program.
            Int32 userNumber = IsValidInput(args);

            // Determine whether the user's number is a negative or postive number 
            // and adjust length accordingly.
            int sign = Math.Sign(userNumber);
            int numberLength = sign >= 0 ? userNumber.ToString().Length : userNumber.ToString().Length - 1;

            // Reverses the user's number and make its length a multiple of 3 in
            // order to convert the number to english.
            NumberFormatter formatter = new NumberFormatter();
            string reverseString = formatter.reverseNumber(userNumber);

            formatter.formatNumberLength(ref reverseString, sign, ref numberLength);

            // Converts the number to english and displays the result.
            StandardDictionaryNumbers numberLibrary = new StandardDictionaryNumbers(reverseString, sign, numberLength, userNumber);
            
            numberLibrary.displayEnglishNumber();
        }
        
        // A function which will exit the program and inform the user if there are too many
        // command line arguments or if a non 32-bit signed number is given. The limits are
        // from -2,147,483,648 to 2,147,483,647.
        
        //  Name:
        //  Purpose:
        //  Description:
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

    class NumberFormatter
    {
        //  Name:
        //  Purpose:
        //  Description:
        public void formatNumberLength(ref string numberStringReversed, int sign, ref int numberLength)
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

        //  Name:
        //  Purpose:
        //  Description:
        public string reverseNumber(Int32 number)
        {
            return new string(number.ToString().ToCharArray().Reverse().ToArray());
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

        //  Name:
        //  Purpose:
        //  Description:
        public StandardDictionaryNumbers(string number, int sign, int length, Int32 numberInt)
        {
            for (int i = 0; i < length; i++)
            {
                if (numberInt == 0)
                {
                    englishNumber.Add(numberDictionary[0]);
                    break;
                }

                // Every third number, we create the english equivalent of that 
                // portion of the overall number. 
                if ((i + 1) % 3 == 0)
                {
                    Int32 onesDigit = Int32.Parse(number[i - 2].ToString());
                    Int32 tensDigit = Int32.Parse(number[i - 1].ToString());
                    Int32 hundredsDigit = Int32.Parse(number[i].ToString());


                    if (onesDigit != 0 || tensDigit != 0 || hundredsDigit != 0)
                    {
                        addPlaceValue(i);
                    }

                    createDoubleDigitNumber(onesDigit, tensDigit);

                    if (hundredsDigit > 0)
                    {
                        englishNumber.Add(hundredsDictionary[3]);
                        englishNumber.Add(numberDictionary[hundredsDigit]);
                    }
                }
            }

            // Apply the negative sign if we have a negative number.
            isNegative(sign);
        }

        //  Name:
        //  Purpose:
        //  Description:
        public void displayEnglishNumber()
        {
            englishNumber.Reverse();

            foreach (var word in englishNumber)
            {
                Console.Write(word + " ");
            }
        }

        //  Name:
        //  Purpose:
        //  Description:
        private void isNegative(int sign)
        {
            if (sign < 0)
            {
                englishNumber.Add("negative");
            }
        }

        //  Name:
        //  Purpose:
        //  Description:
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
     
        //  Name:
        //  Purpose:
        //  Description:
        private void addPlaceValue(int i)
        {
            if (i != 2)
            {
                englishNumber.Add(hundredsDictionary[i + 1]);
            }
        }
    }    
}
