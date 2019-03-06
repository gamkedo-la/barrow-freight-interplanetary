using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{

    bool isHoldingObject = false;
    float interactionRange = 3.0f;

    GameObject heldObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log("Mouse Clicked");

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;

            if (Physics.Raycast(mouseRay, out rhInfo, interactionRange)) {
                if (isHoldingObject && rhInfo.collider.gameObject.tag == "TerminalBay") {

                    //Debug.Log("Mouse ray hit bay " + rhInfo.collider.gameObject.name + " at " + rhInfo.point);
                    placeObject(rhInfo.collider.gameObject);

                } else if (isHoldingObject) { //end of if isHoldingObject and target is TerminalBay.

                    dropObject();

                } else if (Physics.Raycast(mouseRay, out rhInfo, 3.0f)) { //end of if isHoldingObject

                    //Debug.Log("Mouse ray hit " + rhInfo.collider.gameObject.name + " at " + rhInfo.point);
                    if (rhInfo.collider.gameObject.tag == "MovableObject") {
                        pickUpObject(rhInfo.collider.gameObject);
                    } //end of if hit object is movable

                } else { //end of if raycast hits object

                    //Debug.Log("Mouse ray hit nothing.");

                } //end of else
            } //end of if a raycast hits something
        } // end of if mouse button 0 is pressed
    } // end of Update()

    void pickUpObject(GameObject targetObject) {

        isHoldingObject = true;

        heldObject = targetObject;
        heldObject.transform.SetParent(this.transform);

        //notifies the heldObject that is has been picked up.
        portableObject po = heldObject.GetComponent<portableObject>();
        po.PickupObject();

        //Holds the object in position relative to the player camera
        Vector3 pos = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y - 0.5f, Camera.main.transform.localPosition.z + 1f);
        heldObject.transform.localPosition = pos;
        heldObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

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
}
