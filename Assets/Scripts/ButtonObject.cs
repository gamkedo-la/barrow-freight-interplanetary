using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

enum ButtonType
{
	Standard,
	Toggle,
	Hold
}

public class ButtonObject : MonoBehaviour, IInteractable
{
	[SerializeField] private Animator animator = null;
	[SerializeField] private GameObject highlight = null;
	[SerializeField] private ButtonType buttonType = ButtonType.Standard;
	[SerializeField] private bool isOn = false;
	[Header("Events")]
	[SerializeField] private UnityEvent OnEvent = null;
	[SerializeField] private UnityEvent OffEvent = null;

    private ButtonSounds buttonSounds;

    
    public Vector3 StandardButtonLightPosition;
    public Vector3 ToggleButtonLightPosition;
    public Vector3 HoldButtonLightPosition;
    public GameObject StandardButtonLight;


    void Start( )
	{
		Assert.IsNotNull( animator );
		Assert.IsNotNull( highlight );
        //Assert.IsNotNull(StandardButtonLight);

        buttonSounds = GameObject.Find("ButtonSoundManager").GetComponent<ButtonSounds>();
        //buttonSoundManager = GameObject.Find("ButtonSoundManager");
        //Debug.Log(buttonSoundManager);
        
        //StandardButtonLightPosition = GameObject.Find("Standard Button Test/Buttons Test Light").transform.position;
        //Debug.Log(StandardButtonLightPosition);
        //ToggleButtonLightPosition = GameObject.Find("Toggle Button Test/Buttons Test Light").transform.position;
        //HoldButtonLightPosition = GameObject.Find("Hold Button Test/Buttons Test Light").transform.position;
    }

    public void OnPress( )
	{
        switch ( buttonType )
		{
            
			case ButtonType.Standard:
                //StandardButtonLight = GameObject.Find("Standard Button Test/Buttons Test Light");
                //Debug.Log(StandardButtonLight);
                //buttonSounds.updateButtonSoundManagerPosition(StandardButtonLightPosition);

                animator.SetTrigger( "OnPress" );
			isOn = !isOn;
            if (isOn)
            {
                OnEvent.Invoke();
                buttonSounds.ButtonPressSound.Play();
                
                buttonSounds.LightStartingSound.Play();
                buttonSounds.LightLoopingSound.Play();
            }
            else
            {
                OffEvent.Invoke();
                buttonSounds.ButtonPressSound.Play();
                buttonSounds.LightLoopingSound.Pause();
            }
			break;

			case ButtonType.Toggle:


                //buttonSounds.updateButtonSoundManagerPosition(ToggleButtonLightPosition);

                isOn = !isOn;
			if ( isOn )
			{
				animator.SetTrigger( "OnPress" );
				OnEvent.Invoke( );
                buttonSounds.ButtonPressFirstHalf.Play();
                buttonSounds.LightStartingSound.Play();
                buttonSounds.LightLoopingSound.Play();
            }
			else
			{
				animator.SetTrigger( "OnRelease" );
				OffEvent.Invoke( );
                buttonSounds.ButtonPressSecondHalf.Play();
                buttonSounds.LightLoopingSound.Pause();
            }
			break;

			case ButtonType.Hold:


                //buttonSounds.updateButtonSoundManagerPosition(HoldButtonLightPosition);

                animator.SetTrigger( "OnPress" );
			isOn = true;
			OnEvent.Invoke( );
            buttonSounds.ButtonPressFirstHalf.Play();
            buttonSounds.LightStartingSound.Play();
            buttonSounds.LightLoopingSound.Play();
            break;

			default:
			break;
		}
	}

	public void OnRelease( )
	{
		switch ( buttonType )
		{
			case ButtonType.Standard:
			animator.SetTrigger( "OnRelease" );
			break;

			case ButtonType.Toggle:
			break;

			case ButtonType.Hold:
			animator.SetTrigger( "OnRelease" );
			isOn = false;
			OffEvent.Invoke( );
            buttonSounds.ButtonPressSecondHalf.Play();
            buttonSounds.LightLoopingSound.Pause();
            break;

			default:
			break;
		}
	}

	public void OnOverEnter( )
	{
		highlight.SetActive( true );
	}

	public void OnOverExit( )
	{
		highlight.SetActive( false );
	}

	public void PressDone()
	{

	}
}
