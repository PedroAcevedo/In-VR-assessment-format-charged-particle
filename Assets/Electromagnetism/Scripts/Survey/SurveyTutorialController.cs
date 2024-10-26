using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyTutorialController : MonoBehaviour
{
    public GameObject[] ContinueButton;
    public Transform player;
    public GameObject Step4;
    public GameObject particle;

    private bool[] buttonPressed = { false, false, false };
    private bool isStep3;
    private bool isStep4;
    private Vector3 lastPlayerPosition;
    private Vector3 lastParticlePosition;

    private void Update()
    {
        if (isStep3)
        {
            if ((player.position - lastPlayerPosition).magnitude > 0 && !ContinueButton[2].activeSelf)
            {
                ContinueButton[2].SetActive(true);
            }
        }

        if (isStep4)
        {
            if ((particle.transform.position - lastParticlePosition).magnitude > 0 && !ContinueButton[3].activeSelf)
            {
                ContinueButton[3].SetActive(true);
            }
        }
    }

    public void TutorialButtonPressed(int button)
    {
        buttonPressed[button] = true;
        ContinueButton[1].SetActive(buttonPressed[0] && buttonPressed[1] && buttonPressed[2]);
    }

    public void ToggleStep3(bool value)
    {
        isStep3 = value;
        lastPlayerPosition = player.position;
    }

    public void ToggleStep4(bool value)
    {
        isStep4 = value;
        lastParticlePosition = particle.transform.position;
    }

}
