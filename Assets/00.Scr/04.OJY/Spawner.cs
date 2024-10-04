using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnTarget;
        [SerializeField] private List<Transform> _spawnPosDay;
        [SerializeField] private List<Transform> _spawnPosNight;
        private void OnEnable()
        {
            void Spawn()
            {

            }
            Spawn();
        }
    }

}