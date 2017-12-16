using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseKartMovement {
	bool isGrounded{
        get;
        set;
    }
    float Speed{
        get;
    }
	void Gas(float amount);
    void Brake(float amount);
	void Turn(float amount);
    void ResetSteering();
    void SetDrift(bool isDrifting);
    float GetTurnAmountForTurnRadius(float turnRadius);
}
