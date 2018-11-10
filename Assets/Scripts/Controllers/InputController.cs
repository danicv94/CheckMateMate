using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class InputController : MonoBehaviour
{
    public GameController GameController;

    public GameObject AndroidInput;
    
    public VirtualJoystick Direction;
    public VirtualJoystick Aim;

    private float _acumulatedScroll;

    // Use this for initialization
    void Start()
    {
        if (!Application.isMobilePlatform)
        {
            AndroidInput.SetActive(false);
        }

        _acumulatedScroll = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isMobilePlatform)
        {
            Vector3 dir = Direction.InputDirection;
            GameController.MovePlayer(new Vector2(dir.x, dir.z));
            if (Aim.InputDirection.x > 0 || Aim.InputDirection.z > 0 || Aim.InputDirection.x < 0 || Aim.InputDirection.z < 0)
            {
                GameController.PlayerController.Shoot(new Vector2(Aim.InputDirection.x, Aim.InputDirection.z));
            }
        }
        else
        {
            GameController.MovePlayer(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            if (Input.GetMouseButton(0))
            {
                GameController.PlayerController.Shoot(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition) -
                    GameController.PlayerController.transform.position);
            }

            _acumulatedScroll += Input.GetAxis("Mouse ScrollWheel");
            if (_acumulatedScroll > 0.5)
            {
                _acumulatedScroll = 0;
                GameController.ChangeWeapon(1);
            }
            else if (_acumulatedScroll < -0.5)
            {
                _acumulatedScroll = 0;
                GameController.ChangeWeapon(-1);
            }
        }
    }
}