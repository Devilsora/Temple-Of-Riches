﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMoney : MonoBehaviour
{

  public Text moneyText;
  public float money_val = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	  moneyText.text = money_val.ToString();
	}
}
