using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
	[SerializeField] private Image crossHair = null;
	[SerializeField] private Color colorNormal = Color.white;
	[SerializeField] private Color colorCanIntaract = Color.green;
	[SerializeField] private Color colorCantDo = Color.red;

	void Start ()
	{
		Assert.IsNotNull( crossHair );
	}

	public void Normal( )
	{
		crossHair.color = colorNormal;
	}

	public void CanIntaract( )
	{
		crossHair.color = colorCanIntaract;
	}

	public void CantDo( )
	{
		crossHair.color = colorCantDo;
	}
}
