using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardHeadLogic : MonoBehaviour
{

  public GameObject guardParent;
  private GuardLogic parentLogic;

	// Use this for initialization
	void Start ()
	{
	  guardParent = gameObject.transform.parent.gameObject;
	  parentLogic = guardParent.GetComponent<GuardLogic>();
	}
	
	// Update is called once per frame
	void Update () {
		if(parentLogic.foundPlayer == true)
      gameObject.transform.LookAt(parentLogic.playerPos, Vector3.down);
	}
}
