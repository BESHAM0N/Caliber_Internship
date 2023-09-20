using System.Linq;
using UnityEngine;
public class ShopController : MonoBehaviour
{
    private Storage _storage;
    private LocalDataProvider _dataProvider;

    public void Initialize(
        Storage storage,
        LocalDataProvider dataProvider)
    {
        _storage = storage;
        _dataProvider = dataProvider;
    }

    public bool Buy(ShopItem shopItem)
    {
        if (_storage.PlayerInventory.Money < shopItem.BuyPrice)
        {
            Debug.Log("Нужно больше золота");
            return false;
        }

        Spend(shopItem.BuyPrice);
        var itemType = shopItem.GetType();
        if (itemType == typeof(Pack))
        {
            Pack pack = shopItem as Pack;
            foreach (var id in pack!.Items)
            {
                var itemInPack = _storage.ShopConfig.ShopItems.FirstOrDefault(x => x.Id == id);
                AddItemInInventory(itemInPack);
            }
        }
        else
        {
            AddItemInInventory(shopItem);
        }
        
        return true;
    }

    public void Sell(ShopItem shopItem)
    {
        AddCoins(shopItem.SellPrice);
        DeleteOrReduceItemFromInventory(shopItem);
    }

    private void AddItemInInventory(ShopItem shopItem)
    {
        var itemFromInventory = _storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == shopItem.Id);
        if (itemFromInventory == null)
        {
            var ownedItem = new OwnedItem
            {
                Id = shopItem.Id,
                Name = shopItem.Name,
                Amount = 1,
                BuyPrice = shopItem.BuyPrice,
                SellPrice = shopItem.SellPrice
            };
            _storage.PlayerInventory.PlayerItems.Add(ownedItem);
        }
        else
        {
            itemFromInventory.Amount++;
        }

        _dataProvider.SavePlayerInventory();
    }

    private void DeleteOrReduceItemFromInventory(ShopItem shopItem)
    {
        var itemFromInventory = _storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == shopItem.Id);
        if (itemFromInventory == null)
        {
            Debug.Log("Вещь не найдена в инвентаре игрока");
            return;
        }

        if (itemFromInventory.Amount == 1)
            _storage.PlayerInventory.PlayerItems.Remove(itemFromInventory);
        else
            itemFromInventory.Amount--;

        _dataProvider.SavePlayerInventory();
    }

    private void AddCoins(int coins)
    {
        if (coins < 0)
        {
            Debug.Log("Цена товара меньше 0");
            return;
        }

        _storage.PlayerInventory.Money += coins;
    }

    private void Spend(int coins)
    {
        if (coins < 0)
        {
            Debug.Log("Стоймость товара меньше 0");
            return;
        }

        _storage.PlayerInventory.Money -= coins;
    }
}