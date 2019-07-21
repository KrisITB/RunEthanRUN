using System.Collections.Generic;
using UnityEngine;

public class DNA_2 {

    List<int> genes = new List<int>();
    int dnaLength = 0;
    int maxValues = 0;

    public DNA_2(int l, int v)
    {
        dnaLength = l;
        maxValues = v;
        SetRandom();
    }

    public void SetRandom()
    {
        genes.Clear();
        for(int i = 0; i < dnaLength; i++)
        {
            genes.Add(Random.Range(0, maxValues));
        }
    }

    public void SetInt(int pos, int value)
    {
        genes[pos] = value;
    }
    public void Combine(DNA_2 d1, DNA_2 d2)
    {
        for(int i = 0; i < dnaLength; i++)
        {
            if (i < dnaLength / 2.0f)
            {
                genes[i] = d1.genes[i];
            }
            else
            {
                genes[i] = d2.genes[i];
            }
        }
    }

    public void Mutate()
    {
        genes[Random.Range(0, dnaLength)] = Random.Range(0, maxValues);
    }

    public int GetGene(int pos)
    {
        return genes[pos];
    }
}
