using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CollectTreasure : MonoBehaviour
{

  public GameObject treasure_counter;
  public GameObject replacement;
  public float treasure_type = 0;
  public float treasure_value;
  public AudioClip [] moneyClips;
  public AudioSource source;

  // Use this for initialization
  void Start()
  {
    int n = Random.Range(0, moneyClips.Length);
        //Debug.Log("Accessor value");
        source = GetComponent<AudioSource>();
    source.clip = moneyClips[n];
  }


  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("In moneybag on trigger enter");
    if (other.gameObject.tag == "Player")
    {
      AudioSource.PlayClipAtPoint(source.clip, gameObject.transform.position);

      if (treasure_type > 0)
        Instantiate(replacement, transform);

      treasure_counter.GetComponent<UpdateMoney>().money_val += treasure_value;
      Destroy(gameObject);
    }
    else
    {
      Debug.Log("Not triggering on a player");
    }

    

  }

}
