using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventSpeachMessage: UnityEvent<string, float> { }

public class SpeechService : MonoBehaviour
{
	[DllImport( "__Internal" )]
	private static extern int GetVoicesLength( );

	[DllImport( "__Internal" )]
	private static extern string GetVoiceName( int id );

	[DllImport( "__Internal" )]
	private static extern int CheckSpeechSynthesis( );

	[DllImport( "__Internal" )]
	private static extern void Speak( string message, float vol, int voice, float pitch, float rate );

	[DllImport( "__Internal" )]
	private static extern void ShowMessage( string str );

	public static SpeechService Instance { get; private set; }

	/// <summary>
	/// Voice used by the Speech API. Please use GetAvailableVoices() to get list of all the available voices.
	/// </summary>
	public int SelectedVoice { get; set; }
	/// <summary>
	/// Controls pitch of the voice. Please use a float. Default 1.0f.
	/// </summary>
	public float Pitch { get; set; }
	/// <summary>
	/// Controls volume of the voice. Default 1.0f.
	/// </summary>
	public float Volume { get; set; }
	/// <summary>
	/// Controls speech rate. Default 1.0f.
	/// </summary>
	public float Rate { get; set; }

	[SerializeField] private UnityEventSpeachMessage onMessageRecived = null;

	private void Awake( )
	{
		if ( Instance != null && Instance != this )
			Destroy( gameObject );
		else
			Instance = this;

		SelectedVoice = 0;
		Pitch = 1.0f;
		Volume = 1.0f;
		Rate = 1.0f;
	}

	private void Start( )
	{
		Invoke( "WarmUp", 0.5f );
		Invoke( "Init", 0.8f );
	}

	private void OnDestroy( ) { if ( this == Instance ) { Instance = null; } }

	/// <summary>
	/// Uses Speech API to say a message.
	/// </summary>
	/// <param name="message">Message to be said.</param>
	public void SpeakMessage( string message )
	{
		SpeakMessage( message, Volume, SelectedVoice, Pitch, Rate );
	}

	/// <summary>
	/// Uses Speech API to say a message.
	/// </summary>
	/// <param name="message">Message to be said.</param>
	/// <param name="vol">Volume for this message.</param>
	/// <param name="pitch">Pitch for this message.</param>
	/// <param name="rate">Speech rate for this message.</param>
	public void SpeakMessage( string message, float vol, float pitch, float rate )
	{
		SpeakMessage( message, vol, SelectedVoice, pitch, rate );
	}

	/// <summary>
	/// Checks is Speech API is available. Depends on the browser/OS.
	/// Can take few seconds after website/browser (not Unity game) is loaded.
	/// </summary>
	/// <returns>True if available.</returns>
	public bool SpeechServiceAvailable( )
	{
		#if ( UNITY_WEBGL && !UNITY_EDITOR)

		int re = CheckSpeechSynthesis( );

		if ( re == 1 )
			return true;

		#endif

		return false;
	}

	/// <summary>
	/// Returns a list of voices offered by the browser.
	/// </summary>
	/// <returns>List of voices offered by the browser.</returns>
	public List<string> GetAvailableVoices()
	{
		if ( !SpeechServiceAvailable( ) )
			return new List<string>( new string[] { "None. Service unavailable, please buy a DLC ;)" } );

		int voiceCount = GetVoicesLength( );
		List<string> voices = new List<string>( );

		for ( int i = 0; i < voiceCount; i++ )
			voices.Add( GetVoiceName( i ) );

		return voices;
	}

	/// <summary>
	/// Tries to find English voice.
	/// </summary>
	/// <returns>Returns first English voice it finds. Defaults to 0 if no English voice if found or it;s the first one on the list.</returns>
	public int TryFindEnglishVoice()
	{
		List<string> voices = GetAvailableVoices( );

		for ( int i = 0; i < voices.Count; i++ )
		{
			if ( voices[i].Contains( "English" ) )
				return i;
		}

		return 0;
	}

	private void WarmUp( )
	{
		SpeakMessage( "...", 0.1f, 0, 1, 1, true );
	}

	private void Init( )
	{
		SelectedVoice = TryFindEnglishVoice( );
	}

	private void SpeakMessage( string message, float vol = 1.0f, int voice = 0, float pitch = 1.0f, float rate = 1.0f, bool tryEnglish = false )
	{
		onMessageRecived.Invoke( message, rate );

		if ( !SpeechServiceAvailable( ) )
			return;

		if ( tryEnglish )
			voice = TryFindEnglishVoice( );

		Speak( message, vol, voice, pitch, rate );
	}
}
