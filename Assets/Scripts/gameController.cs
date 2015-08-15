using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

    public GameObject inventory;
    public GameObject gearDisplay;

    // Use this for initialization
    void Start()
    {
        inventory.SetActive(!inventory.activeInHierarchy);
        gearDisplay.SetActive(!gearDisplay.activeInHierarchy);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            inventory.SetActive(!inventory.activeInHierarchy);
            gearDisplay.SetActive(!gearDisplay.activeInHierarchy);
        }
    }
}
