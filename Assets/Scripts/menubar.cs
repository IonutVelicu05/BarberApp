using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class menubar : MonoBehaviour
{
    [SerializeField] private GameObject openMenuObject; //the open menu button object
    [SerializeField] private GameObject closeMenuObject; // close menu button object 
    [SerializeField] private Button closeMenuButton; // close menu button
    [SerializeField] private Button openMenuButton; // open menu button
    private Vector3 startposOpen; // position from where the bar starts moving when opening the menu
    private Vector3 endPosOpen; // the end position where the bar stops moving when opening the menu
    private Vector3 startPosClose; // the position from where the bar starts moving when closing the menu
    private Vector3 endPosClose; // the end position where the bar stops moving when closing the menu
    private bool openMenu = false; // bool to check if the user clicks the OpenMenu button
    private bool closeMenu = false; // bool to check if the user clicks the CloseMenu button
    private bool triggerFunction = true; // bool to check if the transition is finished so the buttons can be pressed again
    [SerializeField]
    [Range(0f, 1f)]
    private float lerptime = 0f;
    [SerializeField] private float transitionSpeed = 3f; // the speed of the transition;

    public void OpenMenu()
    {
        if (triggerFunction) // check if the transition is finished
        {
            openMenu = true; // set the bool to true so the opening transition will start running
            closeMenu = false; // set the bool to false so the close transition will not run
            triggerFunction = false; // set the bool to false so the user can't spam click the buttons so it won't get buggy
        }
    }
    public void CloseMenu()
    {
        if (triggerFunction) // check if the transition is finished
        {
            openMenu = false; // set the bool to true so the opening transition will start running
            closeMenu = true; // set the bool to false so the close transition will not run
            triggerFunction = false; // set the bool to false so the user can't spam click the buttons so it won't get buggy
        }
    }
    private void Start()
    {
        startposOpen = gameObject.transform.position; //set the variable to be the position of the bar at the start of the app
        endPosClose = gameObject.transform.position; //set the variable to be the position of the bar at the start of the app
        endPosOpen = gameObject.transform.position + Vector3.right * 265f; //set the var to be the position of the bar at the end of opening transition
    }
    void Update()
    {
        if(openMenu == true) //check if the user clicked the OpenMenu button
        {
            if(lerptime <= 1)
            {
                lerptime += Time.deltaTime;
            }
            gameObject.transform.position = Vector3.Lerp(startposOpen, endPosOpen, lerptime * transitionSpeed);// move the bar the the end position
            if (lerptime >= 0.3)
            {
                openMenuObject.SetActive(false);
                closeMenuObject.SetActive(true);
            }
            if (lerptime >= 1)
            {
                openMenu = false;
                startPosClose = gameObject.transform.position; //set the variable to be the start position of the bar when the user closes the menu
                lerptime = 0f; // set it to 0 so the transition can run again when CloseMenu button is clicked
                triggerFunction = true; // set the bool to true so the open/close functions can run when user click the buttons
            }
        }
        else if(closeMenu == true) // check if the user clicked CloseMenu button
        {
            if (lerptime <= 1)
            {
                lerptime += Time.deltaTime;
            }
            gameObject.transform.position = Vector3.Lerp(startPosClose, endPosClose, lerptime * transitionSpeed); // move the bar the the end position
            if (lerptime >= 0.4)
            {
                closeMenuObject.SetActive(false);
                openMenuObject.SetActive(true);
            }
            if (lerptime >= 1)
            {
                closeMenu = false;
                lerptime = 0f; // set it to 0 so the transition can run again when CloseMenu button is clicked
                triggerFunction = true; // set the bool to true so the open/close functions can run when user click the buttons
            }
        }
    }
}
