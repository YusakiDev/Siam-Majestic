using System;
using TMPro;
using UnityEngine;
using UnityEngine.Apple;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private GameManager _gameManager;
    Movement _movement;
    [SerializeField] private GameObject troopsText;
    public TMP_Text text;
    public Movement movement;
    
    [Header("SelectionUI")]
    [SerializeField] private GameObject selectionUI;
    [SerializeField] private Button selectionUIPlus;
    [SerializeField] private Button selectionUIMinus;
    [SerializeField] private TMP_Text selectionUIText;
    [SerializeField] public GameObject selectionUIConfirm;
    
    public bool _isSecondPointSelected = false;

    public Point selectedPoint;

    private void Awake()
    {
        _movement = Movement.Instance;
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        selectionUIPlus.onClick.AddListener(() =>
        {
            SelectionUIPlus(selectedPoint);
        });
        selectionUIMinus.onClick.AddListener(() =>
        {
            SelectionUIMinus(selectedPoint);
        });
        selectionUIConfirm.GetComponent<Button>().onClick.AddListener(() =>
        {
            SelectionUIConfirm(selectedPoint);
        });
    }

    private void OnValidate()
    {
        if (gameObject.transform.childCount == movement.allPoints.Length)
        {
            return;
            
        }
        else
        {
            foreach (var point in movement.allPoints)
            {
                var go = Instantiate(troopsText, point.transform);
                go.transform.parent = transform;
                go.GetComponent<RectTransform>().localScale = Vector3.one;
                go.name = "TroopsCount: " + point.pointID;
                go.GetComponent<TMP_Text>().text = point.troopsCount.ToString();
            }
        }
    }

    public void OpenSelectionUI(Point point)
    {
        selectionUIConfirm.SetActive(false);
        selectionUI.GetComponent<RectTransform>().position = point.transform.position + new Vector3(0,2,0);
        selectedPoint = point;
        selectionUI.SetActive(true);
        if (selectedPoint.troopsCount > 0)
        {
            selectedPoint.selectedTroopCount = selectedPoint.troopsCount;
            selectionUIText.text = selectedPoint.troopsCount.ToString();
            selectionUIPlus.interactable = true;
            selectionUIMinus.interactable = true;
        }
        else
        {
            selectionUIText.text = "0";
            selectionUIPlus.interactable = false;
            selectionUIMinus.interactable = false;
        }
    }
    
    public void CloseSelectionUI()
    {
        selectionUI.SetActive(false);
    }
    public void SelectionUIPlus(Point point)
    {
        point.selectedTroopCount++;
        if(point.selectedTroopCount > point.troopsCount)
        {
            point.selectedTroopCount = point.troopsCount;
        }
        selectionUIText.text = point.selectedTroopCount.ToString();
    }
    public void SelectionUIMinus(Point point)
    {
        point.selectedTroopCount--;
        if (point.selectedTroopCount < 0)
        {
            point.selectedTroopCount = 0;
        }
        selectionUIText.text = point.selectedTroopCount.ToString();
    }
    public void SelectionUIConfirm(Point point)
    {
        selectionUI.SetActive(false);
        if (_gameManager.secondPoint != null && !point.hasMoved)
        {
            var destination = _gameManager.secondPoint.GetComponent<Point>();

            _gameManager.MoveTroops(point.selectedTroopCount, destination);
            var point1 = _movement.pointsTransform[0].gameObject.GetComponent<Point>().pointID;
            var point2 = _movement.pointsTransform[1].gameObject.GetComponent<Point>().pointID;
            if(_movement.CheckMoveAble(point1, point2))
            {
                point.troopsCount -= point.selectedTroopCount;
                _gameManager.UpdateTroopCount();
                _gameManager.ClearSelected();
                _isSecondPointSelected = false;
                point.hasMoved = true;
            }
            
        }
        
        
        
        
    }

}
