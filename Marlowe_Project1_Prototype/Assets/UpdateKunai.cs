using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class UpdateKunai: MonoBehaviour
{
  public Text kunaiText;
  public float kunai_val;
  private FirstPersonController player;

  // Use this for initialization
  void Start()
  {
    player = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
  }

  // Update is called once per frame
  void Update()
  {
    kunai_val = player.kunaiCount;

    kunaiText.text = kunai_val.ToString();
  }
}

