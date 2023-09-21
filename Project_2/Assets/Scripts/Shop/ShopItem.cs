public class ShopItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    
    public ItemType ItemType { get; set; }
    
    public string[] PackItems { get; set; }
}

public enum ItemType
{
    Common,
    Pack
}