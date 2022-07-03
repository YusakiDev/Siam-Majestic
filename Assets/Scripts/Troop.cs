using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{
    
    UIManager _uiManager;
    GameManager _gameManager;
    #region Public Variables
    
    [Header("Public")]
    private Movement _movement;
    public float speed = 5f;
    public float maxDistanceToPoint = 0.1f;
    public int troopCount;
    public bool isWalking = false;
    private bool _oneTime;
    public bool isAlly;

    #endregion
    
    #region Private Variables
    
    [Header("Private")]
    private IEnumerator<Transform> _nextPoint;
    private bool _isCurrentNull;

    #endregion

    #region Main Methods

    private void Awake()
    {
        _movement = Movement.Instance;
        _uiManager = UIManager.Instance;
        _gameManager = GameManager.Instance;
    }
    

    void Update()
    {
        if (_nextPoint == null || _isCurrentNull)
        {
            return; //Exit if no path is found
            
        }
        Move();
    }
    #endregion

    #region Functions Methods
    
    void Move()
    {
        if (isWalking)
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextPoint.Current.position, Time.deltaTime * speed);
            var distanceSquared = (transform.position - _nextPoint.Current.position).sqrMagnitude;
            if (distanceSquared < maxDistanceToPoint * maxDistanceToPoint) //If you are close enough
            {
                if (distanceSquared == 0 && !_oneTime)
                {
                    _oneTime = true;
                    _nextPoint.MoveNext();
                }
                else if (distanceSquared == 0)
                {
                    _nextPoint.MoveNext();
                    isWalking = false;
                    _gameManager.canSelectPoint = true;
                    _nextPoint.Current.gameObject.GetComponent<Point>().troopsCount += troopCount;
                    _gameManager.UpdateTroopCount();
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Walk()
    {
        var point1 = _movement.pointsTransform[0].gameObject.GetComponent<Point>().pointID;
        var point2 = _movement.pointsTransform[1].gameObject.GetComponent<Point>().pointID;
        Debug.Log(point1 + " : " + point2);
        Debug.Log(_movement.CheckMoveAble(point1 , point2));
        if (isWalking || !_movement.CheckMoveAble(point1 , point2))
        {
            return;
        }

        _oneTime = false;
        isWalking = true;
        _gameManager.canSelectPoint = false;
        _nextPoint = _movement.GetNextPoint();
        _nextPoint.MoveNext();
        Debug.Log(_nextPoint.Current);
        _isCurrentNull = _nextPoint.Current == null;
        
        if (_isCurrentNull)
        {
            Debug.LogError("A path must have points in it to follow", gameObject);
            return; //Exit Start() if there is no point to move to
        }
        transform.position = _nextPoint.Current.position;
    }
    
    
    
    #endregion
}
