using GeneticSharp.Domain.Randomizations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject cubePrefab;
    public static Map Instance;
    public int tileCount { get; private set; }
    public List<Tile> tileList = new List<Tile>();
    public int startTileIndex;
    public int endTileIndex;
    public bool showNeighbor = false;
    public TextMesh logText;
    List<int> selectionList = new List<int>();
    
    private void Awake()
    {
        Application.targetFrameRate = 300;
        if (Instance == null)
            Instance = this;
        else
            Debug.LogWarning("There is another map instance in scene.");
    }
    public int x, y;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = x > y ? x / 2f + 1 : y / 2f + 1;
        Camera.main.transform.position = new Vector3(x / 2f - .5f, y / 2f - .5f, -10);
        tileCount = x * y;
        Transform tileParent = new GameObject("tiles").transform;
        for(int i = 0;i<tileCount;i++)
        {
            Tile newOne = new Tile(i);
            newOne.SetParent(tileParent);
            tileList.Add(newOne);
        }
        yield return null;
        //生成起点和终点
        int[] tempInts = RandomizationProvider.Current.GetUniqueInts(2, 0, tileCount);
        startTileIndex = tempInts[0];
        endTileIndex = tempInts[1];
        Debug.Log($"start:{startTileIndex},end:{endTileIndex}");
        UpdateStarAndEndTile();
    }

    public void UpdateStarAndEndTile()
    {
        ChangeTileState(startTileIndex, CubeState.start);
        ChangeTileState(endTileIndex, CubeState.end);
    }
    public void AddSelectIndex(int index)
    {
        selectionList.Add(index);
    }
    public void ChangeTileState(int index, CubeState state)
    {
        tileList[index].SetSate(state);
    }
    public void ChangeSelectionTileState(CubeState state)
    {
        foreach(var index in selectionList)
        {
            tileList[index].SetSate(state);
        }
        selectionList.Clear();
    }
}

public class Tile
{
    public int index;
    GameObject cubeObj;
    GameObject neighborObj;
    Cube cube;
    Vector2 pos;
    List<int> neighbor = new List<int>();
    
    public Tile(int index)
    {
        this.index = index;
        cubeObj = GameObject.Instantiate(Map.Instance.cubePrefab);
        cube = cubeObj.GetComponent<Cube>();
        cube.tile = this;
        cubeObj.transform.position = new Vector3(index % Map.Instance.x, index / Map.Instance.x, 0);
        InitNeighbor();
    }

    public int[] GetNeighbor()
    {
        return neighbor.ToArray();
    }
    public int[] GetNeighborExceptOne(int index)
    {
        List<int> list = new List<int>();
        foreach(int i in neighbor)
        {
            if (i != index)
                list.Add(i);
        }
        return list.ToArray();
    }

    void InitNeighbor()
    {
        int x = Map.Instance.x;
        int y = Map.Instance.y;
        //左边
        if(index % x != 0)
        {
            if(index - 1 >= 0)
                neighbor.Add(index - 1);
        }
        //右边
        if((index + 1) % x != 0)
        {
            if (index + 1 < Map.Instance.tileCount)
                neighbor.Add(index + 1);
        }
        //上边
        if (index - x >= 0)
            neighbor.Add(index - x);
        if (index + x < Map.Instance.tileCount)
            neighbor.Add(index + x);

        neighborObj = new GameObject("neighbor");
        neighborObj.transform.SetParent(cubeObj.transform, true);
        foreach(var i in neighbor)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.GetComponent<Renderer>().material.color = Color.red;
            go.transform.position = new Vector3(i % Map.Instance.x, i / Map.Instance.x, -1);
            go.transform.SetParent(neighborObj.transform);
        }
        ShowHideNeighbor(false);
    }
    public void ShowHideNeighbor(bool boo)
    {
        neighborObj.SetActive(boo && Map.Instance.showNeighbor);
    }
    public void SetParent(Transform parent)
    {
        cubeObj.transform.SetParent(parent);
    }
    public void SetSate(CubeState state)
    {
        cube.ChangeState(state);
    }
    public CubeState GetState()
    {
        return cube.state;
    }
    public Vector3 GetLinePoint()
    {
        Vector3 pos = cubeObj.transform.position;
        pos.z -= 1;
        return pos;
    }
}
