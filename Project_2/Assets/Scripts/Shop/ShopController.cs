using UnityEngine;
public class ShopController : MonoBehaviour
{
    private ShopService _shopService;
    public void Initialize(ShopService shopService)
    {
        _shopService = shopService;
    }
    public void Buy(ShopItem itemToBuy)
    {
        _shopService.Buy(itemToBuy);
    }
    public void Sell(OwnedItem ownedItem)
    {
        _shopService.Sell(ownedItem);
    }
}