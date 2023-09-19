using System.Linq;
using UnityEngine;
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ShopController _shopController;
    
    private IPlayerData _playerData;
    private IDataProvider _dataProvider;
    private IShopService _shopService;

    public void Awake()
    {
        InitializeData();
        InitializeShop();
        SellTest();
    }

    private void BuyTest()
    {
        Debug.Log(string.Join(',', _shopService.Shop));
        Debug.Log(_playerData.PlayerInventory.Money.ToString());
        _shopController.Buy(_shopService.Shop.ShopItems.SingleOrDefault(c => c.Id == 1));
        Debug.Log(_playerData.PlayerInventory.Money.ToString());
    }

    private void SellTest()
    {
        Debug.Log(_playerData.PlayerInventory.Money.ToString());
        _shopController.Sell(_shopService.Shop.ShopItems.SingleOrDefault(c => c.Id == 1));
        Debug.Log(_playerData.PlayerInventory.Money.ToString());
    }
    

    private void InitializeData()
    {
        _playerData = new PlayerData();
        _shopService = new ShopService();
        
        _dataProvider = new LocalDataProvider(_playerData, _shopService);
        LoadDataOrInit();
    }

    private void InitializeShop()
    {
        _shopController.Initialize(_playerData, _shopService, _dataProvider);
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.LoadInventory() == false)
            _playerData.PlayerInventory = new PlayerInventory();
        _dataProvider.LoadShop();
    }
}