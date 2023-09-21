using System.Linq;
using UnityEngine;
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ShopController _shopController;
    
    private LocalDataProvider _dataProvider;

    public void Awake()
    {
        InitializeData();
        InitializeShop();
        BuyTest();
    }

    private void BuyTest()
    {
        Debug.Log(string.Join(',', Storage.ShopConfig.ShopItems.Select(x => x.Id)));
        Debug.Log(Storage.PlayerInventory.Money.ToString());
        var first = Storage.ShopConfig.ShopItems.FirstOrDefault(c => c.Id == "Young Wizard's pack");
        var result = _shopController.Buy(first!.Id);
        if (!result)
        {
            Debug.Log("Покупка не удалась");
        }
        Debug.Log(Storage.PlayerInventory.Money.ToString());
    }

    private void SellTest()
    {
        Debug.Log(Storage.PlayerInventory.Money.ToString());
        var first = Storage.ShopConfig.ShopItems.FirstOrDefault(c => c.Id == "Health Potion");
        _shopController.Sell(first!.Id);
        Debug.Log(Storage.PlayerInventory.Money.ToString());
    }

    private void InitializeData()
    {
        _dataProvider = new LocalDataProvider();
        LoadDataOrInit();
    }

    private void InitializeShop()
    {
       _shopController.Initialize(_dataProvider);
    }

    private void LoadDataOrInit()
    {
        if (!_dataProvider.LoadInventory())
            Storage.PlayerInventory = new PlayerInventory();
        if (!_dataProvider.LoadShop())
            Storage.ShopConfig = new Shop();
    }
}