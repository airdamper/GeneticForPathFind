using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Randomizations;
using UnityEngine;

public class PfFitness : IFitness
{
    public double Evaluate(IChromosome chromosome)
    {
        var genes = chromosome.GetGenes();

        int[] path = MapInfo.GetPathByGene(genes);

        int x = MapInfo.end % MapInfo.x;
        int y = MapInfo.end / MapInfo.x;
        int diffx = Mathf.Abs(path.Last() % MapInfo.x - x);
        int diffy = Mathf.Abs(path.Last() / MapInfo.x - y);

        ((PfChromosome)chromosome).PathLength = path.Length;

        var fitness = 1.0 / (diffx + diffy + 2);
        fitness /= path.Length;
        if (fitness < 0)
            fitness = 0;
        return fitness;
    }
    bool IsConnection(int last,int current)
    {
        return MapInfo.tiles[last].GetNeighbor().Contains(current);
    }

}