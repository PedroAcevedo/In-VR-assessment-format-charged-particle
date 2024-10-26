using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterestPoint : MonoBehaviour
{
    public int TimeActive = 2;
    public GameObject nextPoint;
    public GameObject newtonCanvas;
    public TextMeshProUGUI newtonLabel;
    public GameObject MarchingRef;
    public bool hideLabel;

    private Color touchedColor = Color.black;
    private Color originColor = new Color(0.0f, 0.0f, 0.0f);

    public float interactionTime = 0.0f;

    private float startTime = 0.0f;

    private void Start()
    {
        newtonCanvas.SetActive(!hideLabel);
    }

    private void Update()
    {
        if (!hideLabel)
        {
            ShowValueDefault();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<Renderer>().material.color = touchedColor;
        
        if (nextPoint != null)
        {
            nextPoint.SetActive(true);
        }

        if (other.gameObject.name == "RightHandAnchor" || other.gameObject.name == "LeftHandAnchor")
        {
            startTime = Time.time;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "RightHandAnchor" || other.gameObject.name == "LeftHandAnchor")
        {
            interactionTime += (Time.time - startTime);
        }
    }

    public bool showLabel()
    {
        return MarchingRef.GetComponent<SimulationController>().getCurrentMode();
    }

    public void Reset()
    {
        this.gameObject.GetComponent<Renderer>().material.color = originColor;
        newtonCanvas.SetActive(false);
        this.newtonLabel.text = "";
    }

    public void ShowValueDefault()
    {
        newtonCanvas.SetActive(true);
        changeValue();
    }

    public void changeValue()
    {
        this.newtonLabel.text 
            = MarchingRef.GetComponent<SimulationController>().getPointValue(this.gameObject.transform.localPosition) + " N";
    }
    
    public string GetCurrentValue()
    {
        return this.newtonLabel.text.Replace(" N", "");
    }

    public int GetNumberValue()
    {
        return MarchingRef.GetComponent<SimulationController>().GetPointValueInt(this.gameObject.transform.localPosition);
    }

}
