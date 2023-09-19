using System.Linq;
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
            return false;
        }

        Spend(shopItem.BuyPrice);
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
        OwnedItem first = _storage.PlayerInventory.PlayerItems.SingleOrDefault(x => x.Id == shopItem.Id);
        if (first == null)
        {
            OwnedItem ownedItem = new OwnedItem()
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

        _dataProvider.Save();
    }

    private void DeleteOrReduceItemFromInventory(ShopItem shopItem)
    {
        OwnedItem first = _storage.PlayerInventory.PlayerItems.SingleOrDefault(x => x.Id == shopItem.Id);

        if (first.Amount == 1)
        {
            _storage.PlayerInventory.PlayerItems.Remove(first);
        }
        else
            first.Amount--;

        _dataProvider.Save();
    }

    public void AddCoins(int coins)
    {
        if (coins < 0)
        {
            Debug.Log("Сумма меньше 0");
            return;
        }

        _storage.PlayerInventory.Money += coins;
    }

    public void Spend(int coins)
    {
        if (coins < 0)
        {
            Debug.Log("Сумма меньше 0");
            return;
        }

        _storage.PlayerInventory.Money -= coins;
    }
}