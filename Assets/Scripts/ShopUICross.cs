using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUICross : MonoBehaviour
{
    public int LVLBuy;
    public int LVlplayer;
    public Text LVLBuyTxt;

    private void Start()
    {
        LVlplayer = PlayerPrefs.GetInt("Lvl");
    }

    public void UpdateLVL()
    {
    }

    void Update()
    {
        UpdateLVL();
        if (LVLBuy <= LVlplayer) gameObject.SetActive(false);
        LVLBuyTxt.text = LVLBuy.ToString();
    }
}
