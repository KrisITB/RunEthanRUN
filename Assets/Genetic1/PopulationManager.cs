using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour {

    private List<GameObject> population;
    private DNA[] populationDNA;

    public GameObject PrefabRef;
    public Transform spawnPoint;

    public bool breading;

    public GameObject BoundsGO;
    public float BoundsUnitsFromCenter= 5;

    public static float elapsed = 0;
    
    public float getElapsed()
    {
        return elapsed;
    }

    public float elapsedShow;
    public int environmentCarryingCapacity = 100;
    
    public int trialTime = 10;
    public int generation = 1;

    float centreX;
    float centreY;
    float centreZ;
    float offset;

    
    // Use this for initialization
    void Start () {

        centreX = BoundsGO.transform.position.x;
        centreY = BoundsGO.transform.position.y;
        centreZ = BoundsGO.transform.position.z;
        offset = BoundsUnitsFromCenter;


        InitialPopulationSetup();
        populationDNA = GetComponentsInChildren<DNA>();
    }
	
    private void InitialPopulationSetup()
    {
        population = new List<GameObject>();
        for (int i = 0; i < environmentCarryingCapacity; i++)
        {
            Vector3 pos = new Vector3(0, 0, 0);
            for (int j = 0; j < 3; j++)
            {
                float min = -1f * offset;
                float max = offset;
                pos[j] = BoundsGO.transform.position[j] + Random.Range(min, max); 
            }

            Quaternion rotation = new Quaternion(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            spawnPoint.transform.position = pos;
            spawnPoint.transform.rotation = rotation;
            GameObject newPerson = Instantiate(PrefabRef,spawnPoint.transform.position,spawnPoint.transform.rotation, this.transform);
           // Debug.Log("initial seed i = " + i + "spawnPoint pos = " + spawnPoint.transform.position);
            newPerson.name = "Person";
            newPerson.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            newPerson.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            newPerson.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            newPerson.GetComponent<DNA>().size = Random.Range(0.5f, 1.5f);

            population.Add(newPerson);
        }
    }

    void BreadNewPopulation()
    {
        breading = true;
        List<GameObject> newPopulation = new List<GameObject>();
        List<GameObject> sortedList = population.OrderBy(o => o.gameObject.GetComponent<DNA>().timeToDie).ToList();
        population.Clear();
        for (int i = (int)(sortedList.Count/2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Bread(sortedList[i], sortedList[i + 1]));
            population.Add(Bread(sortedList[i + 1], sortedList[i]));
        }
        for(int j = 0; j < sortedList.Count; j++)
        {
            Destroy(sortedList[j]);
        }
        breading = false;
    }

    private GameObject Bread(GameObject parent1, GameObject parent2)
    {
        
        Vector3 pos = new Vector3(0, 0, 0);

        for (int j = 0; j < 3; j++)
        {
            pos[j] = BoundsGO.transform.position[j] + Random.Range(-1 * offset, offset);
        }

        Quaternion rotation = new Quaternion(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        spawnPoint.transform.position = pos;
        spawnPoint.transform.rotation = rotation;

        GameObject newPerson = Instantiate(PrefabRef, spawnPoint.transform.position, spawnPoint.transform.rotation, this.transform);
        newPerson.name = "Person";

        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        newPerson.GetComponent<DNA>().r = Random.Range(1, 100) < 50 ? dna1.r : dna2.r;
        newPerson.GetComponent<DNA>().g = Random.Range(1, 100) < 50 ? dna1.g : dna2.g;
        newPerson.GetComponent<DNA>().b = Random.Range(1, 100) < 50 ? dna1.b : dna2.b;
        newPerson.GetComponent<DNA>().size = Random.Range(1, 100) < 50 ? dna1.size : dna2.size;
        return newPerson;
    }
        // Update is called once per frame
    void Update () {
        elapsedShow = getElapsed();
        elapsed += Time.deltaTime;
        if(elapsed > trialTime && breading != true)
        {
            elapsed = 0;
            BreadNewPopulation();
        }
	}
}
