using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToPoints : MonoBehaviour {

public GameObject [] teleportPoints;
public GameObject player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.F1))
		{
			player.transform.SetPositionAndRotation(teleportPoints[0].transform.position, Quaternion.identity);
		}

	  if (Input.GetKeyDown(KeyCode.F2))
	  {
	    player.transform.SetPositionAndRotation(teleportPoints[1].transform.position, Quaternion.identity);
	  }

	  if (Input.GetKeyDown(KeyCode.F3))
	  {
	    player.transform.SetPositionAndRotation(teleportPoints[2].transform.position, Quaternion.identity);
	  }

	  if (Input.GetKeyDown(KeyCode.F4))
	  {
	    player.transform.SetPositionAndRotation(teleportPoints[3].transform.position, Quaternion.identity);
	  }

	  if (Input.GetKeyDown(KeyCode.F5))
	  {
	    player.transform.SetPositionAndRotation(teleportPoints[4].transform.position, Quaternion.identity);
	  }
  }
}
