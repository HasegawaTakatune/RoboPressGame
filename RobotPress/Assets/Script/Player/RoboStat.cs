using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class RobotManager : MonoBehaviour {
public class RoboStat: MonoBehaviour{

	public const byte
	Idol = 0,
	Targetting = 1,
	Attack = 2,
	Catch = 3,
	Delete = 4;
	public static byte status;
}