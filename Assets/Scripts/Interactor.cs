﻿using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class Interactor : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI helpLabel = null;
	[SerializeField] private Crosshair crosshair = null;
	[SerializeField] private float interactionRange = 3.0f;

	private const int layerMask = 1 << 9; // Intractable layer

	private GameObject lastLookedObject;
	private bool pressing = false;

	void Start( )
	{
		Assert.IsNotNull( helpLabel );
		Assert.IsNotNull( crosshair );

		helpLabel.text = "Nothing to interact with...";
	}

	void Update( )
	{
		GameObject currentlyLookintAt = LookForInteractableObject( );

		if ( currentlyLookintAt && Input.GetMouseButtonDown(0) ) // We have an IInteractable and we pressed LMB
		{
			currentlyLookintAt.GetComponent<IInteractable>( )?.OnPress( );
			pressing = true;
		}

		if ( currentlyLookintAt && Input.GetMouseButtonUp( 0 ) )  // We have an IInteractable and we released LMB
		{
			currentlyLookintAt.GetComponent<IInteractable>( )?.OnRelease( );
			pressing = false;
		}

		if ( currentlyLookintAt != lastLookedObject && lastLookedObject && pressing) // We moved away so we treat it as release of LMB
		{
			lastLookedObject.GetComponent<IInteractable>( )?.OnRelease( );
			pressing = false;
		}

		if ( currentlyLookintAt == lastLookedObject ) // Do nothing, we are still looking at the same thing
			return;

		if ( currentlyLookintAt )
		{
			helpLabel.text = "Looking at: " + currentlyLookintAt.name;
			crosshair.CanIntaract( );
		}
		else
		{
			helpLabel.text = "Nothing to interact with...";
			crosshair.Normal( );
		}

		if ( currentlyLookintAt )
			currentlyLookintAt.GetComponent<IInteractable>( )?.OnOverEnter( );

		if ( lastLookedObject )
			lastLookedObject.GetComponent<IInteractable>( )?.OnOverExit( );

		lastLookedObject = currentlyLookintAt;
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
