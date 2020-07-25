using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class t02 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if(Input.GetKeyDown("a"))
        {
            int[] path = CreateOnePath();
            LineRenderer lr = GetComponent<LineRenderer>();
            lr.positionCount = path.Length;
            for(int i=0;i<path.Length;i++)
            {
                lr.SetPosition(i, MapInfo.tiles[path[i]].GetLinePoint());
            }
        }
    }

    private int[] CreateOnePath()
    {
        List<int> path = new List<int>();
        int last = MapInfo.start;
        int current = RandomOneFromArray(MapInfo.tiles[last].GetNeighbor());
        for (int i = 0; i < MapInfo.tileCount; i++)
        {
            if (MapInfo.tiles[current].GetState() == CubeState.end)
            {
                path.Add(current);
                break;
            }
            if (MapInfo.tiles[current].GetState() == CubeState.obstacle || MapInfo.tiles[current].GetState() == CubeState.start)
            {
                break;
            }
            if (path.Contains(current))
            {
                current = RandomOneFromArray(MapInfo.tiles[path.Last()].GetNeighborExceptOne(path[path.Count-2]));
                continue;
            }
            if (MapInfo.tiles[current].GetState() == CubeState.normal)
            {
                path.Add(current);
                int next = RandomOneFromArray(MapInfo.tiles[current].GetNeighborExceptOne(last));
                last = current;
                current = next;
            }
        }
        return path.ToArray();
    }

    int RandomOneFromArray(int[] array)
    {
        List<int> list = new List<int>();
        foreach(var index in  array)
        {
            if (MapInfo.end == index)
                return index;
            if (MapInfo.tiles[index].GetState() == CubeState.normal)
                list.Add(index);
        }
        if (list.Count == 0)
            return -1;
        return list[RandomizationProvider.Current.GetInt(0, list.Count)];
    }
}
