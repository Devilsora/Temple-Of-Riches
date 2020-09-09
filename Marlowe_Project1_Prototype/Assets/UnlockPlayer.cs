using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class UnlockPlayer : MonoBehaviour
{

  public GameObject player;

	// Use this for initialization
	void Start () {
	  player.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
  }
	
	// Update is called once per frame
	void Update () {
		
	}
}
