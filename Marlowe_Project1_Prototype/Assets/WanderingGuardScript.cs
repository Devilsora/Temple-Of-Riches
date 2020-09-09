using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingGuardScript : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {

  }

  void Update()
  {

  }
}
  //if (wander_on && !wandering_to_spot)
  //{
  //  //randomly pick guard spot to go to
  //  int place_to_go_to = Random.Range(0, pointsToWanderTo.Length);
  //  wander_spot_name = pointsToWanderTo[place_to_go_to].gameObject.name;
  //  agent.SetDestination(pointsToWanderTo[place_to_go_to].transform.position);
  //  Debug.Log("Spot to wander to: " + agent.destination + "   " + wander_spot_name);
  //  wandering_to_spot = true;
  //}
  //
  //if (wandering_to_spot && agent.pathStatus == NavMeshPathStatus.PathComplete)
  //{
  //  //pick new spot to wander to on next loop
  //  Debug.Log("Wandered to spot " + wander_spot_name + "   - path complete");
  //  wandering_to_spot = false;
  //}
  ////agent.SetDestination(RandomNavSphere(originalSpot, dist_to_wander, -1));
  //if (wander_on == false)
  //  InIdle();
  //}