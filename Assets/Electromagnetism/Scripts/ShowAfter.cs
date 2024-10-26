using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAfter : MonoBehaviour
{
    public GameObject buttons;
    public float seconds = 2;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(LateCall(seconds));
    }

    IEnumerator LateCall(float seconds)
    {
        if (buttons.activeInHierarchy)
            buttons.SetActive(false);

        yield return new WaitForSeconds(seconds);

        buttons.SetActive(true);
    }

}
