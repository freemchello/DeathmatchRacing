using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.GlobalIllumination;

public class CarControl : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float _horizontalInput;
    private float _verticalInput;
    private float _steerAngle;

    private float _currentStearAngle;
    private float _currentBreakForce;
    private bool _isBreaking;

    [SerializeField] private WheelCollider _frontLeftWheelCollider;
    [SerializeField] private WheelCollider _frontRightWheelCollider;
    [SerializeField] private WheelCollider _backLeftWheelCollider;
    [SerializeField] private WheelCollider _backRightWheelCollider;

    [SerializeField] private Transform _frontLeftWheelTransform;
    [SerializeField] private Transform _frontRightWheelTransform;
    [SerializeField] private Transform _backLeftWheelTransform;
    [SerializeField] private Transform _backRightWheelTransform;

    [SerializeField] private float _motorForce;
    [SerializeField] private float _breakForce;
    [SerializeField] private float _maxSteerAngle;

    //[SerializeField] private Light _rearLeftLight;
    //[SerializeField] private Light _rearRightLight;

    internal float _currentSpeed;

    private void FixedUpdate()
    {
        GetInput();
        HandlerMotor();
        HandleSteering();
        UpdateWheels();
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
        _currentSpeed = Mathf.Abs(localVelocity.z) * 3.6f;
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxis(HORIZONTAL);
        _verticalInput = Input.GetAxis(VERTICAL);
        _isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandlerMotor()
    {
        _frontLeftWheelCollider.motorTorque = _verticalInput * _motorForce;
        _frontRightWheelCollider.motorTorque = _verticalInput * _motorForce;
        _backLeftWheelCollider.motorTorque = _verticalInput * _motorForce;
        _backRightWheelCollider.motorTorque = _verticalInput * _motorForce;

        _currentBreakForce = _isBreaking ? _breakForce : 0f;

        if (_isBreaking)
        {
            ApplyBreaking();
        }

        else if (!_isBreaking)
        {
            StopBreaking();
        }
    }

    private void ApplyBreaking()
    {
        _frontLeftWheelCollider.brakeTorque = _currentBreakForce;
        _frontRightWheelCollider.brakeTorque = _currentBreakForce;


        float currentSteerAngle = _frontLeftWheelCollider.steerAngle;


        float newSteerAngle = Mathf.Lerp(currentSteerAngle, _maxSteerAngle * Mathf.Sign(_horizontalInput), Time.deltaTime * 2f);
        _frontLeftWheelCollider.steerAngle = newSteerAngle;
        _frontRightWheelCollider.steerAngle = newSteerAngle;


        Vector3 force = transform.forward * _motorForce * 0.5f;
        GetComponent<Rigidbody>().AddForceAtPosition(force, transform.position + transform.forward * 2f);
        GetComponent<Rigidbody>().AddTorque(transform.up * _motorForce * 0.1f * Mathf.Sign(_horizontalInput));

        // Включаем свет на объектах Point Light
        //_rearLeftLight.enabled = true;
        //_rearRightLight.enabled = true;
    }

    private void StopBreaking()
    {
        _frontLeftWheelCollider.brakeTorque = 0f;
        _frontRightWheelCollider.brakeTorque = 0f;
        _backLeftWheelCollider.brakeTorque = 0f;
        _backRightWheelCollider.brakeTorque = 0f;

        // Вылючаем свет на объектах Point Light
        //_rearLeftLight.enabled = false;
        //_rearRightLight.enabled = false;
    }

    private void HandleSteering()
    {
        _currentStearAngle = _maxSteerAngle * _horizontalInput;
        _frontLeftWheelCollider.steerAngle = _currentStearAngle;
        _frontRightWheelCollider.steerAngle = _currentStearAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheels(_frontLeftWheelCollider, _frontLeftWheelTransform);
        UpdateSingleWheels(_frontRightWheelCollider, _frontRightWheelTransform);
        UpdateSingleWheels(_backLeftWheelCollider, _backLeftWheelTransform);
        UpdateSingleWheels(_backRightWheelCollider, _backRightWheelTransform);
    }

    private void UpdateSingleWheels(WheelCollider WheelCollider, Transform WheelTransform)
    {
        Vector3 position;
        Quaternion rotation;

        WheelCollider.GetWorldPose(out position, out rotation);
        WheelTransform.rotation = rotation;
        WheelTransform.position = position;
    }
}