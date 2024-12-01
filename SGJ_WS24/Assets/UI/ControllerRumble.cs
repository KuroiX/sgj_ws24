using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerRumble : MonoBehaviour
{
    public Score score;
    [ContextMenu(nameof(Start))]
    void Start()
    {
        // Example: Vibrate for 1 second with medium intensity
        //TriggerRumble(1f, 0.5f, 0.5f);
        //BeatTrackerr.fixedBeatUpdate -= StartRumble;
        //BeatTrackerr.fixedBeatUpdate += StartRumble;
        
        score.OnHit -= ScoreOnOnHit;
        score.OnHit += ScoreOnOnHit;
    }

    private void ScoreOnOnHit(HitType hit)
    {

        float hf = 0.1f;
        switch (hit)
        {
            case HitType.Perfect:
                hf = 0.5f;
                break;
            case HitType.Great:
                hf = 0.4f;
                break;
            case HitType.Good:
                hf = 0.3f;
                break;
            case HitType.Bad:
                hf = 0.2f;
                break;
            case HitType.Miss:
                hf = 0.1f;
                break;
        }
        
        TriggerRumble(0.3f, 0.0f, hf);
    }

    private void OnDisable()
    {
        //BeatTrackerr.fixedBeatUpdate -= StartRumble;
        score.OnHit -= ScoreOnOnHit;
    }

    private void StartRumble()
    {
        TriggerRumble(0.3f, 0.0f, 0.1f);
    }
    void TriggerRumble(float duration, float lowFrequency, float highFrequency)
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null) return;

        // Set vibration levels
        gamepad.SetMotorSpeeds(lowFrequency, highFrequency);

        // Stop vibration after the duration
        Invoke(nameof(StopRumble), duration);
    }

    void StopRumble()
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0, 0); // Stop the motors
        }
    }
}
