using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 _inputVector;

    // Update is called once per frame
    private void Update()
    {

        _inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public NetworkInputData SendInput()
    {
        NetworkInputData inputData = new NetworkInputData();
        inputData.moveDirection = _inputVector;

        return inputData;
    }
}
