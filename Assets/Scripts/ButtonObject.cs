using UnityEngine;
using UnityEngine.Assertions;

public class ButtonObject : MonoBehaviour
{
	void Start ()
	{
		//Assert.IsNotNull(  );
	}

	void Update ()
	{

	}

	private void OnMouseOver( )
	{
		Debug.Log( "OnMouseOver" );
	}

	private void OnMouseDown( )
	{
		Debug.Log( "OnMouseDown" );
	}

	private void OnMouseUp( )
	{
		Debug.Log( "OnMouseUp" );
	}
}
