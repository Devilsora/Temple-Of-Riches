using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeIntoTitle : MonoBehaviour
{
  public float time_til_start = 2;

  public float FadeRate;
  private Image image;
  public bool fadingIn = false;

  private float targetAlpha;
  // Use this for initialization
  void Start()
  {
    this.image = this.GetComponent<Image>();
    if (this.image == null)
    {
      Debug.LogError("Error: No image on " + this.name);
    }

    if (fadingIn)
    {
      this.targetAlpha = this.image.color.a;
    }
    else
    {
      this.targetAlpha = 0.0f;
    }
    
  }

  // Update is called once per frame
  void Update()
  {
    Color curColor = this.image.color;
    float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
    if (alphaDiff > 0.0001f)
    {
      curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
      this.image.color = curColor;
      Debug.Log(gameObject.name + " color value is now: " + curColor);
    }
    
    if(curColor.a <= 0.2 && fadingIn == false)
      gameObject.SetActive(!gameObject.activeInHierarchy);
  }

  public void FadeOut()
  {
    this.targetAlpha = 0.0f;
  }

  public void FadeIn()
  {
    this.targetAlpha = 1.0f;
  }
}
