using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Linq.Expressions;
using System.Linq;
using Assets.Scripts;

public class DataBase : MonoBehaviour
{

    public int inventoryCapacity = 10;

    private List<Item> itemsList = new List<Item>();
    private JsonData itemData;

    private List<Item> inventoryList = new List<Item>();
    private JsonData inventoryData;

    private List<StrewItem> strewImageList = new List<StrewItem>();

    private List<Item> witcherList = new List<Item>();
    private List<WhitcherItems> witcherCounterList = new List<WhitcherItems>();
    private JsonData witcherData;

    private List<Recipe> recipesList = new List<Recipe>();

    private List<ItemDescription> potionDescriptions = new List<ItemDescription>();
    private List<ItemDescription> ingredientDescriptions = new List<ItemDescription>();
    private List<Achive> achives = new List<Achive>();
    private List<Persons> persons = new List<Persons>();

    void Start()
    {
        FillItems();
        FillInventory();
        FillStrewImage();
        FillWhitcher();
        FillRecipes();
        FillDescriptions();
    }

    void FillDescriptions()
    {
        potionDescriptions = JsonMapper.ToObject<List<ItemDescription>>(Resources.Load("Json/potionDescriptions").ToString());
        ingredientDescriptions = JsonMapper.ToObject<List<ItemDescription>>(Resources.Load("Json/ingredientDescriptions").ToString());
        achives = JsonMapper.ToObject<List<Achive>>(Resources.Load("Json/Achives").ToString());
        persons = JsonMapper.ToObject<List<Persons>>(Resources.Load("Json/Persons").ToString());
    }
    public List<Achive> getAchives()
    {
        return achives;
    }

    public List<Persons> getPersons()
    {
        return persons;
    }
    public string GetPotionDescriotion(int id)
    {
        foreach (ItemDescription description in potionDescriptions)
        {
            if (description.Id == id)
                return description.Description;
        }
        return null;
    }

    public string GetIngredientDescriotion(int id)
    {
        foreach (ItemDescription description in ingredientDescriptions)
        {
            if (description.Id == id)
                return description.Description;
        }
        return null;
    }

    void FillRecipes()
    {
        recipesList = JsonMapper.ToObject<List<Recipe>>(Resources.Load("Json/recipes").ToString());

    }

    public List<Recipe> GetRecipes()
    {
        return recipesList;
    }

    void SaveWhitcher()
    {
        /*
        var json = JsonMapper.ToJson(witcherItemsList);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/witcher.json", json, System.Text.Encoding.UTF8);
        */
    }
    void FillWhitcher()
    {
        witcherData = JsonMapper.ToObject(Resources.Load("Json/witcher").ToString());
        witcherCounterList = JsonMapper.ToObject<List<WhitcherItems>>(Resources.Load("Json/witcher").ToString());
        for (int i = 0; i < witcherData.Count; i++)
        {
            for(int j = 0; j < (int)witcherData[i]["Count"]; j++)
            {
                witcherList.Add(getById((int)witcherData[i]["Id"] ));
            }
        }
    }
    public List<Item> getUniqueWhitcher()
    {
        return witcherList.GroupBy(x => x.Id, (key, group) => group.First()).ToList();
    }

    public int getWitcherTotalCount()
    {
        return witcherCounterList.Count;
    }
    public int getWhitcherItemCount(int id)
    {
        int count = 0;
        for (int i = 0; i < witcherCounterList.Count; i++)
        {
            if (witcherCounterList[i].Id == id)
                return witcherCounterList[i].Count;
        }
        return count;
    }
    public int getWhitcherPrice(int id)
    {
        for (int i = 0; i < witcherCounterList.Count; i++)
        {
            if(witcherCounterList[i].Id == id)
            {
                return witcherCounterList[i].Price;
            }
        }
        return -1;
    }

    public void WhitcherAddItem(int id)
    {
        var item = getById(id);
        witcherList.Add(item);

    }

    public void WhitcherAddQuestItem(int id, int price)
    {
        var item = getById(id);
        witcherList.Add(item);
        witcherCounterList.Add(new WhitcherItems(id, 1, price));
    }

    public int whitcherSell(int id)
    {
        for (int i = 0; i < witcherCounterList.Count; i++)
        {
            {
                if (witcherCounterList[i].Id == id)
                {
                    --witcherCounterList[i].Count;
                    if (witcherCounterList[i].Count <= 0)
                    {
                    //    witcherCounterList.RemoveAt(i);
                    //    SaveWhitcher();
                    //    FillWhitcher();
                        return 0;
                    }else
                     //   SaveWhitcher();
                    //FillWhitcher();

                    return witcherCounterList[i].Count;

                }
            }
        }
        return -1;
    }

    void FillInventory()
    {
        inventoryData = JsonMapper.ToObject( Resources.Load("Json/inventory").ToString());
        inventoryList = ItemsBuilder(inventoryData);

    }
    void FillItems()
    {
     
        itemData = JsonMapper.ToObject(Resources.Load("Json/items").ToString());
        itemsList = ItemsBuilder(itemData);

    }
    void FillStrewImage()
    {

        strewImageList = JsonMapper.ToObject<List<StrewItem>>(Resources.Load("Json/strewParticals").ToString());

    }

    public Sprite GetSpriteById(int id)
    {
        foreach (Item item in itemsList)
        {
            if (item.Id == id)
                return item.getSprite();
        }
        return null;
    }

    public Sprite GetStrewSpriteById(int id)
    {
        foreach(StrewItem item in strewImageList)
        {
            if (item.Id == id)
                return item.GetSprite();
        }
        return null;
    }

    public Item getById(int id)
    {
        foreach (Item item in itemsList)
        {
            if (item.Id == id)
                return item;
        }
        return new Item();
    }

    public Item getRandomItem()
    {
        return inventoryList[Random.Range(0, inventoryList.Count - 1)];
    }

    public Item getItemBySlug(string slug)
    {
        return (from item in inventoryList
                where item.Slug == slug 
                select item)
               .FirstOrDefault();
    }

    public List<Item> getInventory()
    {
        return inventoryList;
    }
    public int InventoryCount()
    {
        return inventoryList.Count;
    }
    public int UniqueInventoryCount()
    {
        return getUniqueInventory().Count;
    }

    public Item getFromInventoryById(int id)
    {
        foreach (Item item in inventoryList)
        {
            if (item.Id == id)
                return item;
        }
        return new Item();
    }

    public void addItemToInventory(int id, int quality)
    {

        Item addble = getById(id);
        if (-1 == addble.Id)
            return;
        addble.Quality = quality;
        inventoryList.Add(addble);
        var json = JsonMapper.ToJson(inventoryList);
        Global.Hud.AddToInventoryAnim(id);
       Global.Hud.RefillInventory();
       // File.WriteAllText(Application.dataPath + "/StreamingAssets/inventory.json", json, System.Text.Encoding.UTF8);
    }

    public void deleteFromInventory(int id)
    {
        for(int i = 0; i < inventoryList.Count; i++)
        {
            if(inventoryList[i].Id == id)
            {
                inventoryList.RemoveAt(i);
            }
        }
        var json = JsonMapper.ToJson(inventoryList);
       // File.WriteAllText(Application.dataPath + "/StreamingAssets/inventory.json", json, System.Text.Encoding.UTF8);
        Global.Hud.RefillInventory();
    }

    public int getCountById(int id)
    {
        int count = 0;
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].Id == id)
                ++count;
        }
        return count;
    }

    public List<Item> getUniqueInventory()
    {
        return  inventoryList.GroupBy(x => x.Id, (key, group) => group.First()).ToList();
    }

    public List<Item> ItemsList
    {
        get
        {
            return itemsList;
        }
    }

    private List<Item> ItemsBuilder(JsonData data)
    {
        List<Item> listToReturn = new List<Item>();
        for (int i = 0; i < data.Count; i++)
        {
            listToReturn.Add(
            new Item(
               (int)data[i]["Id"],
               (string)data[i]["Title"],
               (string)data[i]["Slug"],
               (bool)data[i]["Potion"],
               (bool)data[i]["Ingredient"],
               (bool)data[i]["Boilable"],
               (int)data[i]["Quality"],
               (bool)data[i]["Cuttable"],
               (bool)data[i]["Strewable"]));
        }
        return listToReturn;
    }
}

public class WhitcherItems
{
    public int Id { get; private set; }
    public int Count { get;  set; }
    public int Price { get; private set; }

    public WhitcherItems(int id, int count, int price)
    {
        Id = id;
        Count = count;
        Price = price;
    }

    public WhitcherItems()
    {
        Id = -1;
    }
}

public class Item
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    private Sprite sprite;
    public string Slug { get; private set; }
    public bool Potion { get; private set; }
    public bool Ingredient { get; private set; }
    public bool Boilable { get; private set; }
    public bool Cuttable { get; private set; }
    public int Quality { get; set; }
    public bool Strewable { get; private set; }
    

    public Item(int id, string title, string slug, bool potion, bool ingredient, bool boilable,
                int quality, bool cuttable, bool strewable)
    {
        Id = id;
        Title = title;
        Slug = slug;
        this.sprite = Resources.Load<Sprite>("InventorySprites\\" + Slug);
        Potion = potion;
        Ingredient = ingredient;
        Boilable = boilable;
        Quality = quality;
        Cuttable = cuttable;
        Strewable = strewable;

    }

    public Sprite getSprite()
    {
      
        return sprite;
    }
    public Item()
    {
        Id = -1;
    }

}

public class StrewItem
{
    public int Id { get; private set; }
    public string Image { get; private set; }

    private Sprite sprite;

    public StrewItem(int id, string image)
    {
        Id = id;
        Image = image;
        sprite = Resources.Load<Sprite>("ParticalsSprites\\" + image);
    }

    public StrewItem()
    {
        Id = -1;
    }

    public Sprite GetSprite()
    {
        if (sprite == null)
        {
            sprite = Resources.Load<Sprite>("ParticalsSprites\\" + Image);
        }
        return sprite;
    }
}

public class ItemDescription
{
    public int Id { get; private set; }
    public string Description { get; private set; }

    public ItemDescription()
    {
       
    }
}


public class Achive
{
    public int Id { get; private set; }
    public string Description { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public bool IsOpen { get; set; }
    private Sprite sprite;

    public Achive()
    {

    }

    public Sprite getSprite()
    {
        if (sprite == null)
        {
            sprite = Resources.Load<Sprite>("InventorySprites\\" + Slug);
        }
        return sprite;
    }
}

public class Persons
{
    public int Id { get; private set; }
    public string Description { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public bool IsOpen { get; set; }
    private Sprite sprite;

    public Persons()
    {

    }

    public Sprite getSprite()
    {
        if (sprite == null)
        {
            sprite = Resources.Load<Sprite>("InventorySprites\\" + Slug);
        }
        return sprite;
    }
}