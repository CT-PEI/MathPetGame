using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderTimer : MonoBehaviour
{
    //public sliderTimer x;

    //[Tooltip("Time allowed per question.")]
    //public float fillTimeInSecs = 1.0f;

    private Slider _slider;
    private bool stopTimer;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();

    }

    public void SetTimerValue(float fillTimeInSecs)
    {

        _slider.minValue = Time.time;
        _slider.maxValue = Time.time + fillTimeInSecs;
    }

    //public void BeginTimer(float fillTime)
    //{

    //    _slider.minValue = Time.time;
    //    _slider.maxValue = Time.time + fillTimeInSecs;
    //}

    public void StopTimer()
    {
        stopTimer = true;
    }

    public void StartTimer()
    {
        stopTimer = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(!stopTimer)
        {
            _slider.value = Time.time;
        }
            
    }
}
