using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FaceRotation : MonoBehaviour
{
    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 20), 0.3f);
    }
}
