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
            char[] numberReversed = signedNumber.ToString().ToCharArray().Reverse().ToArray();
            int isNegative = Math.Sign(signedNumber);
            int numberLength = isNegative >= 0 ? signedNumber.ToString().Length : signedNumber.ToString().Length - 1;


            // Stores the english equvalent of a one digit number.
            if (numberLength == 1)
            {
                for (int i = 0; i < signedNumber.ToString().Length; i++)
                {
                    if (numberReversed[i].Equals('-'))
                    {
                        englishNumber.Add("negative ");

                        continue;
                    }

                    englishNumber.Add(oneDigitNumberToEnglish(Int32.Parse(numberReversed[i].ToString())) + " ");
                }

                foreach (var item in englishNumber)
                {
                    Console.WriteLine(item);
                }
            }
            // Stores the english equivalent of a two digit number.
            else if (numberLength == 2)
            {
                for (int i = 0; i < signedNumber.ToString().Length; i++)
                {
                    if (numberReversed[i].Equals('-'))
                    {
                        englishNumber.Add("negative ");

                        continue;
                    }

                    if ((i + 1) % 2 == 0)
                    {
                        Int32 onesDigit = Int32.Parse(numberReversed[i - 1].ToString());
                        Int32 tensDigit = Int32.Parse(numberReversed[i].ToString());
                        

                        if (tensDigit > 1)
                        {
                            if (onesDigit > 0)
                            {
                                englishNumber.Add(oneDigitNumberToEnglish(onesDigit) + " ");    
                            }

                            englishNumber.Add(tensDigitNumberToEnglish(tensDigit) + " ");    
                        }
                        else if (tensDigit == 1)
                        {
                            Int32 value = Int32.Parse(string.Concat(tensDigit.ToString(), onesDigit.ToString()));

                            englishNumber.Add(outlierTensDigitNumberToEnglish(value) + " ");
                        }
                    }
                }

                foreach (var item in englishNumber)
                {
                    Console.WriteLine(item);
                }
            }
            // Stores the english equivalent of all other numbers.
            else
            {
                for (int i = 0; i < signedNumber.ToString().Length; i++)
                {
                    if (numberReversed[i].Equals('-'))
                    {
                        englishNumber.Add("negative ");

                        continue;
                    }

                    // Every third number, we create the english equivalent of that 
                    // portion of the overall number. 
                    if ((i + 1) % 3 == 0)
                    {
                        Int32 onesDigit = Int32.Parse(numberReversed[i - 2].ToString());
                        Int32 tensDigit = Int32.Parse(numberReversed[i - 1].ToString());
                        Int32 hundredsDigit = Int32.Parse(numberReversed[i].ToString());

                        //Console.WriteLine(hundredsDictionary[i + 1]);

                        if (i != 2)
                        {
                            englishNumber.Add(hundredsDictionary[i + 1]);
                        }

                        if (tensDigit > 1)
                        {
                            if (onesDigit > 0)
                            {
                                englishNumber.Add(oneDigitNumberToEnglish(onesDigit) + " ");    
                            }

                            englishNumber.Add(tensDigitNumberToEnglish(tensDigit));
                        }
                        else if (tensDigit == 1)
                        {
                            Int32 value = Int32.Parse(string.Concat(tensDigit.ToString(), onesDigit.ToString()));

                            englishNumber.Add(outlierTensDigitNumberToEnglish(value) + " ");
                        }

                        if (hundredsDigit > 0)
                        {
                            englishNumber.Add(oneDigitNumberToEnglish(hundredsDigit) + " " + hundredsDictionary[3] + " ");

                            //Console.WriteLine("i is: " + i);
                        }


                        //Console.WriteLine(numberReversed[i]);
                        //Console.WriteLine(hundredsDictionary[i + 1]);
                    }
                }

                foreach (var item in englishNumber)
                {
                    Console.WriteLine(item);
                }
            }
            


            /*
            char[] numberStringReversed = signedNumber.ToString().ToCharArray().Reverse().ToArray();

            for (int i = 0; i < numberStringReversed.Length; i++)
            {                
                if (numberStringReversed[i].Equals('-'))
                {
                    continue;
                }

                if ((i + 1) % 3 == 0)
                {
                    int hundredsDigit = Int32.Parse(numberStringReversed[i].ToString());
                    int tensDigit = Int32.Parse(numberStringReversed[i - 1].ToString());
                    int onesDigit = Int32.Parse(numberStringReversed[i - 2].ToString());
                    string numberInEnglish = "";

                    if (hundredsDigit != 0)
                    {
                       numberInEnglish = String.Concat(hundredsDigit + " " + hundredsDictionary[3]);
                    }



                    Console.WriteLine(numberInEnglish);


                    
                    if (hundredsDigit == 0)
                    {
                        string tensPlaceNumber = "";
                        string onesPlaceNumber = "";

                        if (tensDigit != 0)
                        {
                            tensPlaceNumber = tensDigitNumberToEnglish(tensDigit) + " ";    
                            onesPlaceNumber = tensDigitNumberToEnglish(onesDigit) + " ";    
                        }
                        else if (tensDigit == 0)
                        {
                            onesPlaceNumber = oneDigitNumberToEnglish(onesDigit) + " ";
                        }
                        

                        Console.WriteLine(tensPlaceNumber + onesPlaceNumber);
                    }
                    
                    

                    
                    //string hundredsPlaceNumber = oneDigitNumberToEnglish(Int32.Parse(numberStringReversed[i].ToString())) + " " + hundredsDictionary[3] + " ";
                    //string tensPlaceNumber = tensDigitNumberToEnglish(Int32.Parse(numberStringReversed[i - 1].ToString())) + " ";
                    //string onesPlaceNumber = oneDigitNumberToEnglish(Int32.Parse(numberStringReversed[i - 2].ToString())) + " ";

                    //Console.Write(hundredsPlaceNumber + tensPlaceNumber + onesPlaceNumber);   
                }  
            }
            */
        }

        private string oneDigitNumberToEnglish(Int32 digit)
        {
            return numberDictionary[digit];
        }

        private string tensDigitNumberToEnglish(Int32 digit)
        {
            return tensDictionary[digit];
        }

        private string outlierTensDigitNumberToEnglish(Int32 digit)
        {
            return specialNumberDictionary[digit];
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
