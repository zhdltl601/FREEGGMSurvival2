using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnDay;
        [SerializeField] private GameObject _spawnNight;
        [SerializeField] private List<Transform> _spawnPosDay;
        [SerializeField] private List<Transform> _spawnPosNight;
        private void Awake()
        {
            bool isMorning = DayManager.GetCurrentState == EDayState.Morning;
            List<Transform> spawnPos = isMorning ? _spawnPosDay : _spawnPosNight;
            GameObject creatureToSpawn = isMorning ? _spawnDay : _spawnNight;
            print(isMorning ? "morning" : "nightspawn");
            foreach (var pos in spawnPos)
            {
                Instantiate(creatureToSpawn, pos.position, pos.rotation, null);
            }
        }
    }

}