using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class UpdateHealth : MonoBehaviour
{
  public FirstPersonController player;
  public GameObject[] healthIcons;

  public int startingHealth;
  public int currentHealth;
  public int lastHealth;

  public float clip_length;
  public float time_since_last_played = 0.0f;

  // Use this for initialization
  void Start ()
	{
	  player = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
	  startingHealth = player.health;
	  currentHealth = player.health;
	  lastHealth = currentHealth;
    clip_length = gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;
  }
	
	// Update is called once per frame
	void Update ()
	{
    currentHealth = player.health;

    if (currentHealth < lastHealth && currentHealth != 0)
	  {
      //lost health - make hearts dissapear
	    for (int i = lastHealth; i > currentHealth; i--)
	    {
        healthIcons[i - 1].SetActive(false);

        //play damage animation
	      time_since_last_played = 0.0f;
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Animator>().Play("TakeDamageAnimation");
        
	    }

	    lastHealth = currentHealth;
	  }
    else if (currentHealth > lastHealth && currentHealth != 0)
	  {
      //gained health - make hearts reappear
	    for (int i = lastHealth; i < currentHealth; i++)
	    {
	      healthIcons[i - 1].SetActive(true);
	    }
    }

	  if (time_since_last_played > clip_length && (gameObject.GetComponent<Animator>().enabled))
	  {
	    gameObject.GetComponent<Animator>().enabled = false;
    }

	  time_since_last_played += Time.deltaTime;
	}
}
