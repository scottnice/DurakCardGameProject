using System;
using System.Collections.Generic;

namespace CardLibrary
{
    public static class Functions
    {
        /// <summary>
        /// Swaps the values of two variables
        /// Author: Scott Nice
        /// Date: 09/01/2014
        /// </summary>
        public static void swap<T>(ref T one, ref T two)
        {
            T temp = one;
            one = two;
            two = temp;
        }

        /// <summary>
        /// Swaps the values of two elements of a container
        /// Author: Scott Nice
        /// Date: 09/01/2014
        /// </summary>
        public static void swap<T>(IList<T> container, int indexOne, int indexTwo)
        {
            T temp = container[indexOne];
            container[indexOne] = container[indexTwo];
            container[indexTwo] = temp;
        }
    }
}
