using System;


public class Wallet
{
    public event Action<int> CoinsChanged;

    private IPlayerData _playerData;

    public Wallet(IPlayerData playerData) => _playerData = playerData;

    public int GetCurrentCoins() => _playerData.PlayerInventory.Money;

    public void AddCoins(int coins)
    {
        if(coins<0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _playerData.PlayerInventory.Money += coins;
        
        CoinsChanged?.Invoke(_playerData.PlayerInventory.Money);
    }

    public bool IsEnough(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));
        return _playerData.PlayerInventory.Money >= coins;
    }

    public void Spend(int coins)
    {
        if(coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));
        _playerData.PlayerInventory.Money -= coins;
        
        CoinsChanged?.Invoke(_playerData.PlayerInventory.Money);
    }
    
}
