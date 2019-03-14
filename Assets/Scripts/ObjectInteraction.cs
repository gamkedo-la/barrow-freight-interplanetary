using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{

    bool isHoldingObject = false;
    float interactionRange = 3.0f;
    float objectMovementStartTime;
    float objectMovementDistance;

    Vector3 handPosition;

    GameObject heldObject;
    portableObject po;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        handPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y - 0.5f, Camera.main.transform.localPosition.z + 1f);

        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log("Mouse Clicked");

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;

            if (Physics.Raycast(mouseRay, out rhInfo, interactionRange)) {
                if (isHoldingObject && rhInfo.collider.gameObject.tag == "TerminalBay") {

                    placeObject(rhInfo.collider.gameObject);

                } else if (isHoldingObject) { //end of if isHoldingObject and target is TerminalBay.

                    dropObject();

                } else if (Physics.Raycast(mouseRay, out rhInfo, 3.0f)) { //end of if isHoldingObject
                    if (rhInfo.collider.gameObject.tag == "MovableObject") {

                        pickUpObject(rhInfo.collider.gameObject);

                    } //end of if hit object is movable

                } else { //end of if raycast hits object

                    //Debug.Log("Mouse ray hit nothing.");

                } //end of else
            } //end of if a raycast hits something
        } // end of if mouse button 0 is pressed

        if (isHoldingObject) {
            MoveObjectToHands();
        }

    } // end of Update()

    void pickUpObject(GameObject targetObject) {

        objectMovementStartTime = Time.time;

        isHoldingObject = true;

        heldObject = targetObject;
        heldObject.transform.SetParent(this.transform);
        objectMovementDistance = Vector3.Distance(heldObject.transform.position, handPosition);

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
        float speed = 1.0F;

        // Distance moved = time * speed.
        float distCovered = (Time.time - objectMovementStartTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / objectMovementDistance;

        // Set object position as a fraction of the distance between the object and hand position.
        heldObject.transform.localPosition = Vector3.Lerp(heldObject.transform.localPosition, handPosition, fracJourney);
        heldObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
