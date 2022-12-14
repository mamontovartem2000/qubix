using UnityEngine;

namespace Project.Modules.Network.StartConnect
{
    public class HubFrontConnect : ConnectTemplate
	{
		[SerializeField] private GameObject _loading;

		private void Start()
		{
			base.InitTemplate(_loading, BuildTypes.Front_Hub);

#if UNITY_WEBGL && !UNITY_EDITOR
			BrowserEvents.ReadyToStart();
#endif
		}

		// Browser method
		public void SetJoinRequest(string request)
		{
			Debug.Log("Get request: " + request);
			ConnectionSteps.ConnectWithCreateSocket(request);
		}
	}
}
