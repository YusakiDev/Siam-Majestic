using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private GameManager _gameManager;
    [SerializeField] private GameObject troopsText;
    public TMP_Text text;
    public Movement movement;
    
    [Header("SelectionUI")]
    [SerializeField] private GameObject selectionUI;
    [SerializeField] private Button selectionUIPlus;
    [SerializeField] private Button selectionUIMinus;
    [SerializeField] private TMP_Text selectionUIText;
    [SerializeField] private GameObject selectionUIConfirm;
    
    private Point _selectedPoint;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        selectionUIPlus.onClick.AddListener(() =>
        {
            SelectionUIPlus(_selectedPoint);
        });
        selectionUIMinus.onClick.AddListener(() =>
        {
            SelectionUIMinus(_selectedPoint);
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
        selectionUI.GetComponent<RectTransform>().position = point.transform.position + new Vector3(0,1,0);
        _selectedPoint = point;
        selectionUI.SetActive(true);
        if (_selectedPoint.troopsCount > 0)
        {
            _selectedPoint.selectedTroopCount = _selectedPoint.troopsCount;
            selectionUIText.text = _selectedPoint.troopsCount.ToString();
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
        
        
    }

}
