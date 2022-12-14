using Project.Modules.Network.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Modules.Network.StartConnect
{
    public class ConnectTemplate : MonoBehaviour
	{
		[SerializeField] private CharacterSelection _select;
		private bool _needLoadGameScene, _needReloadThisScene;
		private GameObject _objectToHide;

		protected void InitTemplate(GameObject objectToHide, BuildTypes buildType)
		{
			_objectToHide = objectToHide;
			NetworkData.BuildType = buildType;
			NetworkEvents.LoadMainMenuScene += ReloadMenuScene;
			NetworkEvents.LoadGameScene += LoadGameScene;
			WaitingRoomTimer.ShowCharacterSelectionWindow += SwapScreens;
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
			if (_objectToHide != null)
            {
				_objectToHide.SetActive(false);
            }

			if (_select != null)
			{
				_select.gameObject.SetActive(true);
			}
		}

		protected virtual void OnDestroy()
		{
			NetworkEvents.LoadMainMenuScene -= ReloadMenuScene;
			NetworkEvents.LoadGameScene -= LoadGameScene;
			WaitingRoomTimer.ShowCharacterSelectionWindow -= SwapScreens;
		}
	}
}
