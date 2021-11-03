using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class menubar : MonoBehaviour
{
    [SerializeField] private CanvasGroup menuVisibility;
    private float speedFadeIn = 4f; // the speed of the fade in transition;
    private float speedFadeOut = 4f; // the speed of the fade out transition;
    private float fadeTime = 0f; 
    private bool isFading = false; //bool to check if the animation will be fade in or fade out


    public void Fade()
    {
        isFading = !isFading;
        if (isFading)
        {
            gameObject.SetActive(true);
        }
    }
    void Update()
    {
        if(isFading == true)
        {
            if (fadeTime < 1f)
            {
                fadeTime += Time.deltaTime;
            }
            menuVisibility.alpha = Mathf.Lerp(0f, 1f, fadeTime * speedFadeIn);
        }
        else if(isFading == false)
        {
            if(fadeTime > 0f)
            {
                fadeTime -= Time.deltaTime * speedFadeOut;
            }
            menuVisibility.alpha = Mathf.Lerp(0f, 1f, fadeTime);
            if(fadeTime < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
