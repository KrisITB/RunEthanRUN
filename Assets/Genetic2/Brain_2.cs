using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]

public class Brain_2 : MonoBehaviour {

    public int DNALength = 1;
    public float timeAlitve;
    public float distanceTraveled;
    public DNA_2 dna;
    private Vector3 startPos;

    private ThirdPersonCharacter m_character;
    private Vector3 m_Move;
    private bool m_Jump;
    public bool alive = true;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Danger")
        {
            alive = false;

            GameObject[] goTemp = gameObject.GetComponentsInChildren<GameObject>();
            foreach (GameObject temp in goTemp)
            {
                temp.GetComponent<Renderer>().enabled = false;
                temp.GetComponent<Rigidbody>().isKinematic = true;
            }            
        }
    }

    public void Init()
    {
        dna = new DNA_2(DNALength, 6);
        m_character = GetComponent<ThirdPersonCharacter>();
        timeAlitve = 0;
        alive = true;
        startPos = this.transform.position;
    }

	// Update is called once per frame
	void Update () {
        float h = 0;
        float v = 0;
        bool crouch = false;
        if (dna.GetGene(0) == 0)
        {
            v = 1;
        }
        else if(dna.GetGene(0) == 1)
        {
            v = -1;
        }
        else if (dna.GetGene(0) == 2)
        {
            v = -1;
        }
        else if (dna.GetGene(0) == 3)
        {
            v = 1;
        }
        else if (dna.GetGene(0) == 4)
        {
            m_Jump = true;
        }
        else if (dna.GetGene(0) == 5)
        {
            crouch = true;
        }

        m_Move = v * Vector3.forward + h * Vector3.right;
        if(m_character != null)
        {
            m_character.Move(m_Move, crouch, m_Jump);
        }
        else
        {
            Debug.LogError("m_character is null ");
        }
        m_Jump = false;
        if (alive)
        {
            lastAlive = Time.deltaTime;
            timeAlitve += Time.deltaTime;
            distanceTraveled =  Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.z), new Vector2 (startPos.x,startPos.z) );
        }
        else if (timeAlitve + (Time.time - lastAlive) + 3.0f > tt)
        {
            Debug.Log("Why?");
            //Destroy(this.gameObject);
        }
    }
    PopulationManager_2 PM;
    float tt;
    float lastAlive;
    void Start()
    {
        lastAlive = 0;
        PM = GetComponentInParent<PopulationManager_2>();
        tt = PM.trialTime;
    }
}
