using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void GoToTitle()
  {
    SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
  }

  public void GoToGame()
  {
    SceneManager.LoadScene(SceneManager.GetSceneAt(1).name);
  }
}
