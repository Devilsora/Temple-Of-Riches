using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ArrowLogic : MonoBehaviour {

    Rigidbody rb;
    float speed_val = 50;
    bool no_longer_damaging = false;

  public Vector3 parent_normalized;
  public Vector3 parent_euler;
  public Quaternion parent_rotation;

    // Use this for initialization
  void Start () {
        rb = GetComponent<Rigidbody>();
    //Debug.Log("Sin value of euler y: " + Mathf.Sin(parent_euler.y) + "    Cos vlue of euler y: " + Mathf.Cos(parent_euler.y));

        rb.velocity = parent_normalized * speed_val;

        Debug.Log("Arrow velocity: " + rb.velocity);

      Debug.Log("Arrow euler angles: " + gameObject.transform.eulerAngles);
    Debug.Log("Parent euler angles: " + parent_euler);

    Vector3 newEuler = Vector3.zero;

    Debug.Log("Parent euler angles: " + parent_euler);

    if ((int)parent_euler.y == 100)
    {
      Debug.Log("In the 100 euler: " + parent_euler.y + " making this the arrow euler value");
      newEuler = new Vector3(0f, 80f, 0f);
    }
    else
    {
      Debug.Log("Parent euler wasn't 100.0f: " + parent_euler.y );
    }


    //parent_euler.y >= 90
    if (parent_euler.y == 180 || parent_euler.y == 90)
    {
      Debug.Log("In the big statement: " + parent_euler.y + " making this the arrow euler value");
      newEuler = new Vector3(0f, parent_euler.y, 0f);
    }
    

      
    else if(parent_rotation.y == 0)
      newEuler = new Vector3(0f, parent_rotation.y + 180, 0f);
      
//    else if(parent_rotation.z == 90)
//      newEuler = new Vector3(0f, parent_rotation.z, 0f);




    gameObject.transform.eulerAngles = newEuler;
    
    //fix rotation here?
    //gameObject.transform.rotation = new Quaternion(0f, parent_euler.y - 180f, 0f, 0f);
  }
	

  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "Player" && !no_longer_damaging && 
        collision.gameObject.GetComponent<FirstPersonController>().invincibility_timer > collision.gameObject.GetComponent<FirstPersonController>().invincibility_time)
    {
      Debug.Log("arrow Collided with player");
      if (collision.gameObject.GetComponent<FirstPersonController>())
      {
        collision.gameObject.GetComponent<FirstPersonController>().health -= 1;
        collision.gameObject.GetComponent<FirstPersonController>().m_AudioSource.clip = collision.gameObject.GetComponent<FirstPersonController>().hit_sound;
        collision.gameObject.GetComponent<FirstPersonController>().m_AudioSource.Play();

        Destroy(this.gameObject);
      }
        
        
    }
    else
    {
      Debug.Log("Collided with tag " + collision.gameObject.tag);
      
    }
    //move it forward a bit to make it stuck in the wall?
    rb.velocity = Vector3.zero;
    no_longer_damaging = true;
  }
}
