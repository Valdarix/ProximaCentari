using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Animator _anim;
    private PlayerState _currentPlayerState;
    [SerializeField] private GameObject _thruster;

    public enum PlayerState
    {
        Idle,
        Horizontal,
        Vertical,
        PowerChange,
        Death
    }
  
    // Start is called before the first frame update
    void Start()
    {
        _currentPlayerState = PlayerState.Idle;
        _anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleStateChange();
    }

    public void UpdatePlayerState(PlayerState newState)
    {
        _currentPlayerState = newState;
    }

    private void HandleStateChange()
    {
        switch (_currentPlayerState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Horizontal:
                break;
            case PlayerState.Vertical:
                break;
            case PlayerState.PowerChange:
                break;
            case PlayerState.Death:
                break;
            default:
                Debug.LogError("Player has no state");
                break;
        }
    }

    private void HandleMovement()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");

        var vertical = Input.GetAxisRaw("Vertical");
        _thruster.SetActive(horizontal > 0 || vertical != 0);
       // _anim.SetFloat("VerticalInput", vertical);
        var movVector = new Vector3(horizontal, 0, -vertical);
        
        transform.Translate(movVector * (_speed * Time.deltaTime));
        
        var position = transform.position;
        var yClamp = Mathf.Clamp(position.y, -7f, 7f);
        var xClamp = Mathf.Clamp(position.x, -11.5f, 11.5f);
        
        position = new Vector3(xClamp, yClamp, 0);
        transform.position = position;
    }
}
