using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{

    bool isHoldingObject = false;
    GameObject heldObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Mouse Clicked");

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;

            if (Physics.Raycast(mouseRay, out rhInfo, 3.0f)) {
                if (isHoldingObject && rhInfo.collider.gameObject.tag == "TerminalBay") {
                    Debug.Log("Mouse ray hit bay " + rhInfo.collider.gameObject.name + " at " + rhInfo.point);
                    placeObject(rhInfo.collider.gameObject);
                } else if (isHoldingObject) {
                    dropObject();
                } else if (Physics.Raycast(mouseRay, out rhInfo, 3.0f)) {
                    Debug.Log("Mouse ray hit " + rhInfo.collider.gameObject.name + " at " + rhInfo.point);
                    if (rhInfo.collider.gameObject.tag == "MovableObject") {
                        pickUpObject(rhInfo.collider.gameObject);
                    }
                } else {
                    Debug.Log("Mouse ray hit nothing.");
                }
            }

   
        }
    }

    void pickUpObject(GameObject targetObject) {

        isHoldingObject = true;

        heldObject = targetObject;
        heldObject.transform.SetParent(this.transform);

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

        toggleGravity();
    }

    void placeObject(GameObject bay) {
        isHoldingObject = false;
        heldObject.transform.SetParent(null);

        heldObject.transform.position = bay.transform.position;
        heldObject.transform.rotation = bay.transform.rotation;
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();

        TerminalBay targetBay = bay.GetComponent<TerminalBay>();
        targetBay.installModule();
    }

    void toggleGravity() {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        rb.useGravity = !rb.useGravity;
        rb.isKinematic = !rb.isKinematic;
    }
}
