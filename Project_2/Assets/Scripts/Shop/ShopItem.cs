using UnityEngine;
public class ShopItem : ScriptableObject
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
}