using UnityEngine;

public class Point : MonoBehaviour
{
    private GameManager _gameManager;
    private Movement _movement;
    public SpriteRenderer spriteRenderer;
    public int pointID;
    public bool hasTroops;
    public int troopsCount;
    public int selectedTroopCount;
    public bool hasMoved = false;
    public bool isAlly;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _movement = Movement.Instance;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void OnMouseEnter()
    {
        if (_gameManager.phase == GameManager.Phase.Move)
        {
            CheckIfHasTroops();
            if (_gameManager.firstPoint == null)
            {
                if (isAlly)
                {
                    spriteRenderer.sprite = _gameManager.pointSprites[5];
                }
                else
                {
                    spriteRenderer.sprite = _gameManager.pointSprites[4];
                }

                if (!hasTroops)
                {
                    spriteRenderer.sprite = _gameManager.pointSprites[3];
                }
            }
        }
    }

    private void OnMouseExit()
    {
        if (_gameManager.phase == GameManager.Phase.Move)
        {
            if (_gameManager.firstPoint == null)
            { 
                if (isAlly)
                {
                    spriteRenderer.sprite = _gameManager.pointSprites[2];
                }
                else
                {
                    spriteRenderer.sprite = _gameManager.pointSprites[1];
                }

                if (!hasTroops)
                {
                    spriteRenderer.sprite = _gameManager.pointSprites[0];
                }
            }
        }
    }
    
    public void CheckIfHasTroops()
    {
        if (troopsCount > 0)
        {
            hasTroops = true;
            if (isAlly)
            {
                spriteRenderer.sprite = _gameManager.pointSprites[2];
            }
            else
            {
                spriteRenderer.sprite = _gameManager.pointSprites[1];
            }
        }
        else
        {
            hasTroops = false;
            spriteRenderer.sprite = _gameManager.pointSprites[0];
        }
    }
}
