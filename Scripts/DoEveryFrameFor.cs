using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoEveryFrameFor :CustomYieldInstruction {

    public float seconds;
    public float currentSeconds;
    public float delay;
    private float currentDelay;
    private Action<float> actionToRun;
    private bool lastExecuted = false;

    /// <summary>
    /// Executes the action every frame for X seconds, returning as paramater the porcentage from 0.0f to 1.0f
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="actionToRun"></param>
    /// <returns></returns>
    public DoEveryFrameFor(float seconds, Action<float> actionToRun, float delay = 0f) {
        this.actionToRun = actionToRun;
        this.seconds = seconds;
        this.currentSeconds = 0f;
        this.delay = delay;
    }

    public override bool keepWaiting {
        get {
            currentDelay += Time.deltaTime;

            if( currentDelay >= delay ) {

                this.currentSeconds += Time.deltaTime;

                actionToRun(currentSeconds / seconds);

                if (!lastExecuted && currentSeconds >= seconds) actionToRun(1f);
            }

            return currentSeconds <= seconds;
        }
    }

}
