using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingScreen : MonoBehaviour
{

  public GameObject player;
  public GameObject kunaiUI;
  public GameObject playerUI;
  private Animator anim;
  
  // Use this for initialization
	void Start ()
	{
	  anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	  if (Input.GetKeyDown(KeyCode.P))
	  {
	    Debug.Log("Playing animation");
	    anim.enabled = true;
      anim.Play("StartScreenAnimation");
	  }
	  else
	  {
	    Debug.Log("Detected other keycode");
	  }

	  if (GetComponentInChildren<RawImage>().color.a == 0)
	  {
      player.SetActive(true);
      playerUI.SetActive(true);
      kunaiUI.SetActive(true);
	    gameObject.SetActive(false);
	  }
	}
}
