using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ShopManager : MonoBehaviour
{

    public int[,] shopItems = new int[5,5];

    public float coins;

    public TextMeshProUGUI CoinsTXT;
    // Start is called before the first frame update
    void Start()
    {
        CoinsTXT.text = "Coins:" + coins.ToString();

        //ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        //price
        shopItems[2, 1] = 3;
        shopItems[3, 2] = 5;

        //quantity //store solider data
        shopItems[3, 1] = 0;
        


    }

    // Update is called once per frame
    public void Buy()
    {
        //call button ref to use in this function
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]+=5;
            CoinsTXT.text = "Coins:" + coins.ToString();
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text =
                shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
            
        }


    }
}
