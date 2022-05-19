﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Modules.Network
{
    public class ConnectSupport : MonoBehaviour
	{
		[SerializeField] private CharacterSelection _select;

		private bool _needLoadGameScene, _needReloadThisScene;

		private void Start()
		{
			Stepsss.LoadMainMenuScene += ReloadMenuScene;
			Stepsss.LoadGameScene += LoadGameScene;
			WaitingRoomTimer.ShowCharacterSelectionWindow += SwapScreens;
#if UNITY_WEBGL && !UNITY_EDITOR
			BrowserEvents.ReadyToStart();
#endif
			//string sdf = "";
			//SetJoinRequest(sdf);
		}

		private void Update()
		{
			if (_needLoadGameScene)
				SceneManager.LoadScene(1, LoadSceneMode.Single);

			if (_needReloadThisScene)
				SceneManager.LoadScene(0, LoadSceneMode.Single);

#if !UNITY_WEBGL || UNITY_EDITOR
			if (NetworkData.Connect != null)
				NetworkData.Connect.DispatchWebSocketMessageQueue();
#endif
		}

		// Browser method
		public void SetJoinRequest(string request)
		{
			Debug.Log(request);
			Stepsss.ProcessJoinRequest(request);
		}

		private void ReloadMenuScene()
		{
			_needReloadThisScene = true;
		}

		private void LoadGameScene()
		{
			_needLoadGameScene = true;
		}

		private void SwapScreens()
		{
			_select.gameObject.SetActive(true);
			var rnd = Random.Range(0, 3);
			_select.Select(rnd) ;
		}

		private void OnDestroy()
		{
			Stepsss.LoadMainMenuScene -= ReloadMenuScene;
			Stepsss.LoadGameScene -= LoadGameScene;
			WaitingRoomTimer.ShowCharacterSelectionWindow -= SwapScreens;
		}
	}
}
