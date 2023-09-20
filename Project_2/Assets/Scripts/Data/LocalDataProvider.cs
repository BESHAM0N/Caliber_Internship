using System;
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

    private string GetFullPath(string fileName)
    {
        string fullPath = Path.Combine(_savePath, $"{fileName}{_saveFileExtension}");
        return fullPath;
    }

    private bool IsDataFileExist(string fullPath)
    {
        var result = File.Exists(fullPath);
        if (!result)
            Debug.Log($"Файл по пути {fullPath} не найден");
        return result;
    }

    public void SavePlayerInventory()
    {
        File.WriteAllText(GetFullPath(_inventoryFileName), JsonConvert.SerializeObject(_storage.PlayerInventory));
    }

    public bool LoadInventory()
    {
        var path = GetFullPath(_inventoryFileName);
        if (!IsDataFileExist(path))
            return false;

        var jsonText = File.ReadAllText(path);
        if (string.IsNullOrEmpty(jsonText))
        {
            Debug.Log("Файл пустой");

            return false;
        }

        try
        {
            _storage.PlayerInventory = JsonConvert.DeserializeObject<PlayerInventory>(jsonText);
        }
        catch (Exception ex)
        {
            Debug.Log("Не удалось разобрать json файл");
            throw ex;
        }

        return true;
    }

    public bool LoadShop()
    {
        var path = GetFullPath(_shopFileName);
        if (!IsDataFileExist(path))
            return false;

        var jsonText = File.ReadAllText(path);
        if (string.IsNullOrEmpty(jsonText))
        {
            Debug.Log("Файл пустой");

            return false;
        }

        try
        {
            _storage.ShopConfig = JsonConvert.DeserializeObject<Shop>(jsonText);
        }
        catch (Exception ex)
        {
            Debug.Log("Не удалось разобрать json файл");
            throw ex;
        }

        return true;
    }
    
}