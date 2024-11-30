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
        Debug.Log("fff" + rotationAmount);
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
        Debug.Log(":(");
        Vector2 value = callbackContext.ReadValue<Vector2>();
        value.Normalize();
        Debug.Log("jjj" + value);
        
        Vector3 moveVector = Vector3.up * value.x + Vector3.left * value.y;
        
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        SpinSpinner(callbackContext);
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
