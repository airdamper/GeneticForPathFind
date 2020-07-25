using GeneticSharp.Domain.Randomizations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t01 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(RandomizationProvider.Current.GetInt(0, 10));
    }
}
