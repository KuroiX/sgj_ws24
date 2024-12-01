using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using FindingMemo;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Spin : MonoBehaviour
{
    [SerializeField] private Score scoreManager;
    [SerializeField] private GameManager gameManager;
    
    public InputAction spinAction;
    public Vector3 spinSpeed = new Vector3(0,0,0f);

    private SpinAction actions;
    
    public float rotationSpeed = 1; 
    public int spinScore = 0; 
    public int spinScoreThreshold = 99; 
    public float speedMultiplier = 1.0f;
    private float rotationAmount;

    public Sprite[] sprites;
    private SpriteRenderer _spriteRenderer;
    private int _spriteNumber = 0;
    
    
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
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = sprites[_spriteNumber];
    }

    // Update is called once per frame
    void Update()
    {
        /* gives errors vector nan
        transform.Rotate(spinSpeed);

        spinSpeed *= 0.9f * Time.deltaTime * 500;
        */

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("score" + spinScore);
        spinScore += 1;
        scoreManager.HitNeuron(Vector2.zero);
    }

    private void SpinSpinner(InputAction.CallbackContext callbackContext)
    {
        rotationAmount += rotationSpeed * speedMultiplier;
        spinSpeed.z =  rotationAmount;
        transform.Rotate(spinSpeed);
        
        ChangeSprite();
        
        AddSpinScoreToTotal();
        
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
        
        ChangeSprite();
        
        AddSpinScoreToTotal();
    }
    
    
    private void AddSpinScoreToTotal()
    {
        if (spinScore >= spinScoreThreshold)
        {
            Debug.Log("Addscore" + spinScore);
            //ScoreManager.AddScore(spinScore);
        }
    }

    private void ChangeSprite()
    {
        if (_spriteNumber >= sprites.Length)
        {
            gameManager.GameState = GameState.End;
            return;
        }
        
        if ((_spriteNumber < sprites.Length)  && (spinScore >= spinScoreThreshold / sprites.Length * _spriteNumber))
        {
            _spriteRenderer.sprite = sprites[_spriteNumber];
            _spriteNumber++;
        }

    }
}
