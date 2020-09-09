using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.AI;

public class KunaiLogic : MonoBehaviour {

    Rigidbody rb;
    float speed_val = 50;

    public AudioClip[] clips;
    public AudioSource source;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
      source = GetComponent<AudioSource>();

      int n = Random.Range(0, clips.Length);
      //Debug.Log("Accessor value");
      source.clip = clips[n];
    //rb.velocity = new Vector3(-speed_val, 0, 0);

  }


  void Update()
  {
    transform.forward = rb.velocity.normalized;
    transform.Rotate(new Vector3(0f, -90f, 0f));

  }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("In collision enter for kunai");
        if (collision.gameObject.tag == "Guard")
        {
            Debug.Log("Collided with guard");
            if (collision.gameObject.GetComponent<NavMeshAgent>())
            {
              Debug.Log("Kunai trigger collider with the guard");
              if (collision.gameObject.GetComponent<GuardLogic>())
              {
                collision.gameObject.GetComponent<GuardLogic>().dead = true;
                collision.gameObject.GetComponent<GuardLogic>().exlcaim.SetActive(false);
                collision.gameObject.GetComponent<GuardLogic>().question.SetActive(false);
                collision.gameObject.GetComponent<GuardLogic>().enabled = false;
              }

              

              if (collision.gameObject.GetComponent<NavMeshAgent>())
                collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;

             

              //trigger guard's death animation
              if (collision.gameObject.GetComponent<Animator>())
              {
              }
            }
        }
        else if (collision.gameObject.tag == "guardhead")
        {
          GameObject parent = collision.gameObject.transform.parent.gameObject;
          parent.GetComponent<GuardLogic>().dead = true;
          parent.GetComponent<GuardLogic>().exlcaim.SetActive(false);
          parent.GetComponent<GuardLogic>().question.SetActive(false);
          parent.GetComponent<GuardLogic>().enabled = false;

          parent.GetComponent<NavMeshAgent>().enabled = false;

        }
        else if(collision.gameObject.tag == "Launcher")
        {
            if(collision.gameObject.GetComponent<ArrowLauncher>())
            {
                collision.gameObject.GetComponent<ArrowLauncher>().broken = true;
            }
        }
        else if (collision.gameObject.tag == "ground")
        {
          rb.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            Debug.Log("Kunai collided with tag " + collision.gameObject.tag);
        }

      rb.constraints = RigidbodyConstraints.FreezeAll;
      //change this if decided to make thrown kunai pickup-able?

    }

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("In kunai logic on trigger enter"); 
    if (other.gameObject.tag == "Player")
    {
      if (other.gameObject.GetComponent<FirstPersonController>())
      {
        AudioSource.PlayClipAtPoint(source.clip, gameObject.transform.position);

        if (other.gameObject.GetComponent<FirstPersonController>())
        {
          other.gameObject.GetComponent<FirstPersonController>().kunaiCount = other.gameObject.GetComponent<FirstPersonController>().kunaiCount + 1;
        }

        Destroy(gameObject);
      }
      
    }
    else if (other.gameObject.tag == "Launcher")
    {
      if (other.gameObject.GetComponent<ArrowLauncher>())
      {
        other.gameObject.GetComponent<ArrowLauncher>().broken = true;
      }
    }
    else if (other.gameObject.tag == "guard")
    {
      Debug.Log("Kunai trigger collider with the guard");
      if (other.gameObject.GetComponent<GuardLogic>())
      {
        other.gameObject.GetComponent<GuardLogic>().exlcaim.SetActive(false);
        other.gameObject.GetComponent<GuardLogic>().question.SetActive(false);
        other.gameObject.GetComponent<GuardLogic>().sightPoint.SetActive(false);
        other.gameObject.GetComponent<GuardLogic>().dead = true;
        other.gameObject.GetComponent<GuardLogic>().enabled = false;
      }
        

      if (other.gameObject.GetComponent<NavMeshAgent>())
        other.gameObject.GetComponent<NavMeshAgent>().enabled = false;

      //rotate self into death position
      Quaternion newRot = other.gameObject.transform.rotation;
      newRot.eulerAngles = new Vector3(newRot.x, newRot.y, -180f);
      other.gameObject.transform.SetPositionAndRotation(other.gameObject.transform.position, newRot);

      //trigger guard's death animation
      //if (other.gameObject.GetComponent<Animator>())
      //{
      //  other.gameObject.GetComponent<Animator>().enabled = true;
      //  other.gameObject.GetComponent<Animator>().Play("GuardDeathAnim");
      //
      //  
      //}

      

      other.gameObject.GetComponent<GuardLogic>().source.clip = other.gameObject.GetComponent<GuardLogic>().deathC;
      other.gameObject.GetComponent<GuardLogic>().source.Play();

      other.gameObject.GetComponent<GuardLogic>().animationParent.GetComponent<Animator>().enabled = true;
      other.gameObject.GetComponent<GuardLogic>().animationParent.GetComponent<Animator>().Play("GuardParentDeathClip");
    }
    else if (other.gameObject.tag == "guardhead")
    {
      GameObject parent = other.gameObject.transform.parent.gameObject;
      parent.GetComponent<GuardLogic>().dead = true;
      parent.GetComponent<GuardLogic>().exlcaim.SetActive(false);
      parent.GetComponent<GuardLogic>().question.SetActive(false);
      parent.GetComponent<GuardLogic>().enabled = false;

      parent.GetComponent<NavMeshAgent>().enabled = false;

      parent.GetComponent<GuardLogic>().source.clip = parent.gameObject.GetComponent<GuardLogic>().deathC;
      parent.GetComponent<GuardLogic>().source.Play();

      parent.GetComponent<GuardLogic>().animationParent.GetComponent<Animator>().enabled = true;
      parent.GetComponent<GuardLogic>().animationParent.GetComponent<Animator>().Play("GuardParentDeathClip");
    }
    //else if (other.gameObject.tag == "sightVisor")
    //{
    //  GameObject parent = other.gameObject.transform.parent.parent.gameObject;
    //  parent.GetComponent<GuardLogic>().dead = true;
    //  parent.GetComponent<GuardLogic>().exlcaim.SetActive(false);
    //  parent.GetComponent<GuardLogic>().question.SetActive(false);
    //  parent.GetComponent<GuardLogic>().enabled = false;
    //
    //  parent.GetComponent<NavMeshAgent>().enabled = false;
    //
    //  parent.GetComponent<GuardLogic>().source.clip = parent.gameObject.GetComponent<GuardLogic>().deathC;
    //  parent.GetComponent<GuardLogic>().source.Play();
    //
    //  parent.GetComponent<GuardLogic>().animationParent.GetComponent<Animator>().enabled = true;
    //  parent.GetComponent<GuardLogic>().animationParent.GetComponent<Animator>().Play("GuardParentDeathClip");
    //  gameObject.transform.parent = other.gameObject.transform;
    //}
    else
    {
      Debug.Log("kunai logic Not triggering on a player, triggering on " + other.gameObject.tag);
    }



  }
}
