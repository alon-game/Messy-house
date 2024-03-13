using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] popUps;
    public int popUpIndex;
    //private float waitTime = 2f;

    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;
            }
        }
    }
}
