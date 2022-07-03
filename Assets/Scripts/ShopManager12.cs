using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;


public class ShopManager12 : MonoBehaviour
{
    
    
    [SerializeField] Button button3;
    [SerializeField] Button button5;
    [SerializeField] private TMP_Text _text;

    [SerializeField] private int sum;
    [SerializeField] private int coins = 2000;

    public GameObject objectToSpawn;

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

            for (int i = 0; i < 5; i++)
            {
                GameObject gameObject = new GameObject("GameObject" + i);
                transform.position = new Vector3(3, 2);
            }
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
            for (int i = 0; i < 10; i++)
            {
                GameObject gameObject = new GameObject("GameObject" + i);
                transform.position = new Vector3(3, 2);
            }
        }
        else
        {
            Debug.Log("Not enough Money");
        }
    }
}
