using Xunit;
using System;
using System.Collections.Generic;
using Integer_To_English_Word;

public class testingClass
{
    // Name:        edgeBasicConditionTest
    // Purpose:     To test ten random use case values.
    // Description: Takes the given random test values and converts them to the english language to test for correctness.
    [Theory]
    [InlineData("-262081978")]
    [InlineData("1006935391")]
    [InlineData("1670376543")]
    [InlineData("1378669339")]
    [InlineData("1047641375")]
    [InlineData("675232136")]
    [InlineData("527791378")]
    [InlineData("-531405650")]
    [InlineData("1571325509")]
    [InlineData("225655")]
    public void edgeBasicConditionTest(string value)
    {
        // Our list of possible correct answers.
        List<string> correctEdgeAnswers = new List<string>(new string[] { 
            "negative two hundred sixty two million eighty one thousand nine hundred seventy eight ",
            "one billion six million nine hundred thirty five thousand three hundred ninety one ",
            "one billion six hundred seventy million three hundred seventy six thousand five hundred forty three ",
            "one billion three hundred seventy eight million six hundred sixty nine thousand three hundred thirty nine ",
            "one billion forty seven million six hundred forty one thousand three hundred seventy five ",
            "six hundred seventy five million two hundred thirty two thousand one hundred thirty six ",
            "five hundred twenty seven million seven hundred ninety one thousand three hundred seventy eight ",
            "negative five hundred thirty one million four hundred five thousand six hundred fifty ",
            "one billion five hundred seventy one million three hundred twenty five thousand five hundred nine ",
            "two hundred twenty five thousand six hundred fifty five "
        });
        
        // Creates the test value and stores them in the corresponding variables.
        int userNumber, sign, numberLength;
        string reverseString;
        
        formatNumber(new string[] {value}, out userNumber, out sign, out numberLength, out reverseString);

        // Converts the test number to english.
        StandardDictionaryNumbers numberLibrary = new StandardDictionaryNumbers(reverseString, sign, numberLength, userNumber);

        // Takes the result and checks the list of correct answers for a match.
        string result = numberLibrary.getEnglishNumber();
        int index = correctEdgeAnswers.IndexOf(result);

        if (index != -1)
        {
            Assert.Equal(correctEdgeAnswers[index], result);    
        }
        else
        {
            Assert.True(false, "The number " + value + " was not properly converted to the english language. We got \"" + result + "\" instead.");
        }
    }

    // Name:        edgePositiveConditionTest
    // Purpose:     To test the positive edge case numbers and 0.
    // Description: Takes the given positive edge cases and converts them to the english language to check for correctness.
    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("9")]
    [InlineData("10")]
    [InlineData("99")]
    [InlineData("100")]
    [InlineData("999")]
    [InlineData("1000")]
    [InlineData("9999")]
    [InlineData("10000")]
    [InlineData("99999")]
    [InlineData("100000")]
    [InlineData("999999")]
    [InlineData("1000000")]
    [InlineData("9999999")]
    [InlineData("10000000")]
    [InlineData("99999999")]
    [InlineData("100000000")]
    [InlineData("999999999")]
    [InlineData("1000000000")]
    [InlineData("2147483646")]
    [InlineData("2147483647")]
    public void edgePositiveConditionTest(string value)
    {
        // Our list of possible correct answers.
        List<string> correctEdgeAnswers = new List<string>(new string[] { 
            "zero ", "one ", "nine ", "ten ", 
            "ninety nine ", "one hundred ",
            "nine hundred ninety nine ", "one thousand ",
            "nine thousand nine hundred ninety nine ", "ten thousand ",
            "ninety nine thousand nine hundred ninety nine ", "one hundred thousand ",
            "nine hundred ninety nine thousand nine hundred ninety nine ", "one million ",
            "nine million nine hundred ninety nine thousand nine hundred ninety nine ", "ten million ",
            "ninety nine million nine hundred ninety nine thousand nine hundred ninety nine ", "one hundred million ",
            "nine hundred ninety nine million nine hundred ninety nine thousand nine hundred ninety nine ", "one billion ",
            "two billion one hundred forty seven million four hundred eighty three thousand six hundred forty six ",
            "two billion one hundred forty seven million four hundred eighty three thousand six hundred forty seven ",
        });
        
        // Creates the test value and stores them in the corresponding variables.
        int userNumber, sign, numberLength;
        string reverseString;
        
        formatNumber(new string[] {value}, out userNumber, out sign, out numberLength, out reverseString);

        // Converts the test number to english.
        StandardDictionaryNumbers numberLibrary = new StandardDictionaryNumbers(reverseString, sign, numberLength, userNumber);

        // Takes the result and checks the list of correct answers for a match.
        string result = numberLibrary.getEnglishNumber();
        int index = correctEdgeAnswers.IndexOf(result);

        if (index != -1)
        {
            Assert.Equal(correctEdgeAnswers[index], result);    
        }
        else
        {
            Assert.True(false, "The number " + value + " was not properly converted to the english language. We got \"" + result + "\" instead.");
        }
    }

    // Name:        edgeNegativeConditionTest
    // Purpose:     To test the negative edge case numbers and 0.
    // Description: Takes the given negative edge cases and converts them to the english language to check for correctness.
    [Theory]
    [InlineData("-1")]
    [InlineData("-9")]
    [InlineData("-10")]
    [InlineData("-99")]
    [InlineData("-100")]
    [InlineData("-999")]
    [InlineData("-1000")]
    [InlineData("-9999")]
    [InlineData("-10000")]
    [InlineData("-99999")]
    [InlineData("-100000")]
    [InlineData("-999999")]
    [InlineData("-1000000")]
    [InlineData("-9999999")]
    [InlineData("-10000000")]
    [InlineData("-99999999")]
    [InlineData("-100000000")]
    [InlineData("-999999999")]
    [InlineData("-1000000000")]
    [InlineData("-2147483647")]
    [InlineData("-2147483648")]
    public void edgeNegativeConditionTest(string value)
    {
        // Our list of possible correct answers.
        List<string> correctEdgeAnswers = new List<string>(new string[] { 
            "negative one ", "negative nine ", "negative ten ", 
            "negative ninety nine ", "negative one hundred ",
            "negative nine hundred ninety nine ", "negative one thousand ",
            "negative nine thousand nine hundred ninety nine ", "negative ten thousand ",
            "negative ninety nine thousand nine hundred ninety nine ", "negative one hundred thousand ",
            "negative nine hundred ninety nine thousand nine hundred ninety nine ", "negative one million ",
            "negative nine million nine hundred ninety nine thousand nine hundred ninety nine ", "negative ten million ",
            "negative ninety nine million nine hundred ninety nine thousand nine hundred ninety nine ", "negative one hundred million ",
            "negative nine hundred ninety nine million nine hundred ninety nine thousand nine hundred ninety nine ", "negative one billion ",
            "negative two billion one hundred forty seven million four hundred eighty three thousand six hundred forty seven ",
            "negative two billion one hundred forty seven million four hundred eighty three thousand six hundred forty eight "
        });
        
        // Creates the test value and stores them in the corresponding variables.
        int userNumber, sign, numberLength;
        string reverseString;
        
        formatNumber(new string[] {value}, out userNumber, out sign, out numberLength, out reverseString);

        // Converts the test number to english.
        StandardDictionaryNumbers numberLibrary = new StandardDictionaryNumbers(reverseString, sign, numberLength, userNumber);

        // Takes the result and checks the list of correct answers for a match.
        string result = numberLibrary.getEnglishNumber();
        int index = correctEdgeAnswers.IndexOf(result);

        if (index != -1)
        {
            Assert.Equal(correctEdgeAnswers[index], result);    
        }
        else
        {
            Assert.True(false, "The number " + value + " was not properly converted to the english language. We got \"" + result + "\" instead.");
        }
    }

    // Name:        formatNumber
    // Purpose:     To make the given number's length be a multiple of 3 and reverse it.
    // Description: Finds the number's real length based on whether it is positive or negative and uses 
    //              that information to adjust the length accordingly and reverse it.
    private static void formatNumber(string[] values, out int userNumber, out int sign, out int numberLength, out string reverseString)
    {
        // If the number is not a signed 32 bit integer, or if 
        // the user has provided too many arguments, inform them
        // and exit the program.
        userNumber = Program.IsValidInput(values);

        // Determine whether the user's number is a negative or positive number 
        // and adjust length accordingly.
        sign = Math.Sign(userNumber);
        numberLength = sign >= 0 ? userNumber.ToString().Length : userNumber.ToString().Length - 1;

        // Reverses the user's number and make its length a multiple of 3 in
        // order to convert the number to english.
        NumberFormatter formatter = new NumberFormatter();
        reverseString = formatter.reverseNumber(userNumber);
        formatter.formatNumberLength(ref reverseString, sign, ref numberLength);
    }
}