using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class PfChromosome : ChromosomeBase
{
    private readonly int m_numberOfTiles;

    public PfChromosome(int numberOfTiles) : base(numberOfTiles)
    {
        m_numberOfTiles = numberOfTiles;
        var tilesIndexes = RandomizationProvider.Current.GetUniqueInts(numberOfTiles, 0, numberOfTiles);

        for (int i = 0; i < numberOfTiles; i++)
        {
                ReplaceGene(i, new Gene(tilesIndexes[i]));
        }
    }
    public int PathLength { get; internal set; }

    public override Gene GenerateGene(int geneIndex)
    {
        return new Gene(RandomizationProvider.Current.GetInt(0, m_numberOfTiles));
    }


    public override IChromosome CreateNew()
    {
        return new PfChromosome(m_numberOfTiles);
    }

    public override IChromosome Clone()
    {
        var clone = base.Clone() as PfChromosome;
        clone.PathLength = PathLength;

        return clone;
    }
}

public static class MapInfo
{
    public static List<Tile> tiles
    {
        get
        {
            return Map.Instance.tileList;
        }
    }
    public static int start
    {
        get
        {
            return Map.Instance.startTileIndex;
        }
    }
    public static int end
    {
        get
        {
            return Map.Instance.endTileIndex;
        }
    }
    public static int tileCount
    {
        get
        {
            return Map.Instance.x * Map.Instance.y;
        }
    }
    public static Tile GetStarTile()
    {
        return tiles[start];
    }
    public static Tile GetEndTile()
    {
        return tiles[end];
    }
    public static int x
    {
        get
        {
            return Map.Instance.x;
        }
    }
    public static int[] GetPathByGene(Gene[] array)
    {
        List<int> pathList = new List<int>();
        Tile current = tiles[start];

        int last = -1;
        for(int i=0;i<tileCount;i++)
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
            int nextValue =-1;
            int nextIndex = -1;
            foreach(var n in neighbors)
            {
                if (tiles[n].GetState() == CubeState.end)
                {
                    nextValue = 1;
                    nextIndex = n;
                    break;
                }
                if(tiles[n].GetState() == CubeState.normal)
                {
                    int v = (int)array[n].Value;
                    if(!pathList.Contains(n))
                    {
                        if (v > nextValue)
                        {
                            nextValue = v;
                            nextIndex = n;
                        }
                    }
                }
            }
            if(nextValue != -1)
            {
                pathList.Add(nextIndex);
                last = current.index;
                current = tiles[nextIndex];
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