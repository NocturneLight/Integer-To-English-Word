// Name:            Antonio Luis Rios
// Program:         Integer_To_English_Word
// Description:     A program which takes a 32-bit unsigned integer and creates the English
//                  language equivalent to that number.
//
// Note:            Program designed with the SOLID principles of object-oriented programming in 
//                  mind but may not perfectly follow these principles.

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

            // Determine whether the user's number is a negative or positive number 
            // and adjust length accordingly.
            int sign = Math.Sign(userNumber);
            int numberLength = sign >= 0 ? userNumber.ToString().Length : userNumber.ToString().Length - 1;

            // Reverses the user's number and make its length a multiple of 3 in
            // order to convert the number to english.
            NumberFormatter formatter = new NumberFormatter();
            string reverseString = formatter.reverseNumber(userNumber);

            formatter.formatNumberLength(ref reverseString, sign, ref numberLength);

            // Converts the number to english and then gets and displays the result.
            StandardDictionaryNumbers numberLibrary = new StandardDictionaryNumbers(reverseString, sign, numberLength, userNumber);
            
            Console.WriteLine(numberLibrary.getEnglishNumber());
        }
        
        //  Name:           IsValidInput
        //  Purpose:        To check the validity of the user's number.
        //  Description:    Checks whether the user has more than one argument or provided a 
        //                  number outside the range of a 32-bit signed integer and then informs the
        //                  user of the error and exits. If no error, returns the string as a number.
        public static Int32 IsValidInput(string[] arguments)
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
        //  Name:           formatNumberLength
        //  Purpose:        To make the length of the string a multiple of 3 so that the main 
        //                  algorithm for converting a number to english works properly.
        //  Description:    Appends 0's to the end of the string until its length is a multiple of 3. 
        //                  Example: 53251 becomes 532510
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

        //  Name:           reverseNumber
        //  Purpose:        To reverse the number so that it will work with 
        //                  the main algorithm for converting a number to english.
        //  Description:    Reverses the given number. Example: 1000 becomes 0001
        public string reverseNumber(Int32 number)
        {
            return new string(number.ToString().ToCharArray().Reverse().ToArray());
        }
    }

    class StandardDictionaryNumbers
    {
        private List<string> englishNumber = new List<string>(); 

        private Dictionary<int, string> hundredsDictionary = new Dictionary<int, string> {
            {3, "hundred"}, {6, "thousand"}, {9, "million"}, {12, "billion"}
        };

        private Dictionary<int, string> tensDictionary = new Dictionary<int, string> {
            {2, "twenty"}, {3, "thirty"}, {4, "forty"}, {5, "fifty"}, 
            {6, "sixty"}, {7, "seventy"}, {8, "eighty"}, {9, "ninety"}
        };

        private Dictionary<int, string> numberDictionary = new Dictionary<int, string> {
            {0, "zero"}, {1, "one"}, {2, "two"}, {3, "three"}, {4, "four"}, 
            {5, "five"}, {6, "six"}, {7, "seven"}, {8, "eight"}, {9, "nine"}
        };

        private Dictionary<int, string> specialNumberDictionary = new Dictionary<int, string> {
            {10, "ten"}, {11, "eleven"}, {12, "twelve"}, {13, "thirteen"}, {14, "fourteen"}, 
            {15, "fifteen"}, {16, "sixteen"}, {17, "seventeen"}, {18, "eighteen"}, {19, "nineteen"}
        };

        //  Name:           StandardDictionaryNumbers
        //  Purpose:        To convert a given number to its english language equivalent.
        //  Description:    Iterates through the given string and on every third number it 
        //                  takes the three numbers it just iterated through and creates an
        //                  english language equivalent. Example: 123 will add to the englishNumbers
        //                  list "one", "twenty", "hundred", and "three". The following webpage was 
        //                  used as reference for my algorithm: https://www.lavc.edu/math/library/math105/Skillbuilders/m105sb-wholeplacevalue.aspx
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


        //  Name:           getEnglishNumber
        //  Purpose:        To take the list of strings and return a constructed english 
        //                  phrase for the user's number. 
        //  Description:    Reverses the list of english words and then appends each string to the string 
        //                  variable before returning it.
        public string getEnglishNumber()
        {
            string result = "";

            englishNumber.Reverse();

            foreach (var word in englishNumber)
            {
                result += word + " ";
            }

            return result;
        }

        //  Name:           isNegative
        //  Purpose:        Check if the number is negative.
        //  Description:    Checks the sign of the number for positivity or negativity and 
        //                  adds the word "negative" to the list if the sign is providing a 
        //                  negative number.
        private void isNegative(int sign)
        {
            if (sign < 0)
            {
                englishNumber.Add("negative");
            }
        }

        //  Name:           createDoubleDigitNumber
        //  Purpose:        To create the last two digits of a triple digit number.
        //  Description:    Adds the tens place digit to the englishNumber list
        //                  and then adds the ones place digit when not 0. For special
        //                  cases like numbers 10-19, adds the corresponding stored 
        //                  english equivalent.
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
            else if (tensDigit == 0 && onesDigit != 0)
            {
                englishNumber.Add(numberDictionary[onesDigit]);
            }
        }
     
        //  Name:           addPlaceValue
        //  Purpose:        To add the corresponding value after the current triple digit number.
        //  Description:    Checks for if the we are not in the hundreds place and then adds the 
        //                  corresponding place value. Example: If the current number is 5392, then
        //                  the string "thousand" is added after the five.
        private void addPlaceValue(int i)
        {
            if (i != 2)
            {
                englishNumber.Add(hundredsDictionary[i + 1]);
            }
        }
    }    
}
