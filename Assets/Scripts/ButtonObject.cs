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



    void Start( )
	{
		Assert.IsNotNull( animator );
		Assert.IsNotNull( highlight );

        buttonSounds = GameObject.Find("ButtonSoundManager").GetComponent<ButtonSounds>();

    }

    public void OnPress( )
	{

        

        switch ( buttonType )
		{
			case ButtonType.Standard:
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
