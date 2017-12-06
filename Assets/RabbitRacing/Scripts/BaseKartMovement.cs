using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseKartMovement : MonoBehaviour {
    #region Public Variables
    public float m_acceleration;
    public float m_brakeAcceleration;
    public float m_frictionAcceleration;
    public float m_maxSpeed;
    public float m_maxReverseSpeed;
    public float m_speedEpsilon;
    public float m_bestTurnSpeed;
    public float m_maxTurnRadius;
    public float m_minTurnRadius;
    public float m_forceDeadZone;
    public float m_turnDeadZone;
    public float m_minDriftRadius;
    #endregion
    #region Private Variables
    private bool m_isGrounded = false;
    private bool m_isDrifting = false;
    private bool m_isForceApplied = false;
    private bool m_isTurning = false;
    private float m_currentTurnRadius = 0f;
    private float m_speed = 0f;
    private Vector3 m_velocity;
    private CharacterController m_characterController;
    #endregion
    #region Accessors
    #endregion
    #region Methods
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        m_velocity = new Vector3();
        m_characterController = GetComponent<CharacterController>();
    }
    public void Gas(float amount){
        // Mathf.Clamp(amount, 0, 1);
        if(amount > m_forceDeadZone){
            m_isForceApplied = true;
            m_speed += amount * m_acceleration * Time.deltaTime;
            m_speed = Mathf.Min(m_speed, m_maxSpeed);
        }
    }
    public void Brake(float amount){
        // Mathf.Clamp(amount, 0, 1);
        if(amount > m_forceDeadZone){
            m_isForceApplied = true;
            m_speed -= amount * m_brakeAcceleration * Time.deltaTime;
            m_speed = Mathf.Max(m_speed, -m_maxReverseSpeed);
        }
    }
    public void Turn(float amount){
        // Mathf.Clamp(amount, -1, 1);
        if(Mathf.Abs(amount) > m_turnDeadZone){
            m_isTurning = true;
            float underSteerFactor = 0f;
            float minTurnRadius = m_isDrifting ? m_minDriftRadius : m_minTurnRadius;
            float turnDirection = amount < 0 ? -1 : 1; 
            if(m_speed != 0){
                underSteerFactor = Mathf.Clamp(m_bestTurnSpeed / m_speed, -1, 1);
            }
            m_currentTurnRadius = turnDirection * (m_maxTurnRadius - (m_maxTurnRadius - m_minTurnRadius) * Mathf.Abs(amount * underSteerFactor));
            if(underSteerFactor == 0f){
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
    }
    void FixedUpdate() {

        if(!m_isForceApplied){
            float slowDirection = m_speed > 0 ? 1 : -1; 
            m_speed -= m_frictionAcceleration * slowDirection * Time.deltaTime;
            if(m_speed * slowDirection < m_speedEpsilon){
                m_speed = 0;
            }
        }

        m_velocity = new Vector3(0, 0, 1);
        m_velocity *= m_speed;
        m_velocity = transform.TransformDirection(m_velocity);
        m_isGrounded = (m_characterController.Move(m_velocity * Time.deltaTime) & CollisionFlags.Below) != 0;

        if(m_isTurning){
            float turnDirection = m_currentTurnRadius > 0 ? 1 : -1;
            float angleToRotate = turnDirection * Mathf.Atan2(m_speed * Time.deltaTime, Mathf.Abs(m_currentTurnRadius)) * Mathf.Rad2Deg;
            transform.Rotate(0, angleToRotate, 0);
        }
        else{
            m_isDrifting = false;
        }
        
        m_isForceApplied = false;
    }
    #endregion

	
}
