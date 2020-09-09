using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class GuardLogic : MonoBehaviour {

    // Use this for initialization
    public bool foundPlayer;
    public NavMeshAgent agent;
    public Vector3 playerPos;
    public float speed = 20f;
    public float rotateSpeed = 0.5f;
    public GameObject head;
    public GameObject sightPoint;
    //public GameObject body_pivot;
    public Material alerted;
    public Material notAlerted;

  public GameObject exlcaim;
  public GameObject question;

  public GameObject animationParent;

  public GameObject[] pointsToWanderTo;

  public AudioSource source;

  public AudioClip[] deathClips;
  public AudioClip deathC;

  public int path_position = 0;

  Vector3 originalSpot;
    Quaternion originalRotation;
    private float time_til_stop = 3.0f;
    private float timer = 0f;

    bool lookingLeft = false;
    float time_til_change_view = 5.0f;

    public bool wander_on = false;
    private bool wandering_to_spot = false;

  public bool dead = false;
  public string wander_spot_name;

    //movement pattern?

    void Start()
    {
        //sightPoint = gameObject.transform.GetChild(0).gameObject;

        originalSpot = transform.position;
        originalRotation = sightPoint.transform.rotation;
      source = gameObject.GetComponent<AudioSource>();

      int index = Random.Range(0, deathClips.Length);
      deathC = deathClips[index];

      path_position = Random.Range(0, pointsToWanderTo.Length);

      //Debug.Log("Guard original position: " + originalSpot);
    }

    // Update is called once per frame
    void Update()
    {
        int place_to_go_to = Random.Range(0, pointsToWanderTo.Length);
        if (foundPlayer)
        {
            agent.acceleration = 20f;
            wandering_to_spot = false;
            timer = 0f;
            head.GetComponent<Renderer>().material.color = alerted.color;
            
            agent.SetDestination(playerPos);
            Debug.Log("Spotted player - playerposition: " + agent.destination);
      
            exlcaim.SetActive(true);
            question.SetActive(false);

            

            //Debug.Log("Found the player - moving in");
            //
            //timer = 0f;
            //
            //Debug.Log("Player pos: " + playerPos);
            //
            ////move towards player
            //movementV = transform.position - playerPos;
            //
            //Debug.Log("Movement vector: " + movementV);
            //transform.position += movementV * speed * Time.deltaTime;

    }
        else
        {
            //if the last state was true but now they've lose the player

            //continue for a few seconds until stopping

            //rotate head to some value between -180 and 180 - simulate looking around
            if (agent.velocity.magnitude == 0.0f)
            {
                Debug.Log("Calling in Idle because not moving");
                InIdle();
            }

            exlcaim.SetActive(false);
            question.SetActive(true);
           

            timer += Time.deltaTime;
            if (timer > time_til_stop && wandering_to_spot == false)
            {
                Debug.Log("Couldn't find player and time's up - going back to original spot");
                agent.SetDestination(originalSpot);
                question.SetActive(false);
                agent.speed = 60f;

                //if(agent.isStopped)
                //  sightPoint.transform.eulerAngles = Vector3.Lerp(sightPoint.transform.eulerAngles, originalRotation.eulerAngles, 0.5f);
            }

            //if this is a wandering agent AND it's not currently wandering to a spot
            if (wander_on && !wandering_to_spot)
            {
              agent.SetDestination(pointsToWanderTo[place_to_go_to].transform.position);
              wandering_to_spot = true; //now wandering to that area - meaning shouldn't go into this func
            }

            //check if finished going to that spot
            if (wandering_to_spot)
            {
              if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= 0.1f)
              {
                wandering_to_spot = false;
                //set destination for new path
                if (path_position < pointsToWanderTo.Length)
                  path_position++;
                else
                  path_position = 0;
                //set new destination

              }
            }

              //agent.SetDestination(RandomNavSphere(originalSpot, dist_to_wander, -1));
              
              if (wander_on == false)
                InIdle();

              //This is the code for random wandering
      /*******
       * //check if done w/ path
          if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= 0.1f)
          {
            //choose a new path to go down
            wandering_to_spot = false;
            int temp_val = Random.Range(0, pointsToWanderTo.Length);

            while (temp_val == place_to_go_to)
              temp_val = Random.Range(0, pointsToWanderTo.Length);

            place_to_go_to = temp_val;
          }
       */



      //if (agent.pathStatus == NavMeshPathStatus.PathComplete && (Vector3.Distance(agent.pathEndPosition,originalSpot)) < 2.5f)
      //{
      //    if(transform.rotation != originalRotation)
      //    {
      //        //gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, 0.2f);
      //        Debug.Log("Rotating to original rotation");
      //    }
      //    else
      //    {
      //        Debug.Log("Don't need to rotate back to original rotation");
      //    }
      //
      //}
      //else
      //{
      //    Debug.Log("Path complete?: " + agent.pathStatus);
      //    Debug.Log("Dist from end to original?" + (Vector3.Distance(agent.pathEndPosition, originalSpot)));
      //}

      head.GetComponent<Renderer>().material.color = notAlerted.color;

            //  if (timer >= time_til_stop && transform.position != originalSpot)
            //{
            //  Debug.Log("Going back to original location");
            //  //go back to original location
            //  movementV = transform.position - originalSpot;
            //  transform.position += movementV * speed * Time.deltaTime;
            //}
            //else
            //{
            //  timer += Time.deltaTime;
            //}

            //start timer until guard loses player
        }
    }

    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag == "Player" && !dead)
        {
            Debug.Log("Guard is ded?: " + dead);
            Debug.Log("Touching the player - deal them damage or kill?");
            other.gameObject.GetComponent<FirstPersonController>().health = other.gameObject.GetComponent<FirstPersonController>().health - 1;
        }
        else
        {
            Debug.Log("Guard's touching object tagged with " + other.gameObject.tag);
            Debug.Log("Guard is ded?: " + dead);
        }
        
    }

    void OnCollision(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Touching the player in ONCOLLISION - deal them damage or kill?");
            
        }
        else
        {
            Debug.Log("Guard's touching object tagged with " + other.gameObject.tag + " in ONCOLLISION");
        }

    }



    void InIdle()
    {
      Debug.Log("In idle func");
      Vector3 lookingLeftV = new Vector3(head.transform.eulerAngles.x, 0f, head.transform.eulerAngles.z);
      Vector3 lookingRightV = new Vector3(head.transform.eulerAngles.x, 180f, head.transform.eulerAngles.z);

      timer += Time.deltaTime;

      //Debug.Log("Current euler angles: " + sightPoint.transform.eulerAngles);
      if (lookingLeft)
      {
        head.transform.eulerAngles = Vector3.Lerp(head.transform.eulerAngles, lookingLeftV, 0.010f);

        if (timer >= time_til_change_view)
        {
          //Debug.Log("Current euler angles when deciding to not look left: " + sightPoint.transform.eulerAngles);
          lookingLeft = false;
          //Debug.Log("Looking left is now false");
          timer = 0f;
        }
      }
      else
      {
        Debug.Log("Now looking right");
        head.transform.eulerAngles = Vector3.Lerp(head.transform.eulerAngles, lookingRightV, 0.010f);

        
        if (timer >= time_til_change_view)
        {
            lookingLeft = true;
            //Debug.Log("Looking left now true since sightPoint.y + 90 > -2f");
            timer = 0f;                
        }
      }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        //Debug.Log("Nav mesh wander: " + navHit.position);

        return navHit.position;
    }
}


//if (wander_on && !wandering_to_spot)
//{
//
//  Debug.Log("Choosing new place to go to since I've reached my spot");
//
//  //randomly pick guard spot to go to
//  wander_spot_name = pointsToWanderTo[place_to_go_to].gameObject.name;
//  agent.SetDestination(pointsToWanderTo[place_to_go_to].transform.position);
//  Debug.Log("Spot to wander to: " + agent.destination + "   " + wander_spot_name);
//  wandering_to_spot = true;
//  Debug.Log("Wandering?: " + wander_on + "   " + "Wandering to the spot?: " + wandering_to_spot);
//
//  if (wandering_to_spot && (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= 0.1f))
//  {
//    //pick new spot to wander to on next loop
//    Debug.Log("Wandering to spot " + wander_spot_name + "  - path complete");
//    
//    Debug.Log("Pick new spot to go to");
//    place_to_go_to = Random.Range(0, pointsToWanderTo.Length);
//    wandering_to_spot = false;
//  }
//  else
//  {
//    Debug.Log("In else statement in wandering logic");
//    Debug.Log("Wandering to spot?: " + wandering_to_spot);
//    Debug.Log("Distance to " + wander_spot_name + ": " + agent.remainingDistance);
//    Debug.Log("Is path complete?: " + agent.pathStatus);
//  }
// 
//}
