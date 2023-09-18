using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class DataProvider : MonoBehaviour
{
    private const string _firstFileName = "PlayerInventory";
    private const string _secondFileName = "Shop";
    private const string _saveFileExtension = ".json";
    
    private IPlayerData _playerData;
    private IShopService _shopService;

    public DataProvider(IPlayerData playerData) => _playerData = playerData;
    private string _savePath => Application.persistentDataPath;
    private string _fullPathToInventory => Path.Combine(_savePath, $"{_firstFileName}{_saveFileExtension}");
    private string _fullPathToShop => Path.Combine(_savePath, $"{_secondFileName}{_saveFileExtension}");

    private bool IsDataFileInventoryExist() => File.Exists(_fullPathToInventory);
    private bool IsDataFileShopExist() => File.Exists(_fullPathToShop);
    
    public void Save()
    {
        File.WriteAllText(_fullPathToInventory, JsonConvert.SerializeObject(_playerData.PlayerInventory));
    }

    public bool LoadInventory()
    {
        if (IsDataFileInventoryExist() == false)
            return false;

        _playerData.PlayerInventory =
            JsonConvert.DeserializeObject<PlayerInventory>(File.ReadAllText(_fullPathToInventory));
        return true;
    }
    public bool LoadShop()
    {
        if (IsDataFileShopExist() == false)
            return false;

        _shopService.Shop = JsonConvert.DeserializeObject<Shop>(File.ReadAllText(_fullPathToShop));
        return true;
    }
    
}
