using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderTimer : MonoBehaviour
{
    private Slider _slider;
    private bool stopTimer;

    private System.Action onCompleteCallbackFn;

    public void SetTimerValue(float fillTimeInSecs)
    {
        _slider = GetComponent<Slider>();

        //ct: Just setting the min and max value of the bar. Not setting the fill.
        _slider.minValue = Time.time;
        _slider.maxValue = Time.time + fillTimeInSecs;
    }

    public void StopTimer()
    {
        stopTimer = true;
    }

    public void StartTimer(System.Action onComplete)
    {
        onCompleteCallbackFn = onComplete;
        stopTimer = false;
    }


    // Update is called once per frame
    void Update()
    {
        //Update timer fill bar
        if(!stopTimer && (_slider.value < _slider.maxValue))
        {
            _slider.value = Time.time;
            //Debug.Log("slider value = " + _slider.value);
        }

        if(!stopTimer && (_slider.value >= _slider.maxValue))
        {
            //Time is up, answer was not selected, show answer.
            if(onCompleteCallbackFn != null)
            {
                onCompleteCallbackFn();
            }
                
        }
            
    }
}
