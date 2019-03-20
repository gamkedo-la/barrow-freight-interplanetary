using UnityEngine;

public class SpeakMessage : MonoBehaviour
{
	[SerializeField] private string onMessage = "On";
	[SerializeField] private string offMessage = "Off";
	[SerializeField, Range(0,1)] private float volume = 1.0f;
	[SerializeField, Range(0.5f,1.5f)] private float pitch = 1.0f;
	[SerializeField, Range(0.1f,2f)] private float rate = 1.0f;

	public void SayOnMessage ()
	{
		SpeechService.Instance.SpeakMessage( onMessage, volume, pitch, rate );
	}

	public void SayOffMessage( )
	{
		SpeechService.Instance.SpeakMessage( offMessage, volume, pitch, rate );
	}
}
