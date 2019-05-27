using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    Animator anim;
    public bool isLocked = false;
    public bool lockMessagePlayed = false;
    public TerminalBay powerBay;
    Scene currentScene;
    Movement boombotMovement;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        powerBay = GameObject.Find("Power Bay").GetComponent<TerminalBay>();
        currentScene = SceneManager.GetActiveScene();
        boombotMovement = GameObject.Find("Boom Bot").GetComponent<Movement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLocked)
        {
            anim.SetTrigger("Open");
            anim.ResetTrigger("Close");
        }

        if (isLocked)
        {
            if (powerBay.IsModuleInstalled())
            {
                UnlockDoor();
            }

            if (!lockMessagePlayed)
            {
                SpeechService.Instance.SpeakMessage("That's odd.  The door has locked.  And it seems that the Power Terminal just to the right of this door has had it's power core removed.  You will need to find a power core and place it back in the Power Terminal to power and unlock that door.");
                lockMessagePlayed = true;
            }
        }

        Debug.Log(currentScene.buildIndex);
        if (currentScene.buildIndex == 1)
        {
            boombotMovement.StartMovement();
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
