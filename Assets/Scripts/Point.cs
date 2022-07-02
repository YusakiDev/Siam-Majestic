using System.ComponentModel.Design;
using UnityEngine;

public class Point : MonoBehaviour
{
    private GameManager _gameManager;
    SpriteRenderer _spriteRenderer;
    public bool hasTroops;
    public int troopsCount;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void OnMouseEnter()
    {
        CheckIfHasTroops();
        if (!_gameManager.isSelectingPoint)
        {
            _spriteRenderer.color = new Color(1f, 0.78f, 0.35f, 0.71f);
        }
    }

    private void OnMouseExit()
    {
        if (!_gameManager.isSelectingPoint)
        {
            _spriteRenderer.color = Color.white;
            
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
