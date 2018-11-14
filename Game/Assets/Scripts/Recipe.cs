using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Recipe
    {
        public int IdPotion { get; private set; }
        public double Temp  { get; private set; }
        public int IdLiquid { get; private set; }
        public int IdIngredient { get; private set; }

        public Recipe (int IdPotion, double Temp, int IdLiquid, int IdIngredient)
        {
            this.IdPotion = IdPotion;
            this.Temp = Temp;
            this.IdLiquid = IdLiquid;
            this.IdIngredient = IdIngredient;
        }

        public Recipe()
        {

        }
    }
}
