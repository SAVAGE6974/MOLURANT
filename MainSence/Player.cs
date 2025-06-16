using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    
    private float _moveSpeed = 5f;
    private CharacterController _cc;
    
    public Transform playerCamera;
    public float mouseSensitivity = 1000f;
    public float rotation;
    
    public bool isMouseLookEnabled = true;
    
    void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1000f);
    }

    void Update()
    {
        PlayerMovement();
        if (NEWUI.canFire)
        {
            HandleMouseLook();
        }

        if (SceneManager.GetActiveScene().name == "Setting")
        {
            SetMouseSensitivity();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    private void SetMouseSensitivity()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (mouseSensitivity >= 0)
                mouseSensitivity -= 100f;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (mouseSensitivity >= 0)
                mouseSensitivity += 100f;
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            if (mouseSensitivity >= 0)
                mouseSensitivity -= 10f;
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            if (mouseSensitivity >= 0)
                mouseSensitivity += 10f;
        }
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity); // 저장
        PlayerPrefs.Save();
    }
    
    private void PlayerMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
         
        Vector3 movement = transform.TransformDirection(new Vector3(moveX, 0, moveZ));
        _cc.Move(movement * (_moveSpeed * Time.deltaTime));
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(rotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
