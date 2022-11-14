using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoEveryFrameFor : CustomYieldInstruction {

    public float seconds;
    public float currentSeconds;
    public float delay;
    private float currentDelay;
    private Action<float> actionToRun;
    private bool lastExecuted = false;
    private bool unscaledTime = false;

    /// <summary>
    /// Executes the action every frame for X seconds, returning as paramater the porcentage from 0.0f to 1.0f
    /// </summary>
    /// <param name="seconds">The seconds that will be running the script.</param>
    /// <param name="actionToRun">The action to run. Returns a T parameter from 0f to 1f relative of the progress of the loop.</param>
    /// <param name="delay">Delay after which will start running the loop.</param>
    /// <param name="unscaledTime">Use unscaled time instead of scaled time.</param>
    /// <returns></returns>
    public DoEveryFrameFor(float seconds, Action<float> actionToRun, float delay = 0f, bool unscaledTime = false) {
        this.actionToRun = actionToRun;
        this.seconds = seconds;
        this.currentSeconds = 0f;
        this.delay = delay;
        this.unscaledTime = unscaledTime;
    }

    public override bool keepWaiting {
        get {
            currentDelay += Time.deltaTime;
            if( currentDelay >= delay ) {
                this.currentSeconds += (unscaledTime) ? Time.unscaledDeltaTime : Time.deltaTime;
                actionToRun(currentSeconds / seconds);
                if (!lastExecuted && currentSeconds >= seconds) actionToRun(1f);
            }
            return currentSeconds <= seconds;
        }
    }

}
