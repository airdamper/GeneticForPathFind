using GeneticSharp.Domain.Randomizations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t03 : MonoBehaviour
{
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            DrawRoute();
        }
    }
    void DrawRoute()
    {
        int[] path = GetPathByGene(RandomizationProvider.Current.GetUniqueInts(MapInfo.tileCount, 0, MapInfo.tileCount));

        lr.positionCount = path.Length;
        for (int i=0;i<path.Length;i++)
        {
            lr.SetPosition(i, MapInfo.tiles[path[i]].GetLinePoint());
        }
    }
    public int[] GetPathByGene(int[] array)
    {
        List<int> pathList = new List<int>();
        Tile current = MapInfo.tiles[MapInfo.start];

        int last = -1;
        for (int i = 0; i < MapInfo.tileCount; i++)
        {
            int[] neighbors;
            if (last >= 0)
            {
                neighbors = current.GetNeighborExceptOne(last);
            }
            else
            {
                neighbors = current.GetNeighbor();
            }

            List<int> nList = new List<int>();
            int nextValue = -1;
            int nextIndex = -1;
            foreach (var n in neighbors)
            {
                if (MapInfo.tiles[n].GetState() == CubeState.end)
                {
                    nextValue = 1;
                    nextIndex = n;
                    break;
                }
                if (MapInfo.tiles[n].GetState() == CubeState.normal)
                {
                    int v = array[n];
                    if (!pathList.Contains(n))
                    {
                        if (v > nextValue)
                        {
                            nextValue = v;
                            nextIndex = n;
                        }
                    }
                }
            }
            if (nextValue != -1)
            {
                pathList.Add(nextIndex);
                last = current.index;
                current = MapInfo.tiles[nextIndex];
                if (current.GetState() == CubeState.end)
                    break;
            }
            else
            {
                break;
            }
        }
        return pathList.ToArray();
    }
}
