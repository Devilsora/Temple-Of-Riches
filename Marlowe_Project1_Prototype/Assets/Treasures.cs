using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Treasures : MonoBehaviour {

  public Text treasureCount; //then also get current treasure amt
  
  public int smallT = 1;
  public int bigT = 5;

  public void OnCollisionEnter(Collision collision)
  {
    if(collision.gameObject.tag == "Player")
    {
      
    }
  }

}
