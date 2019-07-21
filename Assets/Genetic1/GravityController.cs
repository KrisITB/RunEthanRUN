using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour {

    public bool Apply2AllChildren = true;
    public bool ToggleGravity = true;
    public bool DisableRiggidBody = true;
    private Rigidbody[] allChildrenRiggidBody;
    

	// Use this for initialization
	void Start () {
        allChildrenRiggidBody = GetComponentsInChildren<Rigidbody>();
        allChildrenToggleGravity();
        allChildrenToggleRB();
    }
	
	// Update is called once per frame
	void Update () {
        

    }
    
    private void allChildrenToggleGravity()
    {
        foreach (Rigidbody RB in allChildrenRiggidBody)
        {
            RB.useGravity = ToggleGravity;
        }
    }

    private void allChildrenToggleRB()
    {
        foreach (Rigidbody RB in allChildrenRiggidBody)
        {
            RB.isKinematic = DisableRiggidBody;
        }
    }

}
