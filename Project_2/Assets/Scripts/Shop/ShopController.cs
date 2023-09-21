using System.Linq;
using UnityEngine;
public class ShopController : MonoBehaviour {
    
    private LocalDataProvider _dataProvider;
    public void Initialize(LocalDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public bool Buy(string id)
    {
        var item = Storage.ShopConfig.ShopItems.FirstOrDefault(x => x.Id == id);
        if (Storage.PlayerInventory.Money < item!.BuyPrice)
        {
            Debug.Log("Нужно больше золота");
            return false;
        }

        Spend(item!.BuyPrice);
        
        if (item.ItemType == ItemType.Pack)
        {
            foreach (var itemId in item.PackItems)
            {
                var itemInPack = Storage.ShopConfig.ShopItems.FirstOrDefault(x => x.Id == itemId);
                AddItemInInventory(itemInPack);
            }
        }
        else
        {
            AddItemInInventory(item);
        }
        
        return true;
    }

    public void Sell(string id)
    {
        var itemInInventory = Storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == id);
        if (itemInInventory == null)
        {
            Debug.Log("Такого предмета нет в инветаре");
            return;
        }
        var item = Storage.ShopConfig.ShopItems.FirstOrDefault(x => x.Id == id);
        AddCoins(item!.SellPrice);
        DeleteOrReduceItemFromInventory(itemInInventory);
    }

    private void AddItemInInventory(ShopItem shopItem)
    {
        var itemFromInventory = Storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == shopItem.Id);
        if (itemFromInventory == null)
        {
            var ownedItem = new OwnedItem
            {
                Id = shopItem.Id,
                Amount = 1,
            };
            Storage.PlayerInventory.PlayerItems.Add(ownedItem);
        }
        else
        {
            itemFromInventory.Amount++;
        }

        _dataProvider.SavePlayerInventory();
    }

    private void DeleteOrReduceItemFromInventory(OwnedItem ownedItem)
    {
        if (ownedItem.Amount == 1)
            Storage.PlayerInventory.PlayerItems.Remove(ownedItem);
        else
            ownedItem.Amount--;

        _dataProvider.SavePlayerInventory();
    }

    private void AddCoins(int coins)
    {
        if (coins < 0)
        {
            Debug.Log("Цена товара меньше 0");
            return;
        }

        Storage.PlayerInventory.Money += coins;
    }

    private void Spend(int coins)
    {
        if (coins < 0)
        {
            Debug.Log("Стоймость товара меньше 0");
            return;
        }

        Storage.PlayerInventory.Money -= coins;
    }
}