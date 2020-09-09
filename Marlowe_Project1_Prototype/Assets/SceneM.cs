using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneM : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GotoScene(string SceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(SceneName))
            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        else
            Debug.Log("Can't load scene " + SceneName);
    }

  public void QuitGame()
  {
    Application.Quit();
  }
}
