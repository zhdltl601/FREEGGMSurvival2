using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    public List<Collider> col = new List<Collider>();
    public Objectsorts sort;
    public Material green;
    public Material red;
    public bool isBuildable;

    public bool second;
    public PreviewObjectChild childcol;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("���");

        if (other.CompareTag("Building"))
        {
            Debug.Log("������ ���� ����");
            col.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Building"))
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
        if(sort == Objectsorts.foundation)
        {
            if (col.Count <= 0 && childcol.IsStuck())
            {
                isBuildable = true;
            }
            else
            {
                isBuildable = false;
            }
        }
        else
        {
            if (col.Count <= 0)
            {
                isBuildable = true;
            }
            else
            {
                isBuildable = false;
            }
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
                Debug.Log("�׸����� �ٲ�� ���̾�");
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
                Debug.Log("����� �ٲ�� ���̾�");

                child.GetComponent<Renderer>().material = null;

                //child.GetComponent<Renderer>().material = red;
            }
            */
        }
    }
}

public enum Objectsorts
{
    normal,
    foundation,
    floor
}
