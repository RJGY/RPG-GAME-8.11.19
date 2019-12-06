using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemData 
{
    public static Item CreateItem(int itemID)
    {
        string name = "";
        string description = "";
        int amount = 0;
        int value = 0;

        int damage = 0;
        int armour = 0;
        int heal = 0;

        /*
        bool itemRequiresStat;
        string requiredStat;
        int requiredStatValue;
        */

        string iconName = "";
        string meshName = "";
        ItemTypes type = ItemTypes.Misc;

        switch(itemID)
        {
            #region Armour 0 - 99
            case 0:
                name = "Leather Boots";
                description = "ur mom gei";
                amount = 1;
                value = 20;
                damage = 0;
                armour = 2;
                heal = 0;
                iconName = "Armour/leatherBoots";
                meshName = "Armour/leatherBoots";
                type = ItemTypes.Armour;
                break;

            case 1:
                name = "Leather Tunic";
                description = "";
                amount = 1;
                value = 20;
                damage = 0;
                armour = 2;
                heal = 0;
                iconName = "Armour/leatherTunic";
                meshName = "Armour/leatherTunic";
                type = ItemTypes.Armour;
                break;
            case 2:
                name = "Leather Trousers";
                description = "";
                amount = 1;
                value = 20;
                damage = 0;
                armour = 2;
                heal = 0;
                iconName = "Armour/leatherTrousers";
                meshName = "Armour/leatherTrousers";
                type = ItemTypes.Armour;
                break;
            #endregion

            #region Weapons 100 - 199
            case 100:
                name = "Broad Sword";
                description = "";
                amount = 1;
                value = 40;
                damage = 10;
                armour = 0;
                heal = 0;
                iconName = "Weapons/broadSword";
                meshName = "Weapons/broadSword";
                type = ItemTypes.Weapon;
                break;

            case 101:
                name = "Halberd";
                description = "";
                amount = 1;
                value = 35;
                damage = 15;
                armour = 0;
                heal = 0;
                iconName = "Weapons/halberd";
                meshName = "Weapons/halberd";
                type = ItemTypes.Weapon;
                break;

            case 102:
                name = "War Axe";
                description = "";
                amount = 1;
                value = 40;
                damage = 20;
                armour = 0;
                heal = 0;
                iconName = "Weapons/warAxe";
                meshName = "Weapons/warAxe";
                type = ItemTypes.Weapon;
                break;

            #endregion

            #region Potion 200 - 299
            case 200:
                name = "Health Potion";
                description = "";
                amount = 1;
                value = 10;
                damage = 0;
                armour = 0;
                heal = 30;
                iconName = "Potions/healthPotion";
                meshName = "Potions/healthPotion";
                type = ItemTypes.Potion;
                break;

            case 201:
                name = "Mana Potion";
                description = "";
                amount = 1;
                value = 10;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "Potions/manaPotion";
                meshName = "Potions/manaPotion";
                type = ItemTypes.Potion;
                break;

            case 202:
                name = "Stamina Potion";
                description = "";
                amount = 1;
                value = 10;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "Potions/staminaPotion";
                meshName = "Potions/staminaPotion";
                type = ItemTypes.Potion;
                break;
            #endregion

            #region Food 300 - 399
            case 300:
                name = "Apple";
                description = "its red";
                amount = 1;
                value = 1;
                damage = 0;
                armour = 0;
                heal = 2;
                iconName = "Food/Apple";
                meshName = "Food/Apple";
                type = ItemTypes.Food;
                break;

            case 301:
                name = "Meat";
                description = "its red too";
                amount = 1;
                value = 12;
                damage = 0;
                armour = 0;
                heal = 22;
                iconName = "Food/Meat";
                meshName = "Food/Meat";
                type = ItemTypes.Food;
                break;
            #endregion

            #region Ingredient 400 - 499
            case 400:
                name = "Tusk";
                description = "";
                amount = 1;
                value = 100;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "Ingredient/Tusk";
                meshName = "Ingredient/Tusk";
                type = ItemTypes.Ingredient;
                break;

            case 401:
                name = "Goat Hoof";
                description = "";
                amount = 1;
                value = 50;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "Ingredient/goatHoof";
                meshName = "Ingredient/goatHoof";
                type = ItemTypes.Ingredient;
                break;

            case 402:
                name = "Four Leaf Clover";
                description = "";
                amount = 1;
                value = 1000;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "Ingredient/fourLeafClover";
                meshName = "Ingredient/fourLeafClover";
                type = ItemTypes.Misc;
                break;
            #endregion

            #region Craftable 500 - 599
            case 500:
                name = "Book";
                description = "";
                amount = 1;
                value = 15;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "Craftable/book";
                meshName = "Craftable/book";
                type = ItemTypes.Craftable;
                break;

            case 501:
                name = "Dice";
                description = "LADY LUCKY SMILIN";
                amount = 1;
                value = 20;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "Craftable/dice";
                meshName = "Craftable/dice";
                type = ItemTypes.Craftable;
                break;

            case 502:
                name = "Nail";
                description = "";
                amount = 1;
                value = 5;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "Craftable/nail";
                meshName = "Craftable/nail";
                type = ItemTypes.Craftable;
                break;
            #endregion

            #region Quest 600 - 699
            case 600:
                name = "Bronze Coin";
                description = "";
                amount = 1;
                value = 1;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "Quest/bronzeCoin";
                meshName = "Quest/bronzeCoin";
                type = ItemTypes.Quest;
                break;

            case 601:
                name = "Silver Coin";
                description = "";
                amount = 1;
                value = 100;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "Quest/silverCoin";
                meshName = "Quest/silverCoin";
                type = ItemTypes.Quest;
                break;

            case 602:
                name = "Gold Coin";
                description = "";
                amount = 1;
                value = 10000;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "Quest/goldCoin";
                meshName = "Quest/goldCoin";
                type = ItemTypes.Quest;
                break;
            #endregion

            #region Misc 700 - 799
            case 700:
                name = "Heart";
                description = "";
                amount = 1;
                value = 1;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "Misc/Heart";
                meshName = "Misc/Heart";
                type = ItemTypes.Misc;
                break;
            #endregion

            default:
                itemID = 0;
                name = "";
                description = "";
                amount = 0;
                value = 0;
                damage = 0;
                armour = 0;
                heal = 0;
                iconName = "";
                meshName = "";
                type = ItemTypes.Misc;
                break;
        }

        Item temp = new Item
        {
            ID = itemID,
            Name = name,
            Description = description,
            Value = value,
            Amount = amount,
            Damage = damage,
            Armour = armour,
            Heal = heal,
            IconName = Resources.Load("Icons/" + iconName) as Texture2D,
            MeshName = Resources.Load("Prefabs/" + meshName) as GameObject,
            Type = type
        };

        return temp;
    }
}
