//using GeneticSharp.Domain.Chromosomes;
//using GeneticSharp.Domain.Randomizations;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;

//public class PfChromosome : ChromosomeBase
//{
//    private readonly int m_numberOfTiles;

//    public PfChromosome1(int numberOfTiles) : base(numberOfTiles)
//    {
//        m_numberOfTiles = numberOfTiles;
//        //var citiesIndexes = RandomizationProvider.Current.GetUniqueInts(numberOfCities, 0, numberOfCities);
//        int[] path = CreateOnePath();

//        for (int i = 0; i < numberOfTiles; i++)
//        {
//            if (i < path.Length)
//                ReplaceGene(i, new Gene(path[i]));
//            else
//                ReplaceGene(i, new Gene(-i));
//        }
//    }
//    //ºËÐÄ
//    private int[] CreateOnePath()
//    {
//        List<int> path = new List<int>();
//        int last = MapInfo.star;
//        int current = RandomOneFromArray(MapInfo.tiles[last].GetNeighbor());
//        for (int i = 0; i < m_numberOfTiles; i++)
//        {
//            if(MapInfo.tiles[current].GetState() == CubeState.end)
//            {
//                path.Add(current);
//                break;
//            }
//            if (MapInfo.tiles[current].GetState() == CubeState.obstacle || MapInfo.tiles[current].GetState() == CubeState.start)
//            {
//                break;
//            }
//            if(path.Contains(current))
//            {
//                //break;
//                current = RandomOneFromArray(MapInfo.tiles[path.Last()].GetNeighborExceptOne(path[path.Count - 2]));
//                continue;
//            }
//            if(MapInfo.tiles[current].GetState() == CubeState.normal)
//            {
//                path.Add(current);
//                int next = RandomOneFromArray(MapInfo.tiles[current].GetNeighborExceptOne(last));
//                last = current;
//                current = next;
//            }
//        }
//        return path.ToArray();
//    }
//    //int RandomOneFromArray(int[] array)
//    //{
//    //    return array[RandomizationProvider.Current.GetInt(0, array.Length)];

//    //}
//    int RandomOneFromArray(int[] array)
//    {
//        List<int> list = new List<int>();
//        foreach (var index in array)
//        {
//            if (MapInfo.end == index)
//                return index;
//            if (MapInfo.tiles[index].GetState() == CubeState.normal)
//                list.Add(index);
//        }
//        if (list.Count == 0)
//            return -1;
//        return list[RandomizationProvider.Current.GetInt(0, list.Count)];
//    }

//    public int PathLength { get; internal set; }

//    public override Gene GenerateGene(int geneIndex)
//    {
//        var genes = GetGenes();
//        if (geneIndex - 1 < 0)
//            return new Gene(-geneIndex);
//        else
//        {
//            int last = Convert.ToInt32(genes[geneIndex - 1].Value, CultureInfo.InvariantCulture);
//            if (last < 0)
//                return new Gene(-geneIndex);
//            else
//            {
//                if (geneIndex - 2 < 0)
//                {
//                    var g = RandomOneFromArray(MapInfo.tiles[last].GetNeighbor());
//                    var v = MapInfo.tiles[g].GetState() == CubeState.normal ? g : -geneIndex;
//                    return new Gene(v);
//                }
//                else
//                {
//                    int last2 = Convert.ToInt32(genes[geneIndex - 2].Value, CultureInfo.InvariantCulture);
//                    var g = RandomOneFromArray(MapInfo.tiles[last].GetNeighborExceptOne(last2));
//                    var v = MapInfo.tiles[g].GetState() == CubeState.normal ? g : -geneIndex;
//                    return new Gene(v);
//                }
//            }
//        }
//        //if (MapInfo.tiles[geneIndex].GetState() == CubeState.normal || MapInfo.tiles[geneIndex].GetState() == CubeState.highlight)
//        //{
//        //    return new Gene(RandomizationProvider.Current.GetInt(0, MapInfo.tileCount));
//        //}
//        //else
//        //{
//        //    return new Gene(-geneIndex);
//        //}
//    }

//    public override IChromosome CreateNew()
//    {
//        return new PfChromosome(m_numberOfTiles);
//    }

//    public override IChromosome Clone()
//    {
//        var clone = base.Clone() as PfChromosome;
//        clone.PathLength = PathLength;

//        return clone;
//    }
//}

//public static class MapInfo
//{
//    public static List<Tile> tiles
//    {
//        get
//        {
//            return Map.Instance.tileList;
//        }
//    }
//    public static int star
//    {
//        get
//        {
//            return Map.Instance.starTileIndex;
//        }
//    }
//    public static int end
//    {
//        get
//        {
//            return Map.Instance.endTileIndex;
//        }
//    }
//    public static int tileCount
//    {
//        get
//        {
//            return Map.Instance.x * Map.Instance.y;
//        }
//    }
//    public static Tile GetStarTile()
//    {
//        return tiles[star];
//    }
//    public static Tile GetEndTile()
//    {
//        return tiles[end];
//    }
//    public static int x
//    {
//        get
//        {
//            return Map.Instance.x;
//        }
//    }
//}