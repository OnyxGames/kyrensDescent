using UnityEngine;
using System.Collections;

public class ToggleInventory : MonoBehaviour {

	GameObject inventory;
	GameObject gearDisplay;
	
	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag ("Inventory");
		gearDisplay = GameObject.FindGameObjectWithTag ("GearDisplay");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("b")) {
			inventory.SetActive(!inventory.activeInHierarchy);
			gearDisplay.SetActive(!gearDisplay.activeInHierarchy);
		}
	}
}
