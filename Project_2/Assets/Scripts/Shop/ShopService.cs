using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShopService : MonoBehaviour
{
    private Storage _storage;
    private LocalDataProvider _dataProvider;

    public void Initialize(Storage storage,
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
            foreach (var id in pack.Items)
            {
                var itemInPack = _storage.ShopConfig.ShopItems.FirstOrDefault(x => x.Id == id);
                AddItemInInventory(itemInPack);
            }
        }
        else
            AddItemInInventory(shopItem);
        return true;
    }

    public void Sell(ShopItem shopItem)
    {
        AddCoins(shopItem.SellPrice);
        DeleteOrReduceItemFromInventory(shopItem);
    }

    private void AddItemInInventory(ShopItem shopItem)
    {
        var first = _storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == shopItem.Id);
        if (first == null)
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
            first.Amount++;
        }

        _dataProvider.SavePlayerInventory();
    }

    private void DeleteOrReduceItemFromInventory(ShopItem shopItem)
    {
        var first = _storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == shopItem.Id);

        if (first == null)
        {
            Debug.Log("Вещь не найдена в инвентаре игрока");
            return;
        }

        if (first.Amount == 1)
        {
            _storage.PlayerInventory.PlayerItems.Remove(first);
        }
        else
            first.Amount--;

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