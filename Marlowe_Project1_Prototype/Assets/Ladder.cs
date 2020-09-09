using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

	//public GameObject bottom;
	public GameObject top;

	// Use this for initialization
	
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
    {
		Debug.Log("Ladder detecting collision with something");
        if(collision.gameObject.transform.position.y < top.transform.position.y) 
			collision.gameObject.transform.position = top.transform.position;
		else
		{
			Debug.Log("collision position: " + collision.gameObject.transform.position);
			Debug.Log("top position: " + top.transform.position);
		}
			
		
		
    }
}
