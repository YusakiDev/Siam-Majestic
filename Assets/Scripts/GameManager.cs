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
    public Vector2 mousePos;
    private SpriteRenderer _pointSpriteRenderer;
    Point _currentlySelectedPoint;
    public GameObject firstPoint;
    public GameObject secondPoint;
    
    

    [SerializeField] GameObject troopsPrefab;
    [SerializeField] Troop troops;

    private bool _selectUIIsOpen;
    public bool canSelectPoint = true;

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

    public void UpdateTroopCount()
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
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Select();
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            ClearSelected();
        }
        

    }

    private void Select()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && canSelectPoint)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, allTilesLayer);
            if (rayHit.collider != null)
            {
                if(firstPoint != null && secondPoint != null)
                {
                    firstPoint.GetComponent<SpriteRenderer>().color = Color.red;
                    secondPoint.GetComponent<SpriteRenderer>().color = Color.red;
                    firstPoint = null;
                    secondPoint = null;
                    _uiManager._isSecondPointSelected = false;
                    _uiManager.CloseSelectionUI();
                    _selectUIIsOpen = false;
                }
                if (firstPoint != secondPoint && firstPoint != null)
                {
                    if (secondPoint == null)
                    {
                        Debug.Log("second point");
                        secondPoint = rayHit.collider.gameObject;
                        _movement.pointsTransform[1] = secondPoint.transform;
                        secondPoint.GetComponent<SpriteRenderer>().color = new Color(1f, 0.55f, 0.06f);
                        _uiManager._isSecondPointSelected = true;
                        var point1 = _movement.pointsTransform[0].gameObject.GetComponent<Point>();
                        var point2 = _movement.pointsTransform[1].gameObject.GetComponent<Point>();
                        if(_movement.CheckMoveAble(point1.pointID, point2.pointID) && !point1.hasMoved && point1.hasTroops)
                        {
                            _uiManager.selectionUIConfirm.SetActive(true);
                        }
                    }
                }
                
                if (firstPoint == null)
                {
                    Debug.Log("first point");
                    firstPoint = rayHit.collider.gameObject;
                    _movement.pointsTransform[0] = firstPoint.transform;
                    firstPoint.GetComponent<SpriteRenderer>().color = new Color(1f, 0.55f, 0.06f);
                    _uiManager.OpenSelectionUI(firstPoint.GetComponent<Point>());
                    _selectUIIsOpen = true;
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

    public void ClearSelected()
    {
        if (firstPoint != null)
        {
                Debug.Log("Cancel UI Click");
                firstPoint.GetComponent<SpriteRenderer>().color = Color.red;
                firstPoint = null;
                _uiManager.CloseSelectionUI();
        }

        if (secondPoint != null) 
        {
                Debug.Log("Cancel UI Click");
                secondPoint.GetComponent<SpriteRenderer>().color = Color.red;
                secondPoint = null;
                _uiManager.CloseSelectionUI();
        }
                
        
    }

    public void MoveTroops(int number, Point point)
    {
        GameObject go = Instantiate(troopsPrefab, firstPoint.transform.position, Quaternion.identity);
        Troop troop = go.GetComponent<Troop>();
        _movement.pointsTransform[1] = point.transform;
        _movement.pointsTransform[0] = firstPoint.transform;
        _movement.pointMovingTo = 0;
        var point1 = _movement.pointsTransform[0].gameObject.GetComponent<Point>().pointID;
        var point2 = _movement.pointsTransform[1].gameObject.GetComponent<Point>().pointID;
        if(_movement.CheckMoveAble(point1, point2))
        {
            troop.Walk();
            troop.troopCount = number;
        }
        Debug.Log(number);
       
    }
    void NextPhase()
    {
        Debug.Log(_turn + " : " + _phase + " : " + _coins);
        _phase += 1;
        if (_phase > 3)
        {
            _coins += 2;
            _turn += 1;
            _phase = 1;
        }
    }
}
