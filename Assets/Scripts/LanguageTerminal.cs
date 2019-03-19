using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LanguageTerminal : MonoBehaviour
{
	[SerializeField] private TerminalMonitor monitor;

	void Start ()
	{
		Assert.IsNotNull( monitor );

		Invoke( "Warmup", 1f );
		Invoke( "InitTerminal", 3f );
	}

	private void Warmup( )
	{
		SpeechService.Instance.SpeakMessage( "On", 0.1f, 0, 1, 1, true );
	}

	private void InitTerminal()
	{
		List<string> voices = SpeechService.Instance.GetAvailableVoices( );

		string output = "";
		foreach ( var voice in voices )
		{
			output += voice + "\n";
		}

		monitor.WriteToMonitor( output + "\nEOF" );

		SpeechService.Instance.SpeakMessage( "Welcome to Barrow Freight Interplanetary", 1, 0, 1, 1, true );
	}
}
