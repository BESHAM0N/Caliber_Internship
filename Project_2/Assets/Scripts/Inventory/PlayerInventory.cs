using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    public List<OwnedItem> PlayerItems { get; set; } = new();
    private int _money = 1000;
       
    public int Money
    {
        get => _money;
        set
        {
            if (value < 0)
                Debug.Log("Количество монет не может быть отрицательным");
            _money = value;
        }
    }
}