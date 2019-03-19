using UnityEngine;

public class SpeakMessage : MonoBehaviour
{
	[SerializeField] private string onMessage = "On";
	[SerializeField] private string offMessage = "Off";
	[SerializeField, Range(0,1)] private float volume = 1.0f;
	[SerializeField, Range(0.5f,1.5f)] private float pitch = 1.0f;
	[SerializeField, Range(0.1f,2f)] private float rate = 1.0f;
	[SerializeField] private bool useEnglishIfAvailable = true;
	[Tooltip("You can force set language here. 0 is usually system language. Only works in English above is set to false.")]
	[SerializeField] private int languageID = 0;

	public void SayOnMessage ()
	{
		SpeechService.Instance.SpeakMessage( onMessage, volume, 0, pitch, rate, useEnglishIfAvailable );
	}

	public void SayOffMessage( )
	{
		SpeechService.Instance.SpeakMessage( offMessage, volume, 0, pitch, rate, useEnglishIfAvailable );
	}
}
