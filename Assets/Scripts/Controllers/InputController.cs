using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;


public class InputController : IController
{
	private GameData gameData;
	CinemachineFreeLook freeLookCamera;
	int blockLayerMask;

	Camera mainCamera;

	public InputController (GameData gameData)
	{
		this.gameData = gameData;
		blockLayerMask = LayerMask.GetMask("Block");

		freeLookCamera = (CinemachineFreeLook)Main.Instance.orbitalCamera;
		GameEvents.OnArrowClick += OnArrowClick;
		mainCamera = Camera.main;
	}
	

	public void Destroy()
	{
        GameEvents.OnArrowClick -= OnArrowClick;
    }

	void OnArrowClick(Dictionary<string, object> data)
	{
		bool next = (bool)data["next"];
		if (next)
		{
			gameData.goToNextStack = true;
		}
		else
		{
            gameData.goToPrevStack = true;
        }
	}


	public void Execute()
	{
		if (gameData.state == SessionState.READY)
		{
			bool isMousePressed = false;
			gameData.currentlyHoveredBlock = null;

            if (Input.GetMouseButton(0))
			{
				isMousePressed = true;
				gameData.highlightedObjects.Clear();

				//GameEvents.OnJblockClick(null);
			}

			if (isMousePressed)
			{
				// Get mouse movement on the X-axis
				float mouseX = Input.GetAxis("Mouse X");
				//Debug.Log("mouse X " + mouseX);
				// Update the orbit input axis based on mouse movement
				freeLookCamera.m_XAxis.m_InputAxisValue = mouseX * gameData.sensitivity;

			}
			else
			{
				// Reset the orbit input axis when the mouse is not pressed
				freeLookCamera.m_XAxis.m_InputAxisValue = 0f;
				

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, blockLayerMask))
                {
                    // The ray hit an object on block layer
                    //  Debug.Log("Hit object: " + hit.collider.gameObject.name);
                    var data = new Dictionary<string, object>();
                    var jvc = hit.collider.gameObject.GetComponentInParent<JBlockViewController>();
                    if (jvc != null)
                    {
						gameData.currentlyHoveredBlock = jvc;
                    }

                }


            }

			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				gameData.goToNextStack = true;
			}

			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				gameData.goToPrevStack = true;
			}


			if (Input.GetMouseButtonDown(1))
			{
                //cast ray from camera to mouse pos
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, blockLayerMask))
                {
                    // The ray hit an object on block layer
                  //  Debug.Log("Hit object: " + hit.collider.gameObject.name);
					var data = new Dictionary<string, object>();
					var jvc = hit.collider.gameObject.GetComponentInParent<JBlockViewController>();
                    if (jvc != null)
					{
						gameData.highlightedObjects.Clear();
						data["jBlock"] = jvc.jBlock;
						data["pos"] = Input.mousePosition;
						GameEvents.OnJblockClick(data);

                        gameData.highlightedObjects.Add(jvc);
                    }

                }
            }
		}

	}


   

}
