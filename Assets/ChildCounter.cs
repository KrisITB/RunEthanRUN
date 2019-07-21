using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCounter : MonoBehaviour {
    public int ChildrenCount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ChildrenCount = this.gameObject.transform.childCount;
	}

    public bool ClearChildren()
    {
        GameObject[] temp;
        temp = this.gameObject.transform.GetComponentsInChildren<GameObject>();
        for (int i= 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
        return true;
    }
}
