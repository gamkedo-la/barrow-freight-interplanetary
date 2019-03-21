using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class Subtitles : MonoBehaviour
{
	[SerializeField] private GameObject container = null;
	[SerializeField] private TextMeshProUGUI label = null;
	[SerializeField] private float displayTimePerLetter = 0.1f;

	private float nextSubtitlesDelay = 0;
	private int messagesCount = 0;

	void Start ()
	{
		Assert.IsNotNull( container );
		Assert.IsNotNull( label );

		label.text = "";
		container.SetActive( false );
	}

	private void Update( )
	{
		nextSubtitlesDelay -= Time.deltaTime;
		nextSubtitlesDelay = nextSubtitlesDelay < 0 ? 0 : nextSubtitlesDelay;
	}

	public void DisplaySubtitles( string message, float rate)
	{
		float displayTime = ( message.Length * displayTimePerLetter ) / rate;

		StartCoroutine( Display( message, displayTime, nextSubtitlesDelay ) );
		nextSubtitlesDelay += displayTime;
	}

	private IEnumerator Display( string message, float displayTime, float delay )
	{
		// Delay before displaying
		messagesCount++;
		yield return new WaitForSeconds( delay );

		// Show
		container.SetActive( true );
		label.text = message;

		//  Wait
		yield return new WaitForSeconds( displayTime );
		messagesCount--;

		// Hide
		if ( messagesCount == 0 )
			container.SetActive( false );
	}
}
