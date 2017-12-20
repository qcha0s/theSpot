using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent( typeof( CharacterController ) )]
public class CharacterControllerKart : MonoBehaviour, BaseKartMovement {
    #region Public Variables
    public bool m_gluedToGround = true;
    public float m_acceleration = 8f;
    public float m_bestTurnSpeed = 15f;
    public float m_brakeAcceleration = 20f;
    public float m_forceDeadZone = 0.1f;
    public float m_frictionAcceleration = 2f;
    public float m_gravity = -10f;
    public float m_maxReverseSpeed = 5f;
    public float m_maxSpeed = 20f;
    public float m_maxTurnRadius = 50f;
    public float m_minDriftRadius = 1f;
    public float m_minTurnRadius = 15f;
    public float m_rayCastEpsilon = 0.2f;
    public float m_speedEpsilon = 0.1f;
    public float m_surfaceNormalRotationSpeed = 20f;
    public float m_turnDeadZone = 0.3f;
	public LayerMask m_groundLayer;
    public Vector3 m_gravityDirection = -Vector3.up;
    #endregion
    #region Private Variables
    private bool m_isDrifting = false;
    private bool m_isForceApplied = false;
    private bool m_isGrounded = false;
    private bool m_isTurning = false;
    private float m_currentTurnRadius = 0f;
    private float m_forwardSpeed = 0f;
	private float m_rayCastDistance;
    private float m_yVelocity = 0;
	private Vector3 m_surfaceNormal = Vector3.up;
    private Vector3 m_velocity;
    private CharacterController m_characterController;

    #endregion
    #region Accessors

    public float ForwardSpeed {
        get {
            return m_forwardSpeed;
        }
        set { m_forwardSpeed = value; }
    }


    public float MaxSpeed{
        get{
            return m_maxSpeed;
        }
        set{
            if(value > 0){
                m_maxSpeed = value;
            }
        }
    }
    public float Acceleration{
        get{
            return m_acceleration;
        }
        set{
            if(value > 0){
                m_acceleration = value;
            }
        }
    }
    public bool isGluedToGround{
        get{
            return m_gluedToGround;
        }
        set{
            m_gluedToGround = value;
        }
    } 
    public bool isGrounded{
        get{
            return m_isGrounded;
        }
        set{
            m_isGrounded = value;
        }
    }
    public float Speed{
        get{
            return m_forwardSpeed;
        }
    }

    public float MinTurnRadius {
        get { return m_minTurnRadius; }
        set { m_minTurnRadius = value; }
    }

    public float MaxTurnRadius {
        get { return m_maxTurnRadius; }
        set { m_maxTurnRadius = value; }
    }
    

    #endregion
    #region Public Methods
    public void Gas(float amount){
        if(amount > m_forceDeadZone && m_isGrounded){
            m_isForceApplied = true;
            m_forwardSpeed += amount * m_acceleration * Time.deltaTime;
            m_forwardSpeed = Mathf.Min(m_forwardSpeed, m_maxSpeed);
        }
    }
    public void Brake(float amount){
        if(amount > m_forceDeadZone && m_isGrounded){
            m_isForceApplied = true;
            m_forwardSpeed -= amount * m_brakeAcceleration * Time.deltaTime;
            m_forwardSpeed = Mathf.Max(m_forwardSpeed, -m_maxReverseSpeed);
        }
    }
    public void Turn(float amount){
        if(Mathf.Abs(amount) > m_turnDeadZone){
            m_isTurning = true;
            float underSteerFactor = 0f;
            float turnDirection = amount < 0 ? -1 : 1;
            float actualMinTurnRadius = m_isDrifting ? m_minDriftRadius : m_minTurnRadius; 
            if(m_forwardSpeed != 0){
                underSteerFactor = Mathf.Clamp(m_bestTurnSpeed / m_forwardSpeed, -1, 1);
                m_currentTurnRadius = turnDirection * (m_maxTurnRadius - (m_maxTurnRadius - actualMinTurnRadius) * Mathf.Abs(amount * underSteerFactor));
            }
            else if(underSteerFactor == 0f){
                m_currentTurnRadius = Mathf.Infinity;
            }
        }
        else{
            m_currentTurnRadius = Mathf.Infinity;
            m_isTurning = false;
        }
    }
    public void ResetSteering(){
        m_currentTurnRadius = Mathf.Infinity;
        m_isTurning = false;
    }
    public void SetDrift(bool isDrifting){
        m_isDrifting = isDrifting;
        if(Speed < m_bestTurnSpeed){
            m_isDrifting = false;
        }
    }
    public float GetTurnAmountForTurnRadius(float turnRadius){
        float underSteerFactor = 0f;
        float turnDirection = turnRadius < 0 ? -1 : 1;
        float maxMinTurnRadiusDifference = m_maxTurnRadius - m_minTurnRadius;
        if(m_forwardSpeed != 0){
            underSteerFactor = Mathf.Clamp(m_bestTurnSpeed / m_forwardSpeed, -1, 1);
        }else{
            underSteerFactor = 1;
        }
        float turnAmount = (m_maxTurnRadius - (turnRadius / turnDirection)) / 
            maxMinTurnRadiusDifference * underSteerFactor * turnDirection;
        return turnAmount;
    }
    #endregion
    #region Private & Protected Methods
    void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
        m_rayCastDistance = m_characterController.height * 0.5f + m_rayCastEpsilon;
        m_velocity = new Vector3();
    }
    Vector3 CalculateForwardMovement(){
        if(!m_isForceApplied && m_isGrounded){
            float slowDirection = m_forwardSpeed > 0 ? 1 : -1; 
            m_forwardSpeed -= m_frictionAcceleration * slowDirection * Time.deltaTime;
            if(m_forwardSpeed * slowDirection < m_speedEpsilon){
                m_forwardSpeed = 0;
            }
        }

        Vector3 forwardVelocity = transform.forward * m_forwardSpeed;
        return forwardVelocity;
    }
    Vector3 CalculateUpwardMovement(){
        if(m_isGrounded){
			m_yVelocity = 0f;
		}
		else{
			m_yVelocity += m_gravity * Time.deltaTime;
		}
        return (m_gluedToGround && m_isGrounded ? transform.up : -m_gravityDirection) * m_yVelocity;
    }
    void MakePerpendicularToGround(){
        RaycastHit hitInfo;
        Ray surfaceRay = new Ray(transform.position + m_characterController.center, m_gluedToGround ? -transform.up : m_gravityDirection);
        if(Physics.Raycast(surfaceRay.origin, surfaceRay.direction, out hitInfo, m_rayCastDistance, m_groundLayer)){
            m_surfaceNormal = hitInfo.normal;
            transform.rotation = Quaternion.FromToRotation(transform.up, m_surfaceNormal) * transform.rotation;
        }
        else{
            m_surfaceNormal = -m_gravityDirection;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, m_surfaceNormal) * transform.rotation, Time.deltaTime * m_surfaceNormalRotationSpeed);
        }
    }
    void RotateToTurn(){
        if(m_isTurning){
            float turnDirection = m_currentTurnRadius > 0 ? 1 : -1;
            float angleToRotate = turnDirection * Mathf.Atan2(m_forwardSpeed * Time.deltaTime, Mathf.Abs(m_currentTurnRadius)) * Mathf.Rad2Deg;
            transform.Rotate(0, angleToRotate, 0);
        }
    }
    void FixedUpdate() {
        MakePerpendicularToGround();
        m_velocity = CalculateForwardMovement() + CalculateUpwardMovement();
        m_isGrounded = (m_characterController.Move(m_velocity * Time.deltaTime) & CollisionFlags.Below) != 0;
        RotateToTurn();
        m_isForceApplied = false;
    }
    #endregion
}
