using System;
using System.Collections.Generic;

public class PlayerInventory
{
    public List<OwnedItem> PlayerItems { get; set; }

    private int _money;

    public PlayerInventory()
    {
        _money = 100;
        PlayerItems = new();
    }

    public int Money
    {
        get => _money;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _money = value;
        }
    }
}