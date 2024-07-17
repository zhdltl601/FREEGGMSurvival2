using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatSystem : MonoBehaviour
{
    public Dictionary<Stat, PlayerStat> PlayerStats = new Dictionary<Stat, PlayerStat>();
    
    private void Awake()
    {
        foreach (Stat stat in Enum.GetValues(typeof(Stat)))
        {
            PlayerStats[stat] = new PlayerStat();
        }
        
    }

    private void Start()
    {
        /*PlayerStats[Stat.Health].SetDefaultValue(100,100);
        PlayerStats[Stat.Stamina].SetDefaultValue(100,500);
        PlayerStats[Stat.Intelligence].SetDefaultValue(1,50);
        PlayerStats[Stat.Attack].SetDefaultValue(10,50);        
        PlayerStats[Stat.Speed].SetDefaultValue(10,50);*/
    }
    
    
}
