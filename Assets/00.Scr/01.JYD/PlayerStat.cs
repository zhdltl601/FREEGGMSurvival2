
public enum Stat
{
    Health,
    Stamina,
    Attack,
    Speed,
    Intelligence,
    Hunger,
    
}



public class PlayerStat
{
    private int value;
    private int maxValue;
    private bool useMaxValue;
    
    
    
    public void SetDefaultValue(int defaultValue , int defaultMaxValue = 0)
    {
        value = defaultValue;
        
        if (defaultMaxValue != 0)
        {
            useMaxValue = true;
            maxValue = defaultMaxValue;
        }
    }

    public int GetValue()
    {
        return value;
    }
    
    public float GetPercent()
    {
        return useMaxValue ? ((float)value * 100) /maxValue : 0f;
    }

    public bool AddValue(int addValue)
    {
        if (useMaxValue && value + addValue >= maxValue)
        {
            value = maxValue;
            return false;
        }
        
        value += addValue;
        return true;
    }

    public bool RemoveValue(int removeValue)
    {
        if (value - removeValue <= 0)
        {
            value = 0;
            return false;
        }
        
        value -= removeValue;
        return true;
    }
    
}
