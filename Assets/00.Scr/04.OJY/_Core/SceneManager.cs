using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SceneManager : MonoSingleton<SceneManager>
    {
        private Vector3 last2DmapPos;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
        
    }
}
