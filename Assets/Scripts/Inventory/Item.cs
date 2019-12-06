using UnityEngine;

public class Item
{
    #region Variables
    private int _id;

    private string _name;
    private string _description;
    private int _amount;
    private int _value;

    private int _damage;
    private int _armour;
    private int _heal;
    
    public bool itemRequiresStat;
    private string _requiredStat;
    private int _requiredStatValue;

    private Texture2D _iconName;
    private Sprite _iconSprite;
    private GameObject _meshName;
    private ItemTypes _type;

    #endregion
    #region Properties
    public int ID
    {
        get { return _id; }
        set { _id = value;  }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public int Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public int Armour
    {
        get { return _armour; }
        set { _armour = value; }
    }

    public int Heal
    {
        get { return _heal; }
        set { _heal = value; }
    }

    public Texture2D IconName
    {
        get { return _iconName; }
        set { _iconName = value; }
    }

    public Sprite IconSprite
    {
        get { return _iconSprite; }
        set { _iconSprite = value; }
    }

    public GameObject MeshName
    {
        get { return _meshName; }
        set { _meshName = value; }
    }

    public ItemTypes Type
    {
        get { return _type; }
        set { _type = value; }
    }
    #endregion
}

public enum ItemTypes
{
    Armour,
    Weapon,
    Potion,
    Money,
    Quest,
    Food,
    Ingredient,
    Craftable,
    Material,
    Misc
}
