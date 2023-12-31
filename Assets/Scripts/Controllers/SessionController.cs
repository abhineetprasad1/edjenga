﻿
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SessionController : IController
{
	private List<IController> controllers;
	private GameData gameData;

	public SessionController(GameData gameData)
	{
		this.gameData = gameData;
		Initialize();
	}
	
	private void Initialize()
	{
	
		controllers = new List<IController>();
		controllers.Add(new SessionStateController(gameData));
		controllers.Add(new LevelController(gameData));
		controllers.Add(new StackCameraController(gameData));
		controllers.Add(new InputController(gameData));
		controllers.Add(new HighlightController(gameData));
		controllers.Add(new StackTestController(gameData));

        ServiceLocator.GetService<MonoService>().AddUpdateListener(Execute);
		
	}
	
	public void Execute()
	{
		foreach (var controller in controllers)
		{
			controller.Execute();
		}
	}



    public void Destroy()
	{
		ServiceLocator.GetService<MonoService>().RemoveUpdateListener(Execute);

		var viewControllers = Main.Instance.gameObject.GetComponentsInChildren<ViewController>();

		foreach (var view in viewControllers)
		{
			view.Reset();
		}
		
		foreach (var controller in controllers)
		{
			controller.Destroy();
		}

		
	}
}
