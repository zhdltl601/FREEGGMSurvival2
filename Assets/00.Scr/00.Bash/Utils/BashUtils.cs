using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashUtils : MonoBehaviour
{
   public static void SwapList<T>(int idx1,int idx2, ref List<T> list)
    {
        T temp = list[idx1];
        list[idx1] = list[idx2];
        list[idx2] = temp;
    }
}
