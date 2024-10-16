using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DG.Tweening;
using TMPro;

public class FishSystem : MonoSingleton<FishSystem>
{
    [SerializeField] private FishData fishData;
    [SerializeField] private GameObject _droppedFishIns;
    
    private float _catchTimer;
    public float checkbarSpeed;
    private float _startTimer;
    
    public Slider successSlider;
    public RectTransform fish;
    public RectTransform checkBar;
    
    public float xClampPos;
    
    private float _fishFlipTimer;
    private int _fishDir = 1;
    
    private float _checkBarOffset;
    private float _fishOffset;

    private bool isCatch;

    private bool isFishing;
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        fish = Instantiate(fishData.fishObj,transform).GetComponent<RectTransform>();
        isFishing = true;
    }
    private void Start()
    {
        FishEnd();
    }

    private void OnDisable()
    {
        Destroy(fish.gameObject);
    }

    private void Update()
    {
        if (isFishing && PlayerInput.Instance.IsMoving())
        {
            FishEnd();
            return;
        }
        
        FishFlipController();
        FishMove();
        CheckBarMove();

        CheckFishIsInBox();

        _startTimer += Time.deltaTime;
        if (_startTimer >= 60)
        {
            _startTimer = 0;
            FishEnd();
        }
    }

    private void CheckFishIsInBox()
    {
        float checkBarLeft = checkBar.anchoredPosition.x - _checkBarOffset;
        float checkBarRight = checkBar.anchoredPosition.x + _checkBarOffset;
    
        float fishLeft = fish.anchoredPosition.x - fish.sizeDelta.x / 2;
        float fishRight = fish.anchoredPosition.x + fish.sizeDelta.x / 2;
        
        if (fishLeft >= checkBarLeft && fishRight <= checkBarRight)
        {
            IncreaseValue();
        }
        else
        {
            DecreaseValue();
        }
        
        successSlider.value = (_catchTimer * 100/ fishData.catchTime);
    }

    private void DecreaseValue()
    {
        if (_catchTimer > 0)
            _catchTimer -= Time.deltaTime;
    }

    private void IncreaseValue()
    {
        if (CheckSuccess())
        {
            if(isCatch)return;
            CatchFish();
        }
                
        _catchTimer += Time.deltaTime;
    }

    private void CatchFish()
    {
        Time.timeScale = 0;
        
        isCatch = true;
        
        GameObject textObj = new GameObject("SuccessText");
        TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
        text.text = "<color=#FFD700> SUCCESS! </color>";
        text.alignment = TextAlignmentOptions.Center;
        text.transform.SetParent(transform, false);
        
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = fish.anchoredPosition + new Vector2(0, 120f);
        rectTransform.localScale = Vector3.one;
        
        Sequence seq = DOTween.Sequence().SetUpdate(true);
        seq.Append(text.rectTransform.DOScale(2f, 0.7f));
        seq.AppendCallback(() =>
        {
            Destroy(textObj.gameObject);
            FishEnd();
            SpawnFish();
            void SpawnFish()
            {
                print("spawn fish");
                Instantiate(_droppedFishIns, PlayerMovementV2.Instance.transform.position + Vector3.up, Quaternion.identity, null);
            }
        });

    }

    public void FishEnd()
    {
        isCatch = false;
        isFishing = false;
        //fishData = null;
        _catchTimer = 0;
        _fishFlipTimer = 0;
        Time.timeScale = 1;
        _fishDir = 1;
        
        PlayerInput.Instance.canRotate = true;
        
        gameObject.SetActive(false);
    }
    
    private bool CheckSuccess()
    {
        return _catchTimer -fishData.catchTime >= 0;
    }
    private void CheckBarMove()
    {
        if (Input.GetMouseButton(0))
        {
            checkBar.anchoredPosition -= new Vector2(checkbarSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetMouseButton(1))
        {
            checkBar.anchoredPosition -= new Vector2(checkbarSpeed * Time.deltaTime * -1, 0);
        }

        _checkBarOffset = checkBar.sizeDelta.x/2 + 10;
        checkBar.anchoredPosition = new Vector2(Mathf.Clamp(checkBar.anchoredPosition.x , - 700 + _checkBarOffset, 700 - _checkBarOffset) ,0);
    }
    private void FishFlipController()
    {
        _fishFlipTimer += Time.deltaTime;
        if (_fishFlipTimer >= fishData.fishFlipInterval)
        {
            _fishFlipTimer = 0;

            if (Random.value <= 0.5f)
            {
                FishFlip();
            }
        }
    }
    private void FishMove()
    {
        _fishOffset = fish.sizeDelta.x / 2;
        
        float currentPosition = fish.anchoredPosition.x;
        
        
        fish.anchoredPosition += new Vector2(Speed() * Time.deltaTime , 0);
        
        if (currentPosition + _fishOffset >= xClampPos && _fishDir == 1)
        {
            FishFlip();
        }
        else if (currentPosition - _fishOffset <= -xClampPos - 10 && _fishDir == -1)
        {
            FishFlip();
        }
    }
    private float Speed()
    {
        float randomAmount = Random.Range(fishData.minMoveSpeed ,fishData.maxMoveSpeed);
        return randomAmount * _fishDir;
    }
    private void FishFlip()
    {
        fish.Rotate(0,180 ,0);
        _fishDir = _fishDir * -1;
    }
    
}
