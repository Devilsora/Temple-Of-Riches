using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightLineLogic : MonoBehaviour {

    public GuardLogic parent;

    public float timer;
    public float time_til_lose_player = 6.0f;

    public bool canSeePlayer = false;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
          Debug.Log("Spotted player");
          parent.foundPlayer = true;
          parent.playerPos = other.gameObject.transform.position;

          Debug.Log("Player position: " + other.gameObject.transform.position);
          canSeePlayer = true;
          timer = 0f;
            //parent.gameObject.transform.LookAt(other.gameObject.transform.position);

            //RaycastHit hit;
            //    if (Physics.Raycast(transform.position, transform.forward, out hit))
            //gameObject.transform.localScale *= 3;

            //parent.playerPos = other.gameObject.transform.position;

        }
        else
        {
          Debug.Log("Something thats not the player");
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
          canSeePlayer = false;
          Debug.Log("Lost sight of player in sightlinelogic");
        }
    }


    // Update is called once per frame
    void Update () {
		
        if(canSeePlayer == false)
        {
            timer += Time.deltaTime;
            if(timer >= time_til_lose_player)
            {
                Debug.Log("Lost sight of player in update loop for sightline");
                parent.foundPlayer = false;
            }
                
        }

      
	  }
}
