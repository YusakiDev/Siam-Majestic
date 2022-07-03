using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Lumin;


public enum BattleState { START, PLAYERTURN,ENEMYTURN,PLAYERWON,ALIEANWON }
public class BattleSystem : MonoBehaviour
{
    // public GameObject playerPrefab;
    // public GameObject enemyPrefab;

    // public Transform playerBattleStation;
    // public Transform enemyBattleStation;

    // private Unit playerUnit;
    // private Unit enemyUnit;

    //public TMP_Text dialogueText;


    public ShopManager playerHUD;
    public ShopManager enemyHUD;
    
        
    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
        
    }
     
    //ตอนเริ่มฉากมามเป็นยังไงมึงก็เขียนตรงนี้
     void SetupBattle()  
    {
        // transform.position = new Vector3(3, 2);
        // GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        // playerUnit = playerGO.GetComponent<Unit>();
        //
        // transform.position = new Vector3(-3, 2);
        // GameObject enemyGO = Instantiate(playerPrefab, playerBattleStation);
        // enemyUnit = enemyGO.GetComponent<Unit>();
        

        // dialogueText.text = "A wild " + enemyUnit + "approches...";

        

        state = BattleState.PLAYERTURN;
        ShopManager();
    }

    void ShopManager()  
    {
        //dialogueText.text = "Choose an action: ";
    }

    IEnumerator PlayerTurn()
    {
        
        //Write the funtion Buy phraseand etc here
        yield return new WaitForSeconds(2f);
        
        //if (playerbuy)
    //     {
    //         state = BattleState.PLAYERTURN;
    //     }
    //     if (player use skill)
    //     {
    //         state = BattleState.ALIEANTURN;
    //     }
    //
    //     if(player use Move)
    //     {
    //         state = BattleState.ALIEANTURN;
    //     }
    //
    //     if (playerkilled ALiean)
    //     {
    //         state = BattleState.PLAYERWON;
    //     }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2f);
        //if (alieanbuy)
        //     {
        //         state = BattleState.ALIEANTURN;
        //     }
        //     if (aliean use skill)
        //     {
        //         state = BattleState.PLAYERTURN;
        //     }
        //
        //     if(aliean use Move)
        //     {
        //         state = BattleState.PLAYERTURN;
        //     }
        //
        //     if (Aliean killed player)
        //     {
        //         state = BattleState.ALIEANWON;
        //     }
    }
    void EndBattle()
    {
        if (state == BattleState.PLAYERWON)
        {
            //youwon
        }
        else if (state == BattleState.ALIEANWON)
        {
            //playerLost
        }
    }
    
    //OnPlayer action
    public void OnPlayerTurn()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerTurn());

    }  
    
    //this function is just for incase use function to instatiate the action of player instead of if case
    // public void OnPlayerTudrn()
    // {
    //     if (state != BattleState.PLAYERTURN)
    //     {
    //         return;
    //     }
    //
    //     StartCoroutine(PlayerTurn());
    //
    // }
    
    
    
    
    
    
    
    //OnALiean action
    public void OnEnemyTurn()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(EnemyTurn());

    }
}
