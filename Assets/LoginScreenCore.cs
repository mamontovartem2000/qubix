using UnityEngine;

public class LoginScreenCore : MonoBehaviour
{
	[SerializeField] private GameObject _joinPanel;
	[SerializeField] private GameObject _hostPanel;
	[SerializeField] private GameObject _timer;
	[SerializeField] private GameObject _buttons;

	public void Host()
	{
		_buttons.SetActive(false);
		_hostPanel.SetActive(true);
	}

	public void Join()
	{
		_buttons.SetActive(false);
		_joinPanel.SetActive(true);
	}
}
