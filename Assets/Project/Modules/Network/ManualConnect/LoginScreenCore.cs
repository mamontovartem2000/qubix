using UnityEngine;

namespace Project.Modules.Network
{

	public class LoginScreenCore : MonoBehaviour
	{
		[SerializeField] private GameObject _joinPanel;
		[SerializeField] private GameObject _hostPanel;
		[SerializeField] private GameObject _buttons;
		[SerializeField] private GameObject _timer;

		public static bool IsHost = false;

		public void Host()
		{
			_buttons.SetActive(false);
			_hostPanel.SetActive(true);
			IsHost = true;
		}

		public void Join()
		{
			_buttons.SetActive(false);
			_joinPanel.SetActive(true);
		}

		public void ShowTimer()
		{
			_timer.SetActive(true);
		}
	}
}