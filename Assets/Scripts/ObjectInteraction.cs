using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{

    bool isHoldingObject = false;
    float interactionRange = 3.0f;
    float objectMovementStartTime;
    float objectMovementDistance;
    Quaternion startingRotation;
    Quaternion endingRotation = Quaternion.Euler(0,0,0);
    Quaternion objectRotationDistance;
    Vector3 lockedCameraPosition;
    Vector3 initialPlayerPosition;

    bool viewLocked = false;

    Vector3 handPosition;

    GameObject heldObject;
    portableObject po;
    TerminalBay targetBay;
    TerminalMonitor targetMonitor;
    Terminal targetTerminal;
    Camera mainCamera;

    public GameObject terminalBeepsManager;

    private TerminalBeeps terminalBeepsScript;

    private Jobs jobsManager;
    private TerminalStore terminalStore;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        terminalBeepsScript = GameObject.Find("TerminalBeepsManager").GetComponent<TerminalBeeps>();
        terminalBeepsManager = GameObject.Find("TerminalBeepsManager");
        jobsManager = GameObject.Find("Game Managers").GetComponent<Jobs>();
        terminalStore = GameObject.Find("Game Managers").GetComponent<TerminalStore>();

    }

    // Update is called once per frame
    void Update()
    {

        handPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y - 0.5f, Camera.main.transform.localPosition.z + 1f);

        if (Input.GetMouseButtonDown(1)){
            viewLocked = false;
            GetComponent<FirstPersonController>().enabled = true;
            terminalBeepsScript.resetTerminalStartupAndRunningSound();
            terminalBeepsScript.terminalOffSound.Play();
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rhInfo;

            //If an object is clicked...
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rhInfo, interactionRange)) {
                Debug.Log("clicked");
                //..and if that object is a Terminal Monitor
                if (rhInfo.collider.gameObject.tag == "TerminalMonitor") {
                    Debug.Log("rhInfo.collider.gameObject.tag == TerminalMonitor");
                    //Debug.Log("Monitor Clicked");
                    viewLocked = true;
                    targetMonitor = rhInfo.collider.gameObject.GetComponent<TerminalMonitor>();
                    targetTerminal = targetMonitor.GetComponentInParent<Terminal>();

                    initialPlayerPosition = transform.position;
                    lockedCameraPosition = targetTerminal.transform.TransformPoint(Vector3.left * 1.25f);
                    lockedCameraPosition.y = initialPlayerPosition.y;

                    terminalBeepsManager.transform.position = targetTerminal.transform.position;
                    terminalBeepsScript.playInitialBeep();
                    terminalBeepsScript.playDelayedBeeps();
                    terminalBeepsScript.playTerminalStartupAndRunningSound();

                    if (targetTerminal.terminalType == Terminal.TerminalTypes.JobSelection) {
                        jobsManager.GenerateAvailableJobs();
                    }

                    if (targetTerminal.terminalType == Terminal.TerminalTypes.TerminalStore) {
                        terminalStore.GenerateAvailableTerminals();
                    }
                //..and if that object is a terminal bay and the player holds an object
                } else if (isHoldingObject && rhInfo.collider.gameObject.tag == "TerminalBay") {
                    Debug.Log("isHoldingObject && rhInfo.collider.gameObject.tag == TerminalBay");
                    placeObject(rhInfo.collider.gameObject);

                //...and if that object is a terminal bay and the player does not hold an object..
                } else if (!isHoldingObject && rhInfo.collider.gameObject.tag == "TerminalBay") {
                    Debug.Log("!isHoldingObject && rhInfo.collider.gameObject.tag == TerminalBay");
                    //...and if that terminal bay already has an object installed.
                    targetBay = rhInfo.collider.gameObject.GetComponent<TerminalBay>();
                    if (targetBay.IsModuleInstalled()) {
                        GameObject installedObject = targetBay.GetInstalledObject();
                        portableObject po = installedObject.GetComponent<portableObject>();
                        po.DeactivateObject();
                    }
                    
                } else if (rhInfo.collider.gameObject.tag == "TerminalPlacementCollider") {

                    Debug.Log("placement Collider clicked");
                    targetTerminal = rhInfo.collider.gameObject.GetComponentInParent<Terminal>();

                    portableObject po = heldObject.GetComponent<portableObject>();
                    po.PlaceTerminal(targetTerminal.transform.position);

                //...and if the object is not interactable and the player holds an object.
                } else if (isHoldingObject) { //end of if isHoldingObject and target is TerminalBay.
                    dropObject();
                //...and if the object is a movable object
                } else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rhInfo, 3.0f)) { //end of if isHoldingObject
                    if (rhInfo.collider.gameObject.tag == "MovableObject") {

                        portableObject po = rhInfo.collider.gameObject.GetComponent<portableObject>();
                        if (!po.IsObjectInstalled()) {
                            pickUpObject(rhInfo.collider.gameObject);
                        } else {
                            po.ActivateObject();
                        }


                    } //end of if hit object is movable

                

                } else { //end of if raycast hits object

                    //Debug.Log("Mouse ray hit nothing.");

                } //end of else
            } //end of if a raycast hits something
        } // end of if mouse button 0 is pressed

        if (Input.GetKey(KeyCode.E))
        {
            if (isHoldingObject)
            {
                portableObject po = heldObject.GetComponent<portableObject>();
                if (po.IsFireExtinguisher())
                {
                    po.SprayFoam();
                }
            }
        }

        if (isHoldingObject) {
            MoveObjectToHands();
        }

        if (viewLocked) {

            GetComponent<FirstPersonController>().enabled = false;
            mainCamera.transform.LookAt(targetMonitor.transform);
            transform.position = lockedCameraPosition;

        }

    } // end of Update()

    void pickUpObject(GameObject targetObject) {

        objectMovementStartTime = Time.time;

        isHoldingObject = true;

        heldObject = targetObject;
        heldObject.transform.SetParent(this.transform);
        objectMovementDistance = Vector3.Distance(heldObject.transform.position, handPosition);

        startingRotation = heldObject.transform.rotation;
        objectRotationDistance = Quaternion.Inverse(startingRotation) * endingRotation;

        //notifies the heldObject that is has been picked up.
        portableObject po = heldObject.GetComponent<portableObject>();
        po.PickupObject();

        MoveObjectToHands();

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb.useGravity) {
            toggleGravity();
        }
    }

    void dropObject() {
        isHoldingObject = false;
        heldObject.transform.SetParent(null);

        //notifies the heldObject that is has been dropped.
        portableObject po = heldObject.GetComponent<portableObject>();
        po.DropObject();
        po = null;
        toggleGravity();
    }

    void placeObject(GameObject bay) {
        isHoldingObject = false;
        heldObject.transform.SetParent(null);

        heldObject.transform.position = bay.transform.position;
        heldObject.transform.rotation = bay.transform.rotation;
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();

        TerminalBay targetBay = bay.GetComponent<TerminalBay>();

		SpeechService.Instance.SpeakMessage( $"{heldObject.name} inserted in to {bay.name}" );

		targetBay.AttachObjectToBay(heldObject);
        heldObject.transform.SetParent(targetBay.transform.parent.gameObject.transform);

        //notifies the heldObject that is has been installed in a terminal.
        portableObject po = heldObject.GetComponent<portableObject>();
        po.InstallObject();
    }

    //Probably should be moved to the portable objects script.
    void toggleGravity() {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        rb.useGravity = !rb.useGravity;
        rb.isKinematic = !rb.isKinematic;
    }

    void MoveObjectToHands() {
        float speed = 1.0f;
        float rotationSpeed = 1.0f;

        // Distance moved = time * speed.
        float distCovered = (Time.time - objectMovementStartTime) * speed;
        float rotationAmount = (Time.time - objectMovementStartTime) * rotationSpeed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / objectMovementDistance;

        // Set object position as a fraction of the distance between the object and hand position.
        heldObject.transform.localPosition = Vector3.Lerp(heldObject.transform.localPosition, handPosition, fracJourney);
        heldObject.transform.localRotation = Quaternion.Lerp(startingRotation, endingRotation, rotationAmount);
    }
}
