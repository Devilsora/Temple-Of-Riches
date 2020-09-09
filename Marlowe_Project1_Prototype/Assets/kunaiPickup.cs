using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class kunaiPickup : MonoBehaviour
{

  public GameObject kunaiUI;
  public GameObject kunai_counter;
  public GameObject replacement;
  public AudioClip[] metalClangs;
  public AudioSource source;

  // Use this for initialization
  void Start()
  {
    int n = Random.Range(0, metalClangs.Length);
    //Debug.Log("Accessor value");
    source.clip = metalClangs[n];
  }


  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("In kunai pickup on trigger enter");
    if (other.gameObject.tag == "Player")
    {
      if(kunaiUI.activeSelf == false)
        kunaiUI.SetActive(true);
        
      AudioSource.PlayClipAtPoint(source.clip, gameObject.transform.position);

      if (other.gameObject.GetComponent<FirstPersonController>())
      {
        other.gameObject.GetComponent<FirstPersonController>().kunaiCount = other.gameObject.GetComponent<FirstPersonController>().kunaiCount + 1;
      }

      Destroy(gameObject);
    }
    else
    {
      Debug.Log("Not triggering on a player");
    }



  }
}
