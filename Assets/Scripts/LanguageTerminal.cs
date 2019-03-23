using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LanguageTerminal : MonoBehaviour
{
	[SerializeField] private TerminalMonitor monitor = null;

	void Start ()
	{
		Assert.IsNotNull( monitor );

		Invoke( "InitTerminal", 1.5f );
	}

	/// <summary>
	/// Changes voice of the speech API for the next one. Cycles back if the last one was selected.
	/// </summary>
	public void ChangeVoice()
	{
		List<string> voices = SpeechService.Instance.GetAvailableVoices( );
		int langID = SpeechService.Instance.SelectedVoice;
		langID++;
		langID = langID >= voices.Count ? 0 : langID;
		SpeechService.Instance.SelectedVoice = langID;

		OutputSpeachData( voices, langID );

		SpeechService.Instance.SpeakMessage( "New voice selected" );
	}

	private void InitTerminal()
	{
		List<string> voices = SpeechService.Instance.GetAvailableVoices( );
		int langID = SpeechService.Instance.TryFindEnglishVoice( );

		OutputSpeachData( voices, langID );

		SpeechService.Instance.SpeakMessage( "Welcome pilot. Where would you like us to go first?" );
	}

	private void OutputSpeachData( List<string> voices, int langID )
	{
		string output = "Available voices:\n";
		for ( int i = 0; i < voices.Count; i++ )
		{
			if ( i == langID )
				output += "<b>";
			output += voices[i] + "\n";
			if ( i == langID )
				output += "</b>";
		}

		output += "\n";
		output += $"Volume: {SpeechService.Instance.Volume}\n";
		output += $"Pitch: {SpeechService.Instance.Pitch}\n";
		output += $"Rate: {SpeechService.Instance.Rate}\n";

		output += $"\n<b><i>Press button below to change voice</i></b>";

		monitor.WriteToMonitor( output );
	}
}
