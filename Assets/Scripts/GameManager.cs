using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region variables
    
    [SerializeField] Camera mainCamera;
    [SerializeField] private LayerMask allTilesLayer;
    public bool isSelectingPoint;
    public Vector2 _mousePos;
    private SpriteRenderer _pointSpriteRenderer;
    Point _currentlySelectedPoint;
    public GameObject _firstPoint;
    public GameObject _secondPoint;
    
    

    [SerializeField] GameObject troopsPrefab;
    [SerializeField] Troop troops;

    private bool _selectUIIsOpen;

    private UIManager _uiManager;
    Movement _movement;
    
    private int _turn = 1;
    private int _phase = 1; 
    private int _coins;

    #endregion


    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _movement = Movement.Instance;
    }

    private void Start()
    {
        Default();
    }

    void Default()
    {
        _movement.allPoints[4].troopsCount = 5;
        _movement.allPoints[1].troopsCount = 5;
        _movement.allPoints[2].troopsCount = 5;
        _movement.allPoints[10].troopsCount = 5;
        _movement.allPoints[9].troopsCount = 5;
        _movement.allPoints[8].troopsCount = 5;
        UpdateTroopCount();
    }

    void UpdateTroopCount()
    {
        var i = 0;
        foreach (var VARIABLE in _movement.allPoints)
        {
            _uiManager.text = _uiManager.gameObject.transform.GetChild(i).GetComponent<TMP_Text>();
            _uiManager.text.text = VARIABLE.troopsCount.ToString();
            i++;
        }
    }
    private void Update()
    {
        _mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Select();
        MoveToSelected();
        
    }

    private void Select()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(_mousePos, Vector2.zero, Mathf.Infinity, allTilesLayer);
            if (rayHit.collider != null)
            {
                var go = rayHit.collider.gameObject;
                if (_firstPoint == null)
                {
                    _firstPoint = rayHit.collider.gameObject;
                    _firstPoint.GetComponent<SpriteRenderer>().color = new Color(1f, 0.55f, 0.06f);
                    _uiManager.OpenSelectionUI(_firstPoint.GetComponent<Point>());
                    _selectUIIsOpen = true;
                }

                if (_secondPoint == null)
                {
                    _secondPoint = rayHit.collider.gameObject;
                    _secondPoint.GetComponent<SpriteRenderer>().color = new Color(1f, 0.55f, 0.06f);
                    _uiManager._isSecondPointSelected = true;
                }
            } //สวัสดีปีใหม่
            
            else
            {
                if (isSelectingPoint && _selectUIIsOpen)
                {
                    Debug.Log("UI is open Right click to close");
                }

            }
        }
    }

    private void MoveToSelected()
    {
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (_firstPoint != null)
            {
                Debug.Log("Cancel UI Click");
                _firstPoint.GetComponent<SpriteRenderer>().color = Color.white;
                _firstPoint = null;
                _uiManager.CloseSelectionUI();
            }

            if (_secondPoint != null)
            {
                Debug.Log("Cancel UI Click");
                _secondPoint.GetComponent<SpriteRenderer>().color = Color.white;
                _secondPoint = null;
                _uiManager.CloseSelectionUI();
            }
                
        }
    }

    public void MoveTroops(int number, Point point)
    {
        GameObject go = Instantiate(troopsPrefab, _firstPoint.transform.position, Quaternion.identity);
        Troop troop = go.GetComponent<Troop>();
        _movement.pointsTransform[1] = point.transform;
        _movement.pointsTransform[0] = _firstPoint.transform;
        _movement.pointMovingTo = 0;
        troop.Walk();
        troop.troopCount = number;
        Debug.Log(number);
       
    }
    void NextPhase()
    {
        Debug.Log(_turn + " : " + _phase + " : " + _coins);
        _phase += 1;
        _coins += 2;
        if (_phase > 3)
        {
            _turn += 1;
            _phase = 1;
        }
    }
}
