using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeLoadScene : MonoBehaviour
{
    private int sec = 5;
    private int min = 0;
    private TMP_Text _TimerText;
    [SerializeField] private int delta = 0;

    private void Update()
    {
        _TimerText = GameObject.Find("TimerText").GetComponent<TMP_Text>();
        StartCoroutine(ITimer());
    }

    IEnumerator ITimer()
    {
        while (true)
        {
            if (sec == 0)
            {
                min--;
                sec = 60;
            }
            sec -= delta;
            _TimerText.text = "Time: " +  min.ToString("D2") + " : " + sec.ToString("D2");
            if (min == 0 && sec == 0)
            {
                SceneManager.LoadScene("Scenes/EndMenu");
            }
            yield return new WaitForSeconds(1);
        }
    }
}
