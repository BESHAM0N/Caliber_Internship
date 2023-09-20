using System.Linq;
using UnityEngine;
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ShopService _shopService;
    [SerializeField] private ShopController _shopController;

    private Storage _storage;
    private LocalDataProvider _dataProvider;

    public void Awake()
    {
        InitializeData();
        InitializeShop();
        BuyTest();
    }

    private void BuyTest()
    {
        Debug.Log(string.Join(',', _storage.ShopConfig.ShopItems.Select(x => x.Id).ToList()));
        Debug.Log(_storage.PlayerInventory.Money.ToString());                           
        var result = _shopService.Buy(_storage.ShopConfig.Packs.FirstOrDefault(c => c.Id == "Young Wizard's pack"));
        if (!result)
        {
            Debug.Log("Покупка не удалась");
        }
        Debug.Log(_storage.PlayerInventory.Money.ToString());
    }

    private void SellTest()
    {
        Debug.Log(_storage.PlayerInventory.Money.ToString());
        _shopService.Sell(_storage.ShopConfig.ShopItems.FirstOrDefault(c => c.Id == "Recipe"));
        Debug.Log(_storage.PlayerInventory.Money.ToString());
    }

    private void InitializeData()
    {
        _storage = new Storage();
        _dataProvider = new LocalDataProvider(_storage);
        LoadDataOrInit();
    }

    private void InitializeShop()
    {
        _shopService.Initialize(_storage, _dataProvider);
        _shopController.Initialize(_shopService);
    }

    private void LoadDataOrInit()
    {
        if (!_dataProvider.LoadInventory())
            _storage.PlayerInventory = new PlayerInventory();
        if (!_dataProvider.LoadShop())
            _storage.ShopConfig = new Shop();
    }
}