using System;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Singleton<Movement>
{
    #region Public

    public Point[] allPoints;
    public int moveDirection = 1;
    public int pointMovingTo = 0;
    public bool backWards;
    public Transform[] pointsTransform; //All points in Path

    #endregion

    #region Methods

    private void OnValidate()
    {
        var i = 1;
        foreach (var point in gameObject.GetComponentsInChildren<Point>())
        {
            allPoints[i] = point;
            point.pointID = i;
            i++;

        }
    }

    private void OnDrawGizmos()
    {
        if (pointsTransform == null || pointsTransform.Length < 2)
        {
            return;
        }

        for (var i = 1; i < pointsTransform.Length; i++)
        {
            Gizmos.DrawLine(pointsTransform[i - 1].position, pointsTransform[i].position);
        }

        Gizmos.DrawLine(pointsTransform[0].position, pointsTransform[pointsTransform.Length - 1].position);
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
            else if (pointMovingTo >= pointsTransform.Length - 1)
            {
                moveDirection = 0;
                pointsTransform[0] = pointsTransform[1];
            }

            pointMovingTo += moveDirection;
        }
    }

    public bool CheckMoveAble(int pointID, int pointID2)
    {
        switch (pointID)
        {
            case 1:
                if (pointID2 == 2)
                {
                    return true;
                }

                break;
            case 2:
                if (pointID2 == 1 || pointID2 == 3 || pointID2 == 5 || pointID2 == 6)
                {
                    return true;
                }

                break;
            case 3:
                //2,4
                if (pointID2 == 2 || pointID2 == 4)
                {
                    return true;
                }

                break;
            case 4: //3,6,9
                if (pointID2 == 3 || pointID2 == 6 || pointID2 == 9)
                {
                    return true;
                }

                break;
            case 5: //2,6,7
                if (pointID2 == 2 || pointID2 == 6 || pointID2 == 7)
                {
                    return true;
                }

                break;
            case 6: //2,4,5,8,9,10
                if (pointID2 == 2 || pointID2 == 4 || pointID2 == 5 || pointID2 == 8 || pointID2 == 9 || pointID2 == 10)
                {
                    return true;
                }

                break;
            case 7: //5,8
                if (pointID2 == 5 || pointID2 == 8)
                {
                    return true;
                }

                break;
            case 8: //6,7,10
                if (pointID2 == 6 || pointID2 == 7 || pointID2 == 10)
                {
                    return true;
                }

                break;
            case 9: //4,7,10
                if (pointID2 == 4 || pointID2 == 7 || pointID2 == 10)
                {
                    return true;
                }

                break;
            case 10: // 6,8,9
                if (pointID2 == 6 || pointID2 == 8 || pointID2 == 9)
                {
                    return true;
                }

                break;
            case 11: //10
                if (pointID2 == 10)
                {
                    return true;
                }

                break;
            default:
                Debug.Log("You can't move to this point");
                return false;

        }

        Debug.Log("You can't move to this point");
        return false;


        #endregion

    }

}

