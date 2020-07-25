//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using GeneticSharp.Domain.Chromosomes;
//using GeneticSharp.Domain.Fitnesses;
//using GeneticSharp.Domain.Randomizations;
//using UnityEngine;

//public class PfFitness : IFitness
//{
//    public double Evaluate(IChromosome chromosome)
//    {
//        var genes = chromosome.GetGenes();
//        int pathLength = 0;

//        List<int> indexs = new List<int>();
//        int last = -1;
//        int breakCount = 0;
//        for (int i = 0; i < genes.Length; i++)
//        {
//            var currentTilesIndex = Convert.ToInt32(genes[i].Value, CultureInfo.InvariantCulture);
//            if(currentTilesIndex >= 0 && !indexs.Contains(currentTilesIndex))
//            {
//                indexs.Add(currentTilesIndex);
//                if(last>=0)
//                {
//                    if(!IsConnection(currentTilesIndex,last))
//                    {
//                        breakCount++;
//                    }
//                }
//                last = currentTilesIndex;
//            }
//        }
//        pathLength = indexs.Count;

//        ((PfChromosome)chromosome).PathLength = pathLength;

//        //int diffx = 0;
//        //int diffy = 0;


//        //diffx = Mathf.Abs(MapInfo.end % MapInfo.x - indexs.Last() % MapInfo.x);
//        //diffy = Mathf.Abs(MapInfo.end / MapInfo.x - indexs.Last() / MapInfo.x);

//        //var fitness = 1 / (diffx + diffy + 1);


//        var fitness = 1.0 - (pathLength / MapInfo.tileCount * 100);
//        //fitness *= 0.7;
//        if (breakCount > 0)
//        {
//            fitness /= breakCount;
//        }
//        if (!MapInfo.GetStarTile().GetNeighbor().Contains(MapInfo.star))
//        {
//            fitness /= 1000;
//        }

//        if (MapInfo.end != indexs.Last())
//        {
//            fitness /= 1000;
//        }
//        if (fitness < 0)
//            fitness = 0;
//        return fitness;
//    }
//    //public double Evaluate(IChromosome chromosome)
//    //{
//    //    var genes = chromosome.GetGenes();
//    //    int pathLength = 0;
//    //    bool isFind = false;
//    //    int tailCount = 0;
//    //    //bool hasBreak = false;
//    //    int breakpointCount = 0;

//    //    List<int> indexList = new List<int>();

//    //    for (int i = 0; i < genes.Length; i++)
//    //    {
//    //        var currentTilesIndex = Convert.ToInt32(genes[i].Value, CultureInfo.InvariantCulture);
//    //        indexList.Add(currentTilesIndex);
//    //        if (currentTilesIndex == MapInfo.end)
//    //        {
//    //            isFind = true;
//    //        }
//    //        else if(currentTilesIndex >= 0)
//    //        {
//    //            if (isFind)
//    //                tailCount++;
//    //            //if (hasBreak)
//    //            //{
//    //            //    breakpointCount++;
//    //            //    hasBreak = false;
//    //            //}
//    //            if (i - 1 >= 0)
//    //            {
//    //                if(IsConnection(i,i-1))
//    //                {
//    //                    breakpointCount++;
//    //                }
//    //            }
//    //            pathLength++;
//    //        }
//    //    }


//    //    var fitness = 1.0 - (pathLength / MapInfo.tileCount * 100);
//    //    var diff = MapInfo.tileCount - indexList.Distinct().Count();
//    //    if (diff > 0)
//    //    {
//    //        fitness /= diff;
//    //    }


//    //    ((PfChromosome)chromosome).PathLength = pathLength;

//    //    if (breakpointCount > 0)
//    //        fitness /= breakpointCount;
//    //    if (tailCount > 0)
//    //        fitness /= tailCount;
//    //    if (!isFind)
//    //        fitness /= MapInfo.tileCount;
//    //    if (!MapInfo.tiles[MapInfo.star].GetNeighbor().Contains((int)genes[0].Value))
//    //        fitness /= MapInfo.tileCount;

//    //    if (fitness < 0)
//    //    {
//    //        fitness = 0;
//    //    }
//    //    return fitness;
//    //}
//    bool IsConnection(int last,int current)
//    {
//        return MapInfo.tiles[last].GetNeighbor().Contains(current);
//    }

//}