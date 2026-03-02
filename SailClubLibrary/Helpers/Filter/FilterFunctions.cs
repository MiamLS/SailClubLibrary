using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Helpers.Filter
{
    //public class FilterFunctions<T>(List<T> objects, params Predicate<T>[] predicates)
    //{

    //}

    public class FilterFunctions
        {
        //I Filterfunctions skal der laves en generisk funktion, som kan filtrere en generisk liste ud fra et predicate
        public void ConditionalPrint<T>(List<T> objects, Predicate<T> pred)
        {
            Console.WriteLine();
            foreach (var item in objects.FindAll(pred))
            {
                Console.WriteLine(item);
            }
        }

        //I Filterfunctions skal der laves en generisk funktion, som kan filtrere en generisk liste ud fra to predicates

        public void ConditionalPrint2<T>(List<T> objects, Predicate<T> pred1, Predicate<T> pred2)
        {
            Console.WriteLine();
            foreach (var item in objects.FindAll(pred1).FindAll(pred2))
            {
                Console.WriteLine(item);
            }
        }

        //I Filterfunctions skal der laves en generisk funktion, som kan filtrere en generisk liste ud fra flere predicates

    }
}
