using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BshAmiKlr
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public static bool Canf = false;
        public GameObject f;
        
        private void Start()
        {
            f.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        public static void Set(bool vsl)
        {
            Canf = vsl;
        }
    }

}
