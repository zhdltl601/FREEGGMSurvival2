using System.Collections.Generic;
using UnityEngine;

public class Stage_Player : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private List<Vector2Int> renderPosList;
    private static Vector3 last2DmapPos;
    private void Start()
    {
        transform.position = last2DmapPos;

        _lineRenderer = GetComponent<LineRenderer>();
        renderPosList = new List<Vector2Int>();
    }
    
    private void Update()
    {
        if(_lineRenderer.positionCount <= 0)return;
        last2DmapPos = transform.position;
        DrawLine();
        LookAtTarget();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void LookAtTarget()
    {
        //do something..
    }

    public void DrawLine()
    {
        _lineRenderer.SetPosition(0 , transform.position);
        
        for (int i = 1; i < renderPosList.Count; i++)
        {
            Vector2 render = renderPosList[i];
            _lineRenderer.SetPosition(i ,render);
        }
    }

    public void SetUpLineInfo(List<Vector2Int> list)
    {
        renderPosList = new List<Vector2Int>(list.Count);
        _lineRenderer.positionCount = renderPosList.Count;
        
        foreach (var item in list)
        {
            renderPosList.Add(new Vector2Int(item.x, item.y));
        }
    }

    public void RemoveList(Vector2Int data)
    {
        renderPosList.Remove(data);
        _lineRenderer.positionCount = renderPosList.Count;
    }
    
    public void ActiveLine(bool active)
    {
        if (active)
        {
            _lineRenderer.enabled = true;
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
