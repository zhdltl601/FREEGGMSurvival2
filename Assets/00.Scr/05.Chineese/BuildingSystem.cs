using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
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
    public LayerMask whatIsBuilding;

    public float offset = 1.0f;
    public float gridSize = 1.0f;

    public bool isBuilding = true;

    public MCFace dir;
    public Objectsorts sort;

    private int currentPosPlusY;


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
            Debug.Log("1로 바뀌었노");
            ChangeCurrentBuilding(0);
        }

        if (Input.GetKeyDown("2"))
        {
            Debug.Log("2로 바뀌었노");
            ChangeCurrentBuilding(1);
        }
        if (Input.GetKeyDown("3"))
        {
            Debug.Log("3로 바뀌었노");
            ChangeCurrentBuilding(2);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("확내려갔다가");
            currentPosPlusY--;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("확올라갔다가 ");
            currentPosPlusY++;
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

    public void ShowPreview(RaycastHit hit2)
    {
        if (currentObject.sort == Objectsorts.floor)
        {
            dir = GetHitFace(hit2);

            if (dir == MCFace.Up || dir == MCFace.Down)
            {
                currentPos = hit2.point;
            }
            else
            {
                if (dir == MCFace.North)
                {
                    Debug.Log("11");
                    currentPos = hit2.point + new Vector3(0, 0, 2);
                }
                else  if (dir == MCFace.South)
                {
                    Debug.Log("22");
                    currentPos = hit2.point + new Vector3(0, 0, -2);
                }
                else if (dir == MCFace.West)
                {
                    Debug.Log("33");
                    currentPos = hit2.point + new Vector3(-2, 0, 0);
                }
                else if (dir == MCFace.East)
                {
                    Debug.Log("44");
                    currentPos = hit2.point + new Vector3(2,  0, 0);
                }
            }

            currentPos -= Vector3.one * offset;
            currentPos /= gridSize;
            currentPos = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
            currentPos *= gridSize;
            currentPos += Vector3.one * offset;

        }
        else if(currentObject.sort == Objectsorts.Wall)
        {
            //벽벽밍

            dir = GetHitFace(hit2);

            if (dir == MCFace.Up || dir == MCFace.Down)
            {
                currentPos = hit2.point;
            }
            else
            {
                if (dir == MCFace.North)
                {
                    Debug.Log("11");
                    currentPos = hit2.point + new Vector3(0, 0, .25f);
                    currentRot = new Vector3(0, 90, 0);
                }
                else if (dir == MCFace.South)
                {
                    Debug.Log("22");
                    currentPos = hit2.point + new Vector3(0, 0, -.25f);
                    currentRot = new Vector3(0, -90, 0);
                }
                else if (dir == MCFace.West)
                {
                    Debug.Log("33");
                    currentPos = hit2.point + new Vector3(-.25f, 0, 0);
                    currentRot = new Vector3(0, -180, 0);
                }
                else if (dir == MCFace.East)
                {
                    Debug.Log("44");
                    currentPos = hit2.point + new Vector3(.25f, 0, 0);
                    currentRot = new Vector3(0, 180, 0);
                }
            }

            currentPos -= Vector3.one * offset;
            currentPos /= gridSize;
            currentPos = new Vector3(currentPos.x, Mathf.Round(currentPos.y), currentPos.z);
            currentPos *= gridSize;
            currentPos += Vector3.one * offset;

        }

        Debug.Log(currentPos + " 포지션 ");

        //currentPos = hit.point;
        

        //내꺼 ㅎㅎ
        //currentPos += new Vector3(0, currentPosPlusY, 0);

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

    public static MCFace GetHitFace(RaycastHit hit)
    {
        Vector3 inComingVec = hit.normal - Vector3.up;

        if(inComingVec == new Vector3(0, -1, -1))
        {
            Debug.Log("1");
            return MCFace.South;
        }
        else if(inComingVec == new Vector3(0, -1, 1))
        {
            Debug.Log("2");
            return MCFace.North;
        }
        else if(inComingVec == new Vector3(0, 0, 0))
        {
            Debug.Log("3");
            return MCFace.Up;
        }
        else if(inComingVec == new Vector3(1, 1, 1))
        {
            Debug.Log("4");
            return MCFace.Down;
        }
        else if(inComingVec == new Vector3(-1, -1, 0))
        {
            Debug.Log("5");
            return MCFace.West;
        }
        else if(inComingVec == new Vector3(1, -1, 0))
        {
            Debug.Log("6");
            return MCFace.East;
        }
        else
        {
            Debug.Log("7");
            return MCFace.None;
        }
    }
}

[System.Serializable]
public class buildObject
{
    public string name;
    public GameObject prefab;
    public GameObject preview;
    public Objectsorts sort;
    public int gold;
}

public enum MCFace
{
    None,
    Up,
    Down,
    East,
    West,
    North,
    South
}