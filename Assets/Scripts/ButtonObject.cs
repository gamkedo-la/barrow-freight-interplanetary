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
    private AudioSource[] buttonSFX;
    private AudioSource ButtonPressSound;
    private AudioSource LightStartingSound;
    private AudioSource LightLoopingSound;


    void Start( )
	{
		Assert.IsNotNull( animator );
		Assert.IsNotNull( highlight );
        buttonSFX = GetComponents<AudioSource>();
         ButtonPressSound = buttonSFX[0];
         LightStartingSound = buttonSFX[1];
        LightLoopingSound = buttonSFX[2];
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
                    ButtonPressSound.Play();
                    LightStartingSound.Play();
                    LightLoopingSound.Play();
                }
                else
                {
                    OffEvent.Invoke();
                    ButtonPressSound.Play();
                    LightLoopingSound.Pause();
                }
			break;

			case ButtonType.Toggle:
			isOn = !isOn;
			if ( isOn )
			{
				animator.SetTrigger( "OnPress" );
				OnEvent.Invoke( );
			}
			else
			{
				animator.SetTrigger( "OnRelease" );
				OffEvent.Invoke( );
			}
			break;

			case ButtonType.Hold:
			animator.SetTrigger( "OnPress" );
			isOn = true;
			OnEvent.Invoke( );
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
