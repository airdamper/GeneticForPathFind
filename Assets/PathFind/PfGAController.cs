using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Infrastructure.Framework.Threading;
using UnityEngine;

public class PfGAController : MonoBehaviour
{
    private GeneticAlgorithm m_ga;
    private Thread m_gaThread;
    private LineRenderer m_lr;


    private void Awake()
    {
        m_lr = GetComponent<LineRenderer>();
    }
 
    private void Start()
    {
        var fitness = new PfFitness();
        var chromosome = new PfChromosome(MapInfo.tileCount);

        // This operators are classic genetic algorithm operators that lead to a good solution on TSP,
        // but you can try others combinations and see what result you get.
        var crossover = new UniformCrossover();//new OrderedCrossover();
        var mutation = new ReverseSequenceMutation(); 
        var selection = new RouletteWheelSelection();
        var population = new Population(50, 200, chromosome);
       
        m_ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
        m_ga.Termination = new TimeEvolvingTermination(System.TimeSpan.FromHours(1));

        // The fitness evaluation of whole population will be running on parallel.
        m_ga.TaskExecutor = new ParallelTaskExecutor
        {
            MinThreads = 100,
            MaxThreads = 200
        };

        // Everty time a generation ends, we log the best solution.
        m_ga.GenerationRan += delegate
        {
            var pathLength = ((PfChromosome)m_ga.BestChromosome).PathLength;

            
            Debug.Log($"Generation: {m_ga.GenerationsNumber} - Length: ${pathLength}");
        };


        // Starts the genetic algorithm in a separate thread.
        m_gaThread = new Thread(() => m_ga.Start());
        m_gaThread.Start();
    }

    private void Update()
    {

        Map.Instance.logText.text = $"Generation: {m_ga.GenerationsNumber} \nLength: {((PfChromosome)m_ga.BestChromosome).PathLength}";
        DrawRoute();
    }

    private void OnDestroy()
    {
        // When the script is destroyed we stop the genetic algorithm and abort its thread too.
        m_ga.Stop();
        m_gaThread.Abort();
    }

    //void DrawRoute()
    //{
    //    if (m_ga.Population == null|| m_ga.Population.CurrentGeneration == null)
    //        return;
    //    var c = m_ga.Population.CurrentGeneration.BestChromosome as PfChromosome;
    //    if (c == null)
    //        return;
    //    m_lr.positionCount = c.PathLength;

    //    if (c != null)
    //    {
    //        var genes = c.GetGenes();

    //        for (int i = 0; i < c.PathLength; i++)
    //        {
    //            if ((int)genes[i].Value < 0)
    //                continue;
    //            var tile = MapInfo.tiles[(int)genes[i].Value];
    //            m_lr.SetPosition(i, tile.GetLinePoint());
    //        }

    //        //var firstCity = cities[(int)genes[0].Value];
    //        //m_lr.SetPosition(m_numberOfCities, firstCity.Position);
    //    }
    //}
    //void DrawRoute()
    //{
    //    if (m_ga.Population == null || m_ga.Population.CurrentGeneration == null)
    //        return;
    //    var c = m_ga.Population.CurrentGeneration.BestChromosome as PfChromosome;
    //    if (c == null)
    //        return;
    //    var genes = c.GetGenes();
    //    List<int> indexs = new List<int>();

    //    for (int i = 0; i < genes.Length; i++)
    //    {
    //        var currentTilesIndex = Convert.ToInt32(genes[i].Value, CultureInfo.InvariantCulture);
    //        if (currentTilesIndex >= 0 && !indexs.Contains(currentTilesIndex))
    //        {
    //            indexs.Add(currentTilesIndex);
    //        }
    //    }

    //    m_lr.positionCount = indexs.Count;

    //    for(int i=0;i<indexs.Count;i++)
    //    {
    //        var tile = MapInfo.tiles[indexs[i]];
    //        m_lr.SetPosition(i, tile.GetLinePoint());
    //    }
    //}
    void DrawRoute()
    {
        if (m_ga.Population == null || m_ga.Population.CurrentGeneration == null)
            return;
        var c = m_ga.Population.CurrentGeneration.BestChromosome as PfChromosome;
        if (c == null)
            return;
        var genes = c.GetGenes();
        int[] path = MapInfo.GetPathByGene(genes);

        m_lr.positionCount = path.Length+1;
        m_lr.SetPosition(0, MapInfo.GetStarTile().GetLinePoint());
        for (int i = 0; i < path.Length; i++)
        {
            m_lr.SetPosition(i+1, MapInfo.tiles[path[i]].GetLinePoint());
        }
    }
}