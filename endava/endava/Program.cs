using System;
using System.Collections.Generic;
using System.Linq;

namespace endava
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[] { 1, 2, 1, 3 };
            foreach (var number in FindUniqueNumbers(numbers))
                Console.WriteLine(number);
        }

        public static IEnumerable<int> FindUniqueNumbers(IEnumerable<int> numbers)
        {
            return numbers.Distinct();
        }


    }
}