using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {

    public float r, g, b;
    public bool dead = false;
    public float timeToDie = 0;
    public float size = 1;
    Renderer[] goRenderer;
    Collider goCollider;

    public void Die()
    {
        dead = true;
        timeToDie = PopulationManager.elapsed;
        Debug.Log("Dead at: " + timeToDie);
        foreach(Renderer goRend in goRenderer)
        {
            goRend.enabled = false;
            //Debug.LogWarning("renderer disabled ");
        }
        goCollider.enabled = false;
    }


    // Use this for initialization
    void Start () {
        goRenderer = GetComponentsInChildren<Renderer>();
        //Debug.LogWarning(this.gameObject.name + " => " +  goRenderer[0].gameObject.name);
        goCollider = GetComponent<Collider>();
        Birth();
	}

    void Birth()
    {
        goCollider.enabled = true;
        Vector3 newColorRGB = new Vector3();

        foreach (Renderer goRend in goRenderer)
        {
            goRend.enabled = true;
            for (int i = 0; i < 3; i++)
            {
                newColorRGB[i] = Random.Range(0.0f, 1.0f);
            }
            Color newColor = new Color(newColorRGB[0],newColorRGB[1], newColorRGB[2]);
            goRend.material.color = newColor;
        }
        this.gameObject.transform.localScale = new Vector3(Random.Range(0.8f*size, 1.2f*size) , Random.Range(0.5f * size, 1.5f * size), Random.Range(0.8f * size, 1.2f * size));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
