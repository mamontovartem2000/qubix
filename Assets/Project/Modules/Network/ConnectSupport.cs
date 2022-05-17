using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Modules.Network
{
    public class ConnectSupport : MonoBehaviour
	{
		[SerializeField] private GameObject _selectionScreen;
		[SerializeField] private CharacterSelection _select;

		private bool _needLoadGameScene, _needReloadThisScene;

		private void Start()
		{
			Stepsss.LoadMainMenuScene += ReloadMenuScene;
			Stepsss.LoadGameScene += LoadGameScene;
			Stepsss.ShowCharacterSelectionWindow += SwapScreens;
#if UNITY_WEBGL && !UNITY_EDITOR
			BrowserEvents.ReadyToStart();
#endif
			//string sdf = "{\"payload\":\"eyJzZXJ2ZXJfdXJsIjoid3NzOi8vZ2FtZS5xdWJpeGluZmluaXR5LmlvL21hdGNoIiwicm9vbV9pZCI6NDI4LCJwbGF5ZXJfaWQiOiJmZTc4YTMyMC0xNmNjLTQ2MWItYmMwMS0zMmI4YjZkZWY3ZTkiLCJwbGF5ZXJfaWNvbiI6Imh0dHBzOi8vZDNyc2Y3NTYxd2oyNzQuY2xvdWRmcm9udC5uZXQvbW9ja19hdmF0YXJzL3F1Yml4XzExLnBuZyIsInBsYXllcl9uaWNrbmFtZSI6IkRpbWEiLCJtYXBfaWQiOjMsImdhbWVfbW9kZSI6InNraXJtaXNoIiwibXVsdGlwbGF5ZXJfc2NoZW1hIjpbMV0sImF2YWlsYWJsZV9jaGFyYWN0ZXJzIjpbIkxvbWl4IiwiTG9taXgiLCJMb21peCIsIkxvbWl4IiwiTG9taXgiLCJMb21peCIsIlNpbGVuIl0sImV4cGlyZXNfYXQiOjE2NTI3MTIxNzcyNzh9\",\"signatures\":[{\"protected\":\"eyJhbGciOiJSUzI1NiIsImtpZCI6Im5hbWV8bWF0Y2htYWtpbmcifQ\",\"signature\":\"uZbHJOJHTQXCi2tSp240mLJQe69v0O0UAQwG0uBbsjpjf4RRFHjPMiBxzrobbMoJtmg7WkTjicR0hqfF9RlsObJuGx_IVaPQkmPJtpGKl4r98iBPRrBSifWNatjbJXP26UXYyub4eXZaxb38q9DM0AniV_cZSN1TWkYHOM2_-uY\"}]}";
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
			_selectionScreen.SetActive(true);
			//TODO: Add random selection
			var rnd = Random.Range(0, 3);
			_select.Select(rnd) ;
		}

		private void OnDestroy()
		{
			Stepsss.LoadMainMenuScene -= ReloadMenuScene;
			Stepsss.LoadGameScene -= LoadGameScene;
			Stepsss.ShowCharacterSelectionWindow -= SwapScreens;
		}
	}
}
