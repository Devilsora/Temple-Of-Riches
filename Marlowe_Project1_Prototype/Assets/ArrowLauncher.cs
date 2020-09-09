using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
  public GameObject arrow;
  public GameObject launchSpot;
  public int arrowSupply = 5;
  public bool broken;
    float delay = 3f;
    float timer = 0;

  public ParticleSystem OutOfArrowsSmoke;
  public ParticleSystem brokenSmoke;

  public AudioSource source;
  public AudioClip broke;
  public AudioClip firing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
    }

  void OnTriggerEnter(Collider collision)
  {
    if (!broken)
    {
      if (timer > delay)
      {
        FireArrow();
        timer = 0;
      }
      else
      {
        Debug.Log("Can't fire yet");
      }
    }
    else
    {
      source.clip = broke;
      source.Play();
      brokenSmoke.Play();
      Debug.Log("Broken - can't fire");
    }
    
  }

  void FireArrow()
  {
    if (arrowSupply > 0)
    {
      //get normalized forward vector, multiply 
      Vector3 normalizedVec = gameObject.transform.up.normalized;

      arrowSupply--;
      GameObject arrowOBJ = Instantiate(arrow, launchSpot.transform.position, Quaternion.identity);

      //arrowOBJ.transform.forward = gameObject.transform.up;

      arrowOBJ.GetComponent<ArrowLogic>().parent_rotation = gameObject.transform.rotation;
      arrowOBJ.GetComponent<ArrowLogic>().parent_euler = gameObject.transform.eulerAngles;
     

      arrowOBJ.GetComponent<ArrowLogic>().parent_normalized = normalizedVec;

      arrowOBJ.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, 0); //arrowOBJ.transform.eulerAngles.

      //Debug.Log("Shoot spot forward direction: " + -launchSpot.transform.up);
      //
      //arrowOBJ.GetComponent<Rigidbody>().AddForce(-launchSpot.transform.up * 75f, ForceMode.VelocityChange);
      Debug.Log("Firing arrow - euler y value is: " + gameObject.transform.eulerAngles.y);
      
      source.clip = firing;
    }
    else
    {
      OutOfArrowsSmoke.Play();
      source.clip = firing;
    }

    source.Play();
  }

}
