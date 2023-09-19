using System.IO;
using UnityEngine;
using Newtonsoft.Json;
public class LocalDataProvider 
{
    private const string _inventoryFileName = "PlayerInventory";
    private const string _shopFileName = "Shop";
    private const string _saveFileExtension = ".json";
    private Storage _storage;
    
    public LocalDataProvider(Storage storage)
    {
        _storage = storage;
    }
    
    private string _savePath => Application.dataPath;
    private string _fullPathToInventory => Path.Combine(_savePath, $"{_inventoryFileName}{_saveFileExtension}");
    private string _fullPathToShop => Path.Combine(_savePath, $"{_shopFileName}{_saveFileExtension}");

    private bool IsDataFileInventoryExist() => File.Exists(_fullPathToInventory);
    private bool IsDataFileShopExist() => File.Exists(_fullPathToShop);

    public void Save()
    {
        File.WriteAllText(_fullPathToInventory, JsonConvert.SerializeObject(_storage.PlayerInventory));
    }

    public bool LoadInventory()
    {
        if (IsDataFileInventoryExist() == false)
            return false;

        _storage.PlayerInventory 
            = JsonConvert.DeserializeObject<PlayerInventory>(File.ReadAllText(_fullPathToInventory));
        return true;
    }

    public bool LoadShop()
    {
        if (IsDataFileShopExist() == false)
            return false;
        
        _storage.ShopConfig = JsonConvert.DeserializeObject<Shop>(File.ReadAllText(_fullPathToShop));
        
        return true;
    }
}