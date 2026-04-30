using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerInventory : MonoBehaviour
{
    public UIManager uIManager;

    private int _numKeys = 0;
    public int NumKeys
    {
        get
        {
            return _numKeys;
            
        }
        set
        {
            _numKeys += value;
        }
    }

    private int _numCoins = 0;
    public int NumCoins
    {
        get
        {
            return _numCoins;
        }
        set
        {
            _numCoins += value;
        }
    }

    //adds a key to inventory and updates UI element
    public void CollectKey(int numKeys)
    {
        NumKeys = numKeys;
        uIManager.Keys.text = "x " + NumKeys;
    }

    //decrements a key from inventory and updates UI element
    public bool UseKey()
    {
        if (NumKeys > 0)
        {
            NumKeys = -1;
            uIManager.Keys.text = "x " + NumKeys;
            return true;
        }
        return false;
    }

    //adds a coin to the inventory
    public void AddCoin(int numCoins)
    {
        NumCoins = numCoins;
        uIManager.Coins.text = "x " + NumCoins;
    }
}
