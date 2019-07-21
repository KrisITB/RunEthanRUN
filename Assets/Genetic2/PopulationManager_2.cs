using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager_2 : MonoBehaviour {


    public GameObject botPrefab;
    public int populationSize = 50;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public float trialTime = 5;
    int generation = 1;
    private Transform[] spawnArray;
    public GameObject RunnersPlaceholder;

    public bool distanceAsFitness;

    GUIStyle guiStyle = new GUIStyle();
    private void OnGUI()
    {
        guiStyle.fontSize = 25;
        guiStyle.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(10, 10, 250, 150));
        GUI.Box(new Rect(0, 0, 140, 140), "Stats", guiStyle);
        GUI.Label(new Rect(10, 25, 200, 30), "Gen: " + generation, guiStyle);
        GUI.Label(new Rect(10, 75, 200, 30), "Population: " + population.Count, guiStyle);
        GUI.EndGroup();
    }

    // Use this for initialization
    void Start () {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Respawn");
        spawnArray = new Transform[temp.Length];

        for(int i= 0; i < temp.Length; i++)
        {
            spawnArray[i] = temp[i].GetComponent<Transform>();
        }

		for(int i = 0; i < populationSize; i++)
        {
            GameObject b = Instantiate(botPrefab, spawnArray[i].position, spawnArray[i].rotation, RunnersPlaceholder.transform);
            b.GetComponent<Brain_2>().Init();
            population.Add(b);
        }

	}
	
    GameObject Breed(GameObject parent1, GameObject parent2, int i)
    {
        int index = Random.Range(i, spawnArray.Length - 2);

        Debug.LogWarning("spawnArray.Length" + spawnArray.Length + "i = " + i + " index = " + index);

        GameObject offspring = Instantiate(botPrefab, spawnArray[index].position, spawnArray[index].rotation, RunnersPlaceholder.transform);

        Brain_2 b = offspring.GetComponent<Brain_2>();
        b.Init();

        if (Random.Range(0,100) == 1)
        {
            b.dna.Mutate();
            Debug.Log(b);
        }
        else
        {
            b.dna.Combine(parent1.GetComponent<Brain_2>().dna, parent2.GetComponent<Brain_2>().dna);
            Debug.Log(b);
        }
        return offspring;
    }

    void BreedNewPopulation()
    {
        List<GameObject> sortedList;
        if (!distanceAsFitness)
        {
            sortedList = population.OrderBy(o => o.GetComponent<Brain_2>().timeAlitve).ToList();
        }
        else
        {
            sortedList = population.OrderBy(o => o.GetComponent<Brain_2>().distanceTraveled).ToList();
        }
        population.Clear();

        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1], i));
            population.Add(Breed(sortedList[i+1], sortedList[i], i));
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }

	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;
        if(elapsed >= trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
	}
}
