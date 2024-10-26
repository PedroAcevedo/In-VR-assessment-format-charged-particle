using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchUI : MonoBehaviour
{
    public GameObject leftTip;
    public GameObject rightTip;
    public GameObject fingerTip;
    public List<Button> buttons;
    bool isPoiting;
    bool isTouching;
    Vector3 fingerTipForward;

    private float touchDistance;
    private Dictionary<int, Button> buttonMap;
    private Trigger pointerColliderR;
    private Trigger pointerColliderL;

    // Start is called before the first frame update
    void Start()
    {
        pointerColliderR = rightTip.GetComponent<Trigger>();
        pointerColliderL = leftTip.GetComponent<Trigger>();
        fingerTipForward = fingerTip.transform.TransformDirection(Vector3.forward);
        touchDistance = 0.005f;
        isTouching = false;
        isPoiting = false;

        DoButtonMap();
        pointerColliderR.OnTriggerEntered += Touch;
        pointerColliderL.OnTriggerEntered += Touch;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPointing();
    }

    public void Touch(Collider collider)
    {
        if (buttonMap.ContainsKey(collider.gameObject.GetInstanceID()) && isPoiting && !isTouching)
        {
            isTouching = true;
            StartCoroutine(ClickButton(buttonMap[collider.gameObject.GetInstanceID()]));
            
        }
        else
        {
            isTouching = false;
        }
    }
    IEnumerator ClickButton(Button b)
    {
        var pointer = new PointerEventData(EventSystem.current);
        SurveyController.roomClicks++;

        ExecuteEvents.Execute(b.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
        ExecuteEvents.Execute(b.gameObject, pointer, ExecuteEvents.submitHandler);

        yield return new WaitForSeconds(0.1f);
        ExecuteEvents.Execute(b.gameObject, pointer, ExecuteEvents.pointerExitHandler);
    }

    private void DoButtonMap()
    {
        buttonMap = new Dictionary<int, Button>();

        foreach(Button button in buttons)
        {
            buttonMap.Add(button.gameObject.GetInstanceID(), button);
        }
    }

    private void CheckIfPointing()
    {
        if (!OVRInput.Get(OVRInput.NearTouch.SecondaryIndexTrigger))
        {
            isPoiting = true;
            fingerTip = rightTip;
            leftTip.SetActive(false);
        }
        else
        {
            isPoiting = false;
        }

        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger) && !isPoiting)
        {
            isPoiting = true;
            fingerTip = leftTip;
            rightTip.SetActive(false);
        }

        fingerTip.SetActive(isPoiting);
    }
}
