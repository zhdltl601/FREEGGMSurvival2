using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FishSystem : MonoBehaviour
{
    [SerializeField] private FishData fishData;
    
    private float _catchTimer;
    public float checkbarSpeed;
    
    public Slider successSlider;
    public RectTransform fish;
    public RectTransform checkBar;
    
    public float xClampPos;
    
    private float _fishFlipTimer;
    private int _fishDir = 1;
    
    private float _checkBarOffset;
    private float _fishOffset;
    
    private void OnEnable()
    {
        fish = Instantiate(fishData.fishObj,transform).GetComponent<RectTransform>();
    }

    private void Update()
    {
        FishFlipController();
        FishMove();
        CheckBarMove();

        CheckFishIsInBox();
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
            CatchFish();
        }
                
        _catchTimer += Time.deltaTime;
    }

    private void CatchFish()
    {
        Time.timeScale = 0;
                
        
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
