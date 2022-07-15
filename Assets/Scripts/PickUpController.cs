using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    private WeaponSwitching weaponSwitching;
    private Rigidbody rb;
    private BoxCollider coll;
    private WeaponBehavior weaponBehaviorScript;
    public Transform player, weaponContainer;

    private float pickUpRange = 2.0f;

    private bool equipped;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        weaponSwitching = GameObject.Find("Weapon Container").GetComponent<WeaponSwitching>();
        weaponBehaviorScript = FindObjectOfType<WeaponBehavior>();
    }

    void Update()
    {
        // Check if Player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) 
            && !weaponSwitching.slotFull) PickUp();

        // Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUp()
    {
        weaponBehaviorScript.enabled = true;

        equipped = true;

        // Make Rigidbody is kinematic and BoxCollider isTrigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        // Make weapon a child of the player and move it to default position
        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(new Vector3(-90f, 0, 0f));
        transform.localScale = Vector3.one;
    }

    private void Drop()
    {
        weaponBehaviorScript.enabled = false;

        equipped = false;

        // Set parent to null
        transform.SetParent(null);

        // Make Rigidbody is not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;
    }
}
