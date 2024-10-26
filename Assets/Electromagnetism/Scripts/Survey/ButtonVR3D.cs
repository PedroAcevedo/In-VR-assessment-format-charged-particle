using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR3D : MonoBehaviour
{
    public GameObject button;
    public GameObject arrowSignal;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    private GameObject presser;
    private Vector3 firstPositionButton;
    private bool isPressed;
    private bool othersnotpressed;
    public Buttoncontroller buttoncontrollertable;
    public int idbutton;
    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        firstPositionButton = button.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!isPressed && other.gameObject.tag== "Hand")
        {
            buttoncontrollertable.verifychanges(idbutton);
            button.transform.localPosition = firstPositionButton - new Vector3(0, 0.03f, 0);
            presser = other.gameObject;
            button.gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

            onPress.Invoke();
            isPressed = true;
            arrowSignal.SetActive(true);
        }
    }
    public void unpressButton()
    {
        button.transform.localPosition = firstPositionButton;
        button.gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

        onRelease.Invoke();

    }
    public bool getButton()
    {
        return isPressed;
    }
    public void changeButton(bool change)
    {
        isPressed = change;
        arrowSignal.SetActive(isPressed);
        if (!change)
        {
            unpressButton();
        }
    }

}
