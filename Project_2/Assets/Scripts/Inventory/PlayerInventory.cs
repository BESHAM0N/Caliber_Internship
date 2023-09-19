using System;
using System.Collections.Generic;

public class PlayerInventory
{
    public List<OwnedItem> PlayerItems { get; set; } = new();
    private int _money = 100;

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