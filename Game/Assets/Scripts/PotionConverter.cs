using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class PotionConverter
    {
        static public Item GetPotion(int idLiquid, int idIngredient, int IngredientQuality, double Temp )
        {
            foreach (Recipe recipe in Global.DataBase.GetRecipes())
            {
                if ( recipe.IdLiquid == idLiquid && recipe.IdIngredient == idIngredient)
                {
                    int LiquiddQuality = 100 - (int)Math.Abs(recipe.Temp - Temp) * 9;
                    LiquiddQuality = LiquiddQuality < 0 ? 0 : LiquiddQuality;
                    int totalQuality = (LiquiddQuality + IngredientQuality) / 2;
                    var item = Global.DataBase.getById(recipe.IdPotion);
                    item.Quality = totalQuality;
                    return item;
                }
            }
            return new Item();
        }
    }
}
