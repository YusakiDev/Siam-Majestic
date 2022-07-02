using Unity.VisualScripting.FullSerializer.Internal.Converters;
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

    [SerializeField] GameObject troops;
    
    
    private int _turn = 1;
    private int _phase = 1; 
    private int _coins;

    #endregion
    

    private void Update()
    {
        _mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Select();
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
                }

                _secondPoint = rayHit.collider.gameObject;
                if (isSelectingPoint && _secondPoint == _firstPoint)
                {
                    _pointSpriteRenderer.color = Color.white;
                    _currentlySelectedPoint = null;
                    isSelectingPoint = false;
                    _firstPoint = null;
                    _secondPoint = null;
                }
                else if (isSelectingPoint && _secondPoint != _firstPoint)
                {
                    _pointSpriteRenderer.color = Color.white;
                    _pointSpriteRenderer = _secondPoint.GetComponent<SpriteRenderer>();
                    _pointSpriteRenderer.color = new Color(1f, 0.55f, 0.06f);
                    _firstPoint = _secondPoint;
                    _currentlySelectedPoint = _secondPoint.GetComponent<Point>();
                }
                else 
                {
                    _pointSpriteRenderer.color = new Color(1f, 0.55f, 0.06f);
                    isSelectingPoint = true;
                }
                

            }
        }
    }

    private void SeperateTroop()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (_currentlySelectedPoint.hasTroops)
            {
                
            }
        }
    }

    void NextPhase()
    {
        Debug.Log(_turn + " : " + _phase + " : " + _coins);
        _phase += 1;
        _coins += 1;
        if (_phase > 3)
        {
            _turn += 1;
            _phase = 1;
        }
    }
}
