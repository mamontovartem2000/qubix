using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Modules.Network
{
    public class ConnectSupport : MonoBehaviour
	{
		[DllImport("__Internal")]
		private static extern void ReadyToStart();

		[DllImport("__Internal")]
		private static extern void GameCancelled();

		[SerializeField] private GameObject _selectionScreen;

		private bool _needLoadGameScene, _needReloadThisScene;

		private void Start()
		{
			Stepsss.LoadMainMenuScene += ReloadMenuScene;
			Stepsss.LoadGameScene += LoadGameScene;
			Stepsss.ShowCharacterSelectionWindow += SwapScreens;
			ReadyToStart();
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
			_selectionScreen.SetActive(true);
			//TODO: Add random selection
			CharacterSelectionScript.FirePlayerSelected("GoldHunter");
		}
	}
}
