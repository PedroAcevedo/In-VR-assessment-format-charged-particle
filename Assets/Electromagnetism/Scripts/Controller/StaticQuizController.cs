using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticQuizController : MonoBehaviour
{
    public GameObject[] answersIndicator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOptionSelected(int option)
    {
        for (int i = 0; i < answersIndicator.Length; i++)
        {
            answersIndicator[i].SetActive(i == option);
        }
    }
}
