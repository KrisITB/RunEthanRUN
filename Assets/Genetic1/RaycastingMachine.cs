using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastingMachine : MonoBehaviour {

    public GameObject TargetBall;

    public bool RaycastOn = true;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate() {
        LetsPlay();
    }

    private void LetsPlay()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.name == "Person")
            {
                hit.transform.gameObject.GetComponent<DNA>().Die();
            }            
        }
    }
}
