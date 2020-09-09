using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class CollectHelmet : MonoBehaviour
{

  public GameObject finaleCanvas;
  public GameObject playerUI;
  public GameObject kunaiUI;
  public GameObject player;

  public GameObject finaleCamera;
  public GameObject fader;

  // Use this for initialization
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      fader.GetComponent<Animator>().enabled = true;
    }
  }

  public void Update()
  {
    if (fader.GetComponent<RawImage>().color.a >= 0.9f)
    {
      Debug.Log("Setting up finale screen");
      playerUI.SetActive(false);
      kunaiUI.SetActive(false);
      finaleCanvas.SetActive(true);
      player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(false);
      player.GetComponentInChildren<Camera>().enabled = false;
      finaleCamera.SetActive(true);
    }
    else
    {
      Debug.Log("Fader value: " + fader.GetComponent<RawImage>().color.a);
    }
  }
}
