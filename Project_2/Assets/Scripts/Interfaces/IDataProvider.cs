using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataProvider
{
    public void Save();
    public bool LoadInventory();
    public bool LoadShop();
}
