using System;
using System.Linq;
using UnityEngine;
public class ShopController : MonoBehaviour
{
    private IPlayerData _playerData;
    private IShopService _shopService;
    private IDataProvider _dataProvider;

    public void Initialize(IPlayerData playerData, IShopService shopService,
        IDataProvider dataProvider)
    {
        _playerData = playerData;
        _shopService = shopService;
        _dataProvider = dataProvider;
    }

    public bool Buy(ShopItem shopItem)
    {
        if (_playerData.PlayerInventory.Money < shopItem.BuyPrice)
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
        OwnedItem first = _playerData.PlayerInventory.PlayerItems.SingleOrDefault(x => x.Id == shopItem.Id);
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
            _playerData.PlayerInventory.PlayerItems.Add(ownedItem);
        }
        else
        {
            first.Amount++;
        }

        _dataProvider.Save();
    }

    private void DeleteOrReduceItemFromInventory(ShopItem shopItem)
    {
        OwnedItem first = _playerData.PlayerInventory.PlayerItems.SingleOrDefault(x => x.Id == shopItem.Id);

        if (first.Amount == 1)
        {
            _playerData.PlayerInventory.PlayerItems.Remove(first);
        }
        else
            first.Amount--;

        _dataProvider.Save();
    }
    
    public void AddCoins(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _playerData.PlayerInventory.Money += coins;
    }

    public void Spend(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));
        _playerData.PlayerInventory.Money -= coins;
    }
}