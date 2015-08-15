using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearDisplay : MonoBehaviour {

	const int NUM_GEAR_SLOTS = 4;
	public const int HELMET_INDEX = 0;				//Holds indices for different types of gear slots
	public const int CHEST_INDEX = 1;
	public const int WEAPON_INDEX = 2;
	public const int LEGS_INDEX = 3;

	public List<GearDisplaySlot> gearSlots;

	// Use this for initialization
	void Start () {
		Debug.Log("IN START() INSIDE CHARACTER_SCRIPT");
		for (int i = 0; i < NUM_GEAR_SLOTS; i++) {
			gearSlots.Add (transform.GetChild(i).GetComponent<GearDisplaySlot>());
			gearSlots[i].GetComponent<GearDisplaySlot>().index = i;
			Debug.Log("Index at position " + i + ": " + gearSlots[i].GetComponent<GearDisplaySlot>().index);
		}
	
	}
}
