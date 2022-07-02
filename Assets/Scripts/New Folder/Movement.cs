using System.Collections.Generic;
using UnityEngine;

public class Movement : Singleton<Movement>
{

    #region Public
    public int moveDirection = 1;
    public int pointMovingTo = 0;
    public bool backWards;
    public Transform[] pointsTransform; //All points in Path

    #endregion

    #region Methods

    private void OnDrawGizmos()
    {
        if (pointsTransform == null || pointsTransform.Length < 2)
        {
            return;
        }
        for (var i =1 ; i < pointsTransform.Length; i++)
        {
            Gizmos.DrawLine(pointsTransform[i - 1].position, pointsTransform[i].position);
        }
        
        Gizmos.DrawLine(pointsTransform[0].position, pointsTransform[pointsTransform.Length -1].position);
    }
    #endregion

    #region Corutines
    
    public IEnumerator<Transform> GetNextPoint()
    {
        if (pointsTransform == null || pointsTransform.Length < 1)
        {
            yield break;
        }
        if (backWards)
        {
            moveDirection = -1;
        }
        else
        {
            moveDirection = 1;
        }
        while (true)
        {
            yield return pointsTransform[pointMovingTo];
            if (pointsTransform.Length == 1)
            {
                continue;
            }

            if (pointMovingTo <= 0)
            {
                moveDirection = 1;
            }
            else if(pointMovingTo >= pointsTransform.Length - 1)
            {
                moveDirection = 0;
                //Do Something
            }
            pointMovingTo += moveDirection;
        }
    }

    #endregion

    
}

