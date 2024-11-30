using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Spin : MonoBehaviour
{
    public InputAction spinAction;
    public Vector3 spinSpeed = new Vector3(0,0,0f);

    private SpinAction actions;
    
    public float rotationSpeed = 1; 
    public int spinScore = 0; 
    public int spinScoreThreshold = 100; 
    public float speedMultiplier = 1.0f;
    private float rotationAmount;
    
    private void OnEnable()
    {
        actions = new SpinAction();
        actions.SpinnerKeyboard.Spin.performed += SpinSpinner;
        actions.SpinnerGamepad.Spin.performed += SpinSpin;
        actions.SpinnerKeyboard.Enable();
        actions.SpinnerGamepad.Enable();
    }
    
    private void OnDisable()
    { 
        actions?.SpinnerKeyboard.Disable();
        actions?.SpinnerGamepad.Disable();
        AddSpinScoreToTotal();
        spinScore = 0;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        spinAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        transform.Rotate(spinSpeed);

        spinSpeed *= 0.9f * Time.deltaTime * 500;
        */
        
    }

    private void SpinSpinner(InputAction.CallbackContext callbackContext)
    {
        rotationAmount += rotationSpeed * speedMultiplier;
        spinSpeed.z =  rotationAmount;
        transform.Rotate(spinSpeed);
        
        /*
        if (rotationAmount >= 360f)
        {
            spinScore++;
            rotationSpeed = 0.1f;
        }
        */
    }

    private void SpinSpin(InputAction.CallbackContext callbackContext)
    {
        Vector2 value = callbackContext.ReadValue<Vector2>();
        value.Normalize();

        transform.up = value;
    }
    
    private void AddSpinScoreToTotal()
    {
        if (spinScore >= spinScoreThreshold)
        {
            Debug.Log("Addscore" + spinScore);
            //ScoreManager.AddScore(spinScore);
        }
    }
}
