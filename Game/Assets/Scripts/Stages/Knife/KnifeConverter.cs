using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    static public class KnifeConverter
    {
        static private Dictionary<int, int> convertDictionary;
        static KnifeConverter()
        {
            convertDictionary = new Dictionary<int, int>();
            convertDictionary.Add(1, 6);
            convertDictionary.Add(0, 10);
            convertDictionary.Add(11, 12);
            convertDictionary.Add(2, 16);
        }
        static public int getAfterKnife(int ingredient)
        {
            return convertDictionary[ingredient];
        }
    }
}
