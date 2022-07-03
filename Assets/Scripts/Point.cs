using UnityEngine;

public class Point : MonoBehaviour
{
    private GameManager _gameManager;
    private Movement _movement;
    SpriteRenderer _spriteRenderer;
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
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void OnMouseEnter()
    {
        if (_gameManager.phase == GameManager.Phase.Move)
        {
            CheckIfHasTroops();
            if (_gameManager.firstPoint == null)
            {
                _spriteRenderer.color = new Color(1f, 0.78f, 0.35f, 0.71f);
            }
        }
    }

    private void OnMouseExit()
    {
        if (_gameManager.phase == GameManager.Phase.Move)
        {
            if (_gameManager.firstPoint == null)
            { 
                _spriteRenderer.color = Color.red;
            }
        }
    }
    
    void CheckIfHasTroops()
    {
        if (troopsCount > 0)
        {
            hasTroops = true;
        }
        else
        {
            hasTroops = false;
        }
    }
}
