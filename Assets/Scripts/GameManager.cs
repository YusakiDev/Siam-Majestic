using System;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region variables
    
    [SerializeField] Camera mainCamera;
    [SerializeField] private LayerMask allTilesLayer;
    public bool isSelectingPoint;
    private Vector2 _mousePos;
    private SpriteRenderer _pointSpriteRenderer;
    Point _currentlySelectedPoint;
    private GameObject _firstPoint;
    private GameObject _secondPoint;

    [SerializeField] Troop troopsPrefab;
    [SerializeField] Troop troops;

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
                if (!isSelectingPoint)
                {
                     _firstPoint = rayHit.collider.gameObject;
                     _currentlySelectedPoint = _firstPoint.GetComponent<Point>();
                    _pointSpriteRenderer = _firstPoint.GetComponent<SpriteRenderer>();
                    _uiManager.OpenSelectionUI(_currentlySelectedPoint);
                }

                _secondPoint = rayHit.collider.gameObject;
                if (isSelectingPoint && _secondPoint == _firstPoint)
                {
                    _pointSpriteRenderer.color = Color.white;
                    _currentlySelectedPoint = null;
                    isSelectingPoint = false;
                    _firstPoint = null;
                    _secondPoint = null;
                    _uiManager.CloseSelectionUI();
                }
                else if (isSelectingPoint && _secondPoint != _firstPoint)
                {
                    _pointSpriteRenderer.color = Color.white;
                    _pointSpriteRenderer = _secondPoint.GetComponent<SpriteRenderer>();
                    _pointSpriteRenderer.color = new Color(1f, 0.55f, 0.06f);
                    _firstPoint = _secondPoint;
                    _currentlySelectedPoint = _secondPoint.GetComponent<Point>();
                    _uiManager.OpenSelectionUI(_currentlySelectedPoint);
                }
                else 
                {
                    _pointSpriteRenderer.color = new Color(1f, 0.55f, 0.06f);
                    isSelectingPoint = true;
                }
                

            }
            else
            {
                if (_firstPoint != null)
                {
                    _pointSpriteRenderer.color = Color.white;
                    isSelectingPoint = false;
                    _firstPoint = null;
                    _secondPoint = null;
                    _uiManager.CloseSelectionUI();
                }
                
            }
        }
    }

    private void MoveToSelected()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && !troops.isWalking)
        {
            _movement.pointsTransform[1] = _currentlySelectedPoint.transform;
            _movement.pointMovingTo = 0;
            troops.Walk();
        }
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
