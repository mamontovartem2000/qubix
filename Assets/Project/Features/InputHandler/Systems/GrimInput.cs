using UnityEngine;

public class GrimInput : MonoBehaviour
{
    private static PlayerInput _input;

    private void Start()
    {
        _input = new PlayerInput();
        _input.Enable();
        SubscribeToMethods();

    }

    private void SubscribeToMethods()
    {
        _input.Player.MoveForward.started += ctx => StartMoveForward();
        _input.Player.MoveForward.canceled += ctx => EndMoveForward();

        _input.Player.MoveBackward.started += ctx => StartMoveBackward();
        _input.Player.MoveBackward.canceled += ctx => EndMoveBackward();

        _input.Player.RotateLeft.performed += ctx => RotateLeft();
        _input.Player.RotateRight.performed += ctx => RotateRight();

        _input.Player.LeftShoot.started += ctx => StartLeftShoot();
        _input.Player.LeftShoot.canceled += ctx => ReleseLeftShoot();

        _input.Player.RightShoot.started += ctx => StartRightShoot();
        _input.Player.RightShoot.canceled += ctx => ReleseRightShoot();
    }

    private void StartMoveForward()
    {
        if (_input.Player.MoveBackward.IsPressed())
        {

        }
        else
        {

        }
    }

    private void StartMoveBackward()
    {
        if (_input.Player.MoveForward.IsPressed())
        {

        }
        else
        {

        }
    }

    private void EndMoveForward()
    {
        if (_input.Player.MoveBackward.IsPressed())
        {

        }
        else
        {

        }
    }

    private void EndMoveBackward()
    {
        if (_input.Player.MoveForward.IsPressed())
        {

        }
        else
        {

        }
    }

    private void RotateLeft()
    {
        
    }

    private void RotateRight()
    {
        
    }

    private void StartLeftShoot()
    {
        
    }

    private void StartRightShoot()
    {

    }

    private void ReleseLeftShoot()
    {

    }

    private void ReleseRightShoot()
    {

    }
}