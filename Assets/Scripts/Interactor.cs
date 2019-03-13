using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class Interactor : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI helpLabel = null;
	[SerializeField] private Crosshair crosshair = null;
	[SerializeField] private float interactionRange = 3.0f;

	private const int layerMask = 1 << 9; // Intractable

	private GameObject lookedAtObject;

	void Start( )
	{
		Assert.IsNotNull( helpLabel );
		Assert.IsNotNull( crosshair );
	}

	void Update( )
	{
		lookedAtObject = LookForInteractableObject( );

		if ( lookedAtObject )
		{
			helpLabel.text = "Looking at: " + lookedAtObject.name;
			crosshair.CanIntaract( );
		}
		else
		{
			helpLabel.text = "Nothing to interact with...";
			crosshair.Normal( );
		}
	}

	private GameObject LookForInteractableObject( )
	{
		Ray mouseRay = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hitInfo;

		GameObject foundObject = null;

		if ( Physics.Raycast( mouseRay, out hitInfo, interactionRange, layerMask ) )
			foundObject = hitInfo.collider.gameObject;

		return foundObject;
	}
}
