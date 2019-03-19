using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions;

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

	private void Awake( )
	{
		if ( Instance != null && Instance != this )
			Destroy( gameObject );
		else
			Instance = this;
	}

	private void OnDestroy( ) { if ( this == Instance ) { Instance = null; } }

	public void SpeakMessage( string message, float vol = 1.0f, int voice = 0, float pitch = 1.0f, float rate = 1.0f, bool tryEnglish = false )
	{
		if ( !SpeechServiceAvailable( ) )
			return;

		if ( tryEnglish )
			voice = TryFindEnglish( );

		Speak( message, vol, voice, pitch, rate );
	}

	public bool SpeechServiceAvailable( )
	{
		#if ( UNITY_WEBGL && !UNITY_EDITOR)

		int re = CheckSpeechSynthesis( );

		if ( re == 1 )
			return true;

		#endif

		return false;
	}

	public List<string> GetAvailableVoices()
	{
		if ( !SpeechServiceAvailable( ) )
			return new List<string>( new string[] { "No languages available" } );

		int voiceCount = GetVoicesLength( );
		List<string> voices = new List<string>( );

		for ( int i = 0; i < voiceCount; i++ )
			voices.Add( GetVoiceName( i ) );

		return voices;
	}

	private int TryFindEnglish()
	{
		List<string> voices = GetAvailableVoices( );

		for ( int i = 0; i < voices.Count; i++ )
		{
			if ( voices[i].Contains( "English" ) )
				return i;
		}

		return 0;
	}
}
