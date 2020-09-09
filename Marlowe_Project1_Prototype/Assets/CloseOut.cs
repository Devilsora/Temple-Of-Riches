using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class CloseOut : MonoBehaviour {

	public GameObject canvas;
  public GameObject UI;
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void CloseMainMenu(GameObject canvas)
	{
		Debug.Log("Closing menu");
		canvas.SetActive(!canvas.activeInHierarchy);
	  

    if (player.GetComponent<FirstPersonController>())
		{
			player.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
		}

	  player.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 90, 0);
	}

  public void OpenPlayerUI(GameObject UI)
  {
    UI.SetActive(!UI.activeInHierarchy);
  }

  public void Restart()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void QuitGame()
    {
        Application.Quit();
    }
}
