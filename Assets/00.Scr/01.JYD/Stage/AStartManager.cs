using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
}

public class AStartManager : MonoBehaviour
{
    public Vector2Int bottomLeft, topRight, startPos; 
    [SerializeField] private Vector2Int targetPos;
    public List<Node> FinalNodeList;
    public bool allowDiagonal, dontCrossCorner;
    
    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;

    public LayerMask whatIsWall;
    public GameObject target;
    public Vector3 mousePos;
    public float moveSpeed;
    
    public Stage nodePrefab;
    private int index;
    
    [SerializeField] private StageUIManager stageUIManager;
    
    private void Awake()
    {
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];
        StageUIManager.OnSceneChange += HandleOnSceneChange;
    }
    private void Start()
    {
        float spacing = 3.0f;

        int aStartSizeX = Mathf.RoundToInt(sizeX/spacing);
        int aStartSizeY = Mathf.RoundToInt(sizeY/spacing);

        
        //for (int i = 0; i < aStartSizeX; i++)
        //{
        //    for (int j = 0; j < aStartSizeY; j++)
        //    {
        //        Vector3 nodePosition = new Vector3(bottomLeft.x + (spacing * j), bottomLeft.y + (spacing * i), 0);
        //        Stage newStage =  Instantiate(nodePrefab,nodePosition , quaternion.identity);
        //        
        //        ++index;
        //        newStage.sceneName = index.ToString();
        //    }
        //}
    }
    private void OnDestroy()
    {
        StageUIManager.OnSceneChange -= HandleOnSceneChange;
    }

    private void HandleOnSceneChange(bool obj)
    {
        void OnEnterScene()
        {
            print("onEnterScene");
            //DayManager.Is2D
            DayManager.CanProcess = true;
        }
        void OnMap()
        {
            print("onmap");
            DayManager.CanProcess = false;
            //load
        }
        if (obj) OnEnterScene();
        else OnMap();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())  
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos = new Vector2Int(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y));
            startPos = new Vector2Int(Mathf.RoundToInt(target.transform.position.x), Mathf.RoundToInt(target.transform.position.y));
DayManager.CanProcess = true;
stageUIManager.OnTimeToggle(true);
            StopAllCoroutines();
            PathFinding();
        }
        int hour = (int)DayManager.Instance.GetTimeOfDay;
        int min = (int)((DayManager.Instance.GetTimeOfDay - hour) * 10);
        stageUIManager.UpdateTime(hour, min / 10f * 60);

    }

    private void PathFinding()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.25f))
                    if ((whatIsWall & (1 << col.gameObject.layer)) != 0) 
                        isWall = true;
                
                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }
        
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];
        
        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();
        
        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            // 목표 지점에 도달한 경우
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                StartCoroutine(GoRoutine());
                return;
            }
                        
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);

            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }
        }
    }

    private IEnumerator GoRoutine()
    {
        Stage_Player line = target.GetComponent<Stage_Player>();

        List<Vector2Int> renderPos = new List<Vector2Int>();
        foreach (var item in FinalNodeList)
        {
            Vector2Int pos = new Vector2Int(item.x , item.y);
            renderPos.Add(pos);
        }
        
        line.SetUpLineInfo(renderPos);
        line.ActiveLine(true);
        
        foreach (var node in FinalNodeList)
        {
            Vector3 targetPosition = new Vector3(node.x, node.y, 0);
            
            while (Vector3.Distance(target.transform.position, targetPosition) > 0.01f)
            {
                DecreaseNodeAlpha();
                
                target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, Time.deltaTime * moveSpeed);
                yield return null;
            }

            target.transform.position = targetPosition;
            line.RemoveList(new Vector2Int(node.x, node.y));
        }

        Collider2D[] closeNode = Physics2D.OverlapBoxAll(target.transform.position, new Vector3(1,1,1), 0f, ~whatIsWall);//Physics2D.OverlapCircleAll(target.transform.position, 0.35f,~whatIsWall);
        
        closeNode[0].TryGetComponent(out Stage stage);
        if (stage != null)
        {
            stageUIManager.SetActive(true);
            stageUIManager.SetScene(stage.sceneName);

DayManager.CanProcess = false;
stageUIManager.OnTimeToggle(false);

            //stage.SceneMove();   
        }

        line.ActiveLine(false);
    }

    private void DecreaseNodeAlpha()
    {
        Collider2D[] farNode = Physics2D.OverlapCircleAll(target.transform.position, 1.3f,~whatIsWall);
        Collider2D[] closeNode = Physics2D.OverlapCircleAll(target.transform.position, 0.5f,~whatIsWall);
        
        farNode = farNode.Where(item => !closeNode.Contains(item)).ToArray();
            
        foreach (var item in farNode)
        {
            float targetFade = 0.1f;
            item.GetComponent<SpriteRenderer>().DOFade(targetFade, 0.4f);
        }
        
        foreach (var item in closeNode)
        {
            float targetFade = 0;
            item.GetComponent<SpriteRenderer>().DOFade(targetFade, 0.4f);
        }
    }

    private void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // 대각선 허용시, 벽 사이로 통과 안됨
            if (allowDiagonal && NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
            if (dontCrossCorner && (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall)) return;

            // 이웃 노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);

            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    void OnDrawGizmos()
    {
        //if (FinalNodeList.Count != 0)
        //    for (int i = 0; i < FinalNodeList.Count - 1; i++)
        //        Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
        
        /*for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                Vector3 nodePosition = new Vector3(bottomLeft.x + j, bottomLeft.y + i, 0);
                Gizmos.DrawWireCube(nodePosition, new Vector3(0.9f, 0.9f, 0.9f)); // Adjust size if needed
            }
        }*/
    }
}
