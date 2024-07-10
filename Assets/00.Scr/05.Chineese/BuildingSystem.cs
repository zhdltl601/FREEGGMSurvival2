using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public List<buildObject> objects = new List<buildObject>();
    public buildObject currentObject;
    private Vector3 currentPos;
    private Vector3 currentRot;
    public Transform currentPreview;
    public Transform cam;
    public RaycastHit hit;
    public LayerMask layer;

    public float offset = 1.0f;
    public float gridSize = 1.0f;

    public bool isBuilding = true;


    private void Awake()
    {
        currentObject = objects[0];
        ChangeCurrentBuilding(0);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (isBuilding)
        {
            StartPreview();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Build();
        }

        /*
        if(Input.GetKeyDown("0") || Input.GetKeyDown("1"))
        {
            SwitchCurrentBuilding();
        }
        */

        if (Input.GetKeyDown("1"))
        {
            Debug.Log("1·Î ¹Ù²î¾ú³ë");
            ChangeCurrentBuilding(0);
        }

        if (Input.GetKeyDown("2"))
        {
            Debug.Log("2·Î ¹Ù²î¾ú³ë");
            ChangeCurrentBuilding(1);
        }
    }

    public void SwitchCurrentBuilding()
    {
        for (int i = 0; i < 2; i++)
        {
            if (Input.GetKeyDown("" + 1))
            {
                ChangeCurrentBuilding(1);
            }
        }
    }

    public void ChangeCurrentBuilding(int cur)
    {
        currentObject = objects[cur];
        if (currentPreview != null)
        {
            Destroy(currentPreview.gameObject);
        }
        GameObject curPrev = Instantiate(currentObject.preview, currentPos, Quaternion.Euler(currentRot)) as GameObject;
        currentPreview = curPrev.transform;
    }

    public void StartPreview()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, 10, layer))
        {
            if (hit.transform != this.transform)
            {
                ShowPreview(hit);
            }
        }
    }

    public void ShowPreview(RaycastHit hit)
    {
        currentPos = hit.point;
        currentPos -= Vector3.one * offset;
        currentPos /= gridSize;
        currentPos = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
        currentPos *= gridSize;
        currentPos += Vector3.one * offset;
        currentPreview.position = currentPos;

        if (Input.GetMouseButtonDown(1))
        {
            currentRot += new Vector3(0, 90, 0);
        }

        currentPreview.localEulerAngles = currentRot;
    }

    public void Build()
    {
        PreviewObject po = currentPreview.GetComponent<PreviewObject>();
        if (po.isBuildable)
        {
            Instantiate(currentObject.prefab, currentPreview.position, Quaternion.Euler(currentRot));
        }
    }
}

[System.Serializable]
public class buildObject
{
    public string name;
    public GameObject prefab;
    public GameObject preview;
    public int gold;
}
