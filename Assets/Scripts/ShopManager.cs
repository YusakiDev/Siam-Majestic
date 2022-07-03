using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{
    
    
    [SerializeField] Button button3;
    [SerializeField] Button button5;
    [SerializeField] private TMP_Text _text;

    [SerializeField] private int sum;
    [SerializeField] private int coins = 2000;

    private void Start()
    {
        button3.onClick.AddListener(Button3);
        button5.onClick.AddListener(Button5);
    }

    private void Button3()
    {
        if (coins >= 3)
        {
            coins -= 3;
            sum += 5;
            _text.text = $"Coins: {coins} Sum: {sum}";
        }

        else
        {
            Debug.Log("Not enough Money");
        }
    }
    
    private void Button5()
    {
        if (coins >= 5)
        {
            coins -= 5;
            sum += 10;
            _text.text = $"Coins: {coins} Sum: {sum}";
        }
        else
        {
            Debug.Log("Not enough Money");
        }
    }
}
