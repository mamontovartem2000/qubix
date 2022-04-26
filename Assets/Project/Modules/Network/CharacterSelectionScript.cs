using System;
using UnityEngine;

namespace Project.Modules.Network
{
	public class CharacterSelectionScript : MonoBehaviour
	{
		public static event Action<string> OnPlayerSelected;

		public string[] Names;

		[SerializeField] private GameObject[] _chars;

		public void Select(int index)
		{
			for (int i = 0; i < _chars.Length; i++)
			{
				_chars[i].SetActive(i == index);
			}

			OnPlayerSelected?.Invoke(Names[index]);
		}
	}
}
