using UnityEngine;
using System.Collections;

public class PlayerCarController : Car 
{
    protected override void Initialise() 
    {
 
    }

    protected override void CarUpdate() 
    {
 
    }


	void OnEnable(){
		EasyJoystick.On_JoystickMove += On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
	}
	
	
	void OnDisable(){
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
	}
	
	void OnDestroy(){
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
	}

	void On_JoystickMove( MovingJoystick move){
		if ("Move_Turn_Joystick" == move.joystickName) {
			throttle = move.joystickAxis.y; 
			steer = move.joystickAxis.x;
		}
	}
	
	void On_JoystickMoveEnd (MovingJoystick move)
	{
		if ("Move_Turn_Joystick" == move.joystickName) {
			throttle = 0.0f;
			steer = 0.0f;
		}
	}
}
