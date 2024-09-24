using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public bool IsDead;
    public Dictionary<Type, IAgentComponent> Components = new Dictionary<Type, IAgentComponent>();
    
    public T GetCompo<T>() where T : class
    {
        if(Components.TryGetValue(typeof(T), out IAgentComponent compo))
        {
            return compo as T;
        }
        return default;
    }
    public void FactToTarget(Vector3 target)
    {
        Quaternion targetRot = Quaternion.LookRotation(target - transform.position);
        Vector3 currentEulerAngle = transform.rotation.eulerAngles;
        
        float yRotation = Mathf.LerpAngle(currentEulerAngle.y , targetRot.eulerAngles.y , 5 * Time.deltaTime);
        
        transform.rotation = Quaternion.Euler(currentEulerAngle.x , yRotation , currentEulerAngle.z);
    }


    protected virtual void Awake()
    {
        GetComponentsInChildren<IAgentComponent>().ToList().ForEach(x=>
        {
            Components.Add(x.GetType(),x);
        });
        foreach (var item in Components.Values)
        {
            item.Initialize(this);
        }
    }
}
