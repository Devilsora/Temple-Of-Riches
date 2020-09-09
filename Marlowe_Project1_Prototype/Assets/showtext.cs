using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showtext : MonoBehaviour {

  public GameObject textmsg;

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("Turning on message");
    textmsg.SetActive(true);
  }

  private void OnTriggerExit(Collider other)
  {
    Debug.Log("Turning off message");
    textmsg.SetActive(false);
  }
}
