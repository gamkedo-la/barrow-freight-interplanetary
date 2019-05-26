using UnityEngine;

public class Door : MonoBehaviour
{

    Animator anim;
    public bool isLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLocked)
        {
            anim.SetTrigger("Open");
            anim.ResetTrigger("Close");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Close");
            anim.ResetTrigger("Open");
        }
    }

    public void LockDoor()
    {
        isLocked = true;
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }
}
