using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    public ParticleSystem particleToTest;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            particleToTest.Play();
        }
    }
}
