using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{
    #region Public Variables
    
    [Header("Public")]
    private Movement _path;
    public float speed = 1f;
    public float maxDistacneToPoint = 0.1f;
    public bool isWalking = false;

    #endregion
    
    #region Private Variables
    
    [Header("Private")]
    private IEnumerator<Transform> _nextPoint;
    private bool _isCurrentNull;

    #endregion

    #region Main Methods

    private void Awake()
    {
        _path = Movement.Instance;
    }

    private void Start()
    {
        Walk();
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
        transform.position = Vector3.MoveTowards(transform.position, _nextPoint.Current.position, Time.deltaTime * speed);
        var distanceSquared = (transform.position - _nextPoint.Current.position).sqrMagnitude;
        if (distanceSquared < maxDistacneToPoint * maxDistacneToPoint) //If you are close enough
        {
            _nextPoint.MoveNext(); //Go to the next point
            Debug.Log("Log");
        }
    }

    public void Walk()
    {
        if (isWalking)
        {
            return;
        }

        isWalking = true;
        _nextPoint = _path.GetNextPoint();
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
