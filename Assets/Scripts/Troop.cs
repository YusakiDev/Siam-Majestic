using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{
    #region Public Variables
    
    [Header("Public")]
    private Movement _movement;
    public float speed = 1f;
    public float maxDistanceToPoint = 0.1f;
    public bool isWalking = false;
    private bool _oneTime;

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
