using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractive : MonoBehaviour
{
    public float reach=3f;
    [SerializeField]
    LayerMask interacivable;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if(Physics.SphereCast(transform.position,0.2f,transform.forward,out hit, reach, interacivable, QueryTriggerInteraction.Collide))
            {
                if (hit.transform.TryGetComponent<IInteracivable>(out IInteracivable i))
                {
                    i.GetInteractive();
                }
            }
        }
    }
}

public interface IInteracivable
{
    public void GetInteractive();
}