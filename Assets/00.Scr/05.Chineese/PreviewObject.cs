using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    public bool foundation;
    public List<Collider> col = new List<Collider>();
    public Material green;
    public Material red;
    public bool isBuildable;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("닿다");

        if (other.CompareTag("Building") && foundation)
        {
            Debug.Log("마참내 닿고야 말다");
            col.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Building") && foundation)
        {
            col.Remove(other);
        }
    }

    private void Update()
    {
        ChangeColor();
    }

    public void ChangeColor()
    {
        if (col.Count <= 0)
        {
            isBuildable = true;
        }
        else
        {
            isBuildable = false;
        }

        if (isBuildable)
        {
            if (transform.GetComponent<Renderer>() == null)
            {
                transform.GetComponentInChildren<Renderer>().material = green;
            }
            else
            {
                transform.GetComponent<Renderer>().material = green;
            }

            /*Debug.Log("isBuildable");
            foreach(Transform child in this.transform)
            {
                Debug.Log("그린으로 바꿨노 게이야");
                child.GetComponent<Renderer>().material = null;

                //child.GetComponent<Renderer>().material = green;
            }*/
        }
        else
        {
            if (transform.GetComponent<Renderer>() == null)
            {
                transform.GetComponentInChildren<Renderer>().material = red;
            }
            else
            {
                transform.GetComponent<Renderer>().material = red;
            }
            /*
            foreach (Transform child in this.transform)
            {
                Debug.Log("레드로 바꿨노 게이야");

                child.GetComponent<Renderer>().material = null;

                //child.GetComponent<Renderer>().material = red;
            }
            */
        }
    }
}
