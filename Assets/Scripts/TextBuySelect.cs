using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBuySelect : MonoBehaviour
{
    string textUi;   
    // Start is called before the first frame update
    void Start()
    {
        textUi = PlayerPrefs.GetString("ShopBuyOrSelected");
        GetComponent<Text>().text = textUi;
    }
}
