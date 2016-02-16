using UnityEngine;
using System.Collections;

public class PlayerWeaponController : MonoBehaviour 
{
    public WeaponGun gun;
    public WeaponMissile[] missile; //Left and Right missile pod
    public Transform Turret;
    //private Transform Recticle; //The Recticle object, the mouse cursor graphic

	private bool isShooting = false;
	// Use this for initialization
	void Start () 
    {
        //if (!Recticle)
        //    Recticle = GameObject.Find("Recticle_Player").transform;
	}

	void OnEnable(){
		EasyJoystick.On_JoystickMove += On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
		//已经在inspector里面写了event sendMessage
//		EasyButton.On_ButtonPress += On_ButtonPress;
//		EasyButton.On_ButtonUp += On_ButtonUp;	
		//EasyButton.On_ButtonDown += On_ButtonDown;
	}
	
	
	void OnDisable(){
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
//		EasyButton.On_ButtonPress -= On_ButtonPress;
//		EasyButton.On_ButtonUp -= On_ButtonUp;	
	}
	
	void OnDestroy(){
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
//		EasyButton.On_ButtonPress -= On_ButtonPress;
//		EasyButton.On_ButtonUp -= On_ButtonUp;	
	}
	
	void On_JoystickMove( MovingJoystick move){

		if ("Rotate_straff_Joystick" == move.joystickName) {
						float angle = move.Axis2Angle (true);
//						Debug.Log ("move.joystickAxis.sqrMagnitude=" + move.joystickAxis.sqrMagnitude);
						Turret.transform.localRotation = Quaternion.Euler (new Vector3 (0, angle, 0));

						if (!isShooting) {
								isShooting = true;
								gun.Shoot ();
						}
				}
	}
	
	void On_JoystickMoveEnd (MovingJoystick move)
	{
		if ("Rotate_straff_Joystick" == move.joystickName) {
						isShooting = false;
						gun.StopShoot ();
				}
	}
	

//	void On_ButtonPress (string buttonName)
//	{
//		Debug.Log ("On_ButtonPress buttonName=" + buttonName);
//		if (buttonName=="Fire"){
//			gun.Shoot();
//		}
//	}
//	
//	void On_ButtonUp (string buttonName)
//	{
//		Debug.Log ("On_ButtonUp buttonName=" + buttonName);
//		if(buttonName == "missile")
//			stopMissile();
//	}	

	void launchMissile(){
//		Debug.LogError ("launchmissle");
		for (int i=0; i < missile.Length; i++) {
			missile[i].Shoot();
		}
	}

	void stopMissile(){
//		Debug.LogError ("stopMissile");
		for (int i=0; i < missile.Length; i++) {
			missile[i].StopShoot();
		}
	}
}
