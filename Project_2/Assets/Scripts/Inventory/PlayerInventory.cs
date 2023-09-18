using System;
using System.Collections.Generic;
public class PlayerInventory
{
    //public int Money {get; set;}
    public List<OwnedItems> PlayerItems {get; set;}

    private int _money = 1000;

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
    
