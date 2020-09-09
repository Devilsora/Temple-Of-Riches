using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayingAndQuitting : MonoBehaviour
{

  public GameObject canvas;
  public GameObject player;
  public GameObject UI;

  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	  if (Input.GetKeyDown(KeyCode.P))
	  {
	    Debug.Log("Closing menu");
	    canvas.SetActive(!canvas.activeInHierarchy);


	    if (player.GetComponent<FirstPersonController>())
	    {
	      player.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
	    }

	    player.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);
      UI.SetActive(!UI.activeInHierarchy);
    }

	  if (Input.GetKeyDown(KeyCode.Q))
	  {

	  }

	}
}
