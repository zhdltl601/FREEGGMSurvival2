using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (QuestManager.QuestCompleted)
        {
            print("all done");

        }
    }
}
