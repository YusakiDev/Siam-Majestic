using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public enum Phase
    {
        Buy,
        Skill,
        Move
    }

    #region variables

    public Sprite[] troopSprites;
    [SerializeField] Camera mainCamera;
    [SerializeField] private LayerMask allTilesLayer;
    public bool isSelectingPoint;
    public Vector2 mousePos;
    private SpriteRenderer _pointSpriteRenderer;
    Point _currentlySelectedPoint;
    public GameObject firstPoint;
    public GameObject secondPoint;

    [SerializeField] public Sprite[] pointSprites;

    [SerializeField] GameObject troopsPrefab;

    private bool _selectUIIsOpen;
    public bool canSelectPoint = true;

    private UIManager _uiManager;
    Movement _movement;

    public bool isAllyPhase = true;
    public Phase phase;

    public bool allowNextPhase;
    private int _enemyPhase = 0;
    private int _allyPhase = 0;


    public int enemyCoins = 0;
    public int allyCoins = 0;

    //coins instantiate
    [SerializeField] Button button3;
    [SerializeField] Button button5;
    [SerializeField] public TMP_Text coinText;

    [SerializeField] Button alieanbutton3;
    [SerializeField] Button alieanbutton5;


    private int _turn = 1;

    #endregion


    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _movement = Movement.Instance;
    }

    private void Start()
    {
        button3.onClick.AddListener(Button3);
        button5.onClick.AddListener(Button5);
        alieanbutton3.onClick.AddListener(AlieanButton3);
        alieanbutton5.onClick.AddListener(AlieanButton5);
        allowNextPhase = true;
        Default();
    }

    void Default()
    {
        _movement.allPoints[0].troopsCount = 40;
        _movement.allPoints[4].troopsCount = 5;
        _movement.allPoints[1].troopsCount = 5;
        //ฝั่งดี
        _movement.allPoints[2].troopsCount = 5;
        //ฝั่งenemy
        _movement.allPoints[10].troopsCount = 40;
        _movement.allPoints[9].troopsCount = 5;
        _movement.allPoints[8].troopsCount = 5;
        _movement.allPoints[7].troopsCount = 5;
        NextPhase();
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
        if (Mouse.current.leftButton.wasPressedThisFrame && canSelectPoint && phase == Phase.Move)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, allTilesLayer);
            if (rayHit.collider != null)
            {
                if(firstPoint != null && secondPoint != null)
                {
                    var point1 = _movement.pointsTransform[0].GetComponent<Point>();
                    var point2 = _movement.pointsTransform[1].GetComponent<Point>();
                    if (point1.isAlly)
                    {
                        point1.spriteRenderer.sprite = pointSprites[2];
                    }
                    else
                    {
                        point1.spriteRenderer.sprite = pointSprites[1];
                    }
                    if (!point1.hasTroops)
                    {
                        point1.spriteRenderer.sprite = pointSprites[0];
                    }

                    if (point2.isAlly)
                    {
                        point2.spriteRenderer.sprite = pointSprites[2];
                    }
                    else
                    {
                        point2.spriteRenderer.sprite = pointSprites[1];
                    }
                    if (!point2.hasTroops)
                    {
                        point2.spriteRenderer.sprite = pointSprites[0];
                    }

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
                            _uiManager._isSecondPointSelected = true;
                            var point1 = _movement.pointsTransform[0].gameObject.GetComponent<Point>();
                            var point2 = _movement.pointsTransform[1].gameObject.GetComponent<Point>();
                            if (point2.isAlly)
                            {
                                point2.spriteRenderer.sprite = pointSprites[8];
                            }
                            else
                            {
                                point2.spriteRenderer.sprite = pointSprites[7];
                            }

                            if (!point2.hasTroops)
                            {
                                point2.spriteRenderer.sprite = pointSprites[6];
                            }
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
                    if (!firstPoint.GetComponent<Point>().hasTroops)
                    {
                        Debug.Log("Null1?");
                        firstPoint = null;
                        return;
                    }
                    if (firstPoint.GetComponent<Point>().isAlly == isAllyPhase && firstPoint.GetComponent<Point>().hasTroops)
                    {
                        _movement.pointsTransform[0] = firstPoint.transform;
                        var point = _movement.pointsTransform[0].GetComponent<Point>();
                        if (point.isAlly)
                        {
                            point.spriteRenderer.sprite = pointSprites[8];
                        }
                        else
                        {
                            point.spriteRenderer.sprite = pointSprites[7];
                        }

                        if (!point.hasTroops)
                        {
                            point.spriteRenderer.sprite = pointSprites[6];
                        }

                        _uiManager.OpenSelectionUI(firstPoint.GetComponent<Point>());
                        _selectUIIsOpen = true;
                    }
                    else
                    {
                        Debug.Log("Null?");
                        firstPoint = null;
                    }
                }





            } //สวัสดีปีใหม่
        }
    }

    public void ClearSelected()
    {
        if (phase != Phase.Move)
        {
            return;
        }
        if (firstPoint != null)
        {
                Debug.Log("Cancel UI Click");
                var point = _movement.pointsTransform[0].GetComponent<Point>();
                if (point.isAlly)
                {
                    point.spriteRenderer.sprite = pointSprites[2];
                }
                else
                {
                    point.spriteRenderer.sprite = pointSprites[1];
                }
                if (!point.hasTroops)
                {
                    point.spriteRenderer.sprite = pointSprites[0];
                }
                firstPoint = null;
                _uiManager.CloseSelectionUI();
        }

        if (secondPoint != null)
        {
                Debug.Log("Cancel UI Click");
                var point = _movement.pointsTransform[1].GetComponent<Point>();
                if (point.isAlly)
                {
                    point.spriteRenderer.sprite = pointSprites[2];
                }
                else
                {
                    point.spriteRenderer.sprite = pointSprites[1];
                }
                if (!point.hasTroops)
                {
                    point.spriteRenderer.sprite = pointSprites[0];
                }
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
    public void NextPhase()
    {
        Debug.Log(allowNextPhase);
        if (allowNextPhase)
        {
            if (isAllyPhase)
            {
            _uiManager.turnsText.text =  _turn + "/" + 15;
            _uiManager.characterUI.GetComponent<Image>().sprite = _uiManager.allyCharacter;
            _allyPhase += 1;
            if (_allyPhase <= 3)
            {
                Debug.Log("Turn: "+_turn + " AllyPhase: " + _allyPhase + " AllyCoins: " + allyCoins);
            }
            if (_allyPhase == 1)
            {
                phase = Phase.Buy;
                _uiManager.shopUIAlly.SetActive(true);
                allyCoins += 2;
                _uiManager.coinsText.text = allyCoins.ToString();
                _uiManager.buyPhaseUI.GetComponent<Image>().color = Color.red;





            } else if (_allyPhase == 2)
            {
                phase = Phase.Skill;
                _uiManager.shopUIAlly.SetActive(false);
                _uiManager.buyPhaseUI.GetComponent<Image>().color = Color.white;
                _uiManager.skillPhaseUI.GetComponent<Image>().color = Color.red;
            } else if (_allyPhase == 3)
            {
                phase = Phase.Move;
                _uiManager.skillPhaseUI.GetComponent<Image>().color = Color.white;
                _uiManager.movePhaseUI.GetComponent<Image>().color = Color.red;
            }

            if (_allyPhase > 3)
            {
                isAllyPhase = false;
                _uiManager.movePhaseUI.GetComponent<Image>().color = Color.white;
            }
            }
            if (!isAllyPhase)
            {
                _uiManager.characterUI.GetComponent<Image>().sprite = _uiManager.enemyCharacter;
                _enemyPhase += 1;
                if (_enemyPhase <= 3)
                {
                    Debug.Log("Turn: "+_turn + " EnemyPhase: " + _enemyPhase + " EnemyCoins: " + enemyCoins);
                }

                if (_enemyPhase == 1)
                {
                    phase = Phase.Buy;
                    _uiManager.shopUIEnemy.SetActive(true);
                    enemyCoins += 2;
                    _uiManager.coinsText.text = enemyCoins.ToString();
                    _uiManager.buyPhaseUI.GetComponent<Image>().color = Color.red;
                }
                else if (_enemyPhase == 2)
                {
                    phase = Phase.Skill;
                    _uiManager.shopUIEnemy.SetActive(false);
                    _uiManager.buyPhaseUI.GetComponent<Image>().color = Color.white;
                    _uiManager.skillPhaseUI.GetComponent<Image>().color = Color.red;
                } else if (_enemyPhase == 3)
                {
                    phase = Phase.Move;
                    _uiManager.skillPhaseUI.GetComponent<Image>().color = Color.white;
                    _uiManager.movePhaseUI.GetComponent<Image>().color = Color.red;
                }
                if (_enemyPhase > 3)
                {
                    isAllyPhase = true;
                    _uiManager.movePhaseUI.GetComponent<Image>().color = Color.white;
                }
                if (_enemyPhase > 3)
                {
                    _turn += 1;
                    _allyPhase = 1;
                    allyCoins += 2;
                    _enemyPhase = 0;
                    foreach (var VARIABLE in _movement.allPoints)
                    {
                        VARIABLE.GetComponent<Point>().hasMoved = false;

                    }
                    _uiManager.buyPhaseUI.GetComponent<Image>().color = Color.red;
                    _uiManager.shopUIAlly.SetActive(true);
                    _uiManager.characterUI.GetComponent<Image>().sprite = _uiManager.allyCharacter;
                    _uiManager.coinsText.text = allyCoins.ToString();
                    _movement.allPoints[3].troopsCount ++;
                    UpdateTroopCount();
                    phase = Phase.Buy;
                    ClearSelected();
                    _uiManager.turnsText.text =  _turn + "/" + 15;
                    Debug.Log("Turn: "+_turn + " AllyPhase: " + _allyPhase + " AllyCoins: " + allyCoins);
                }

            }
        }

    }
    private void Button3()
    {
        if (allyCoins >= 3)
        {
            allyCoins -= 3;
            coinText.text = allyCoins.ToString();
            _movement.allPoints[1].troopsCount += 5;
            UpdateTroopCount();
        }
    }
    private void Button5()
    {
        if (allyCoins >= 5)
        {
            allyCoins -= 5;
            coinText.text = allyCoins.ToString();
            _movement.allPoints[1].troopsCount += 10;
            UpdateTroopCount();
        }
    }

    private void AlieanButton3()
    {
        if (enemyCoins >= 3)
        {
            enemyCoins -= 3;
            coinText.text = enemyCoins.ToString();
            _movement.allPoints[9].troopsCount += 5;
            UpdateTroopCount();
        }

    }
    private void AlieanButton5()
    {
        if (enemyCoins >= 5)
        {
            enemyCoins -= 5;
            coinText.text = enemyCoins.ToString();
            _movement.allPoints[9].troopsCount += 10;
            UpdateTroopCount();
        }
    }
}
