using System;
using UnityEngine;

namespace Project.Modules.Network
{
	public class CharacterSelectionScript : MonoBehaviour
	{
		public static event Action<string> OnPlayerSelected;
		public static void FirePlayerSelected(string s) => OnPlayerSelected?.Invoke(s);

		public string[] Names;

		[SerializeField] private GameObject[] _chars;

		public void Select(int index)
		{
			for (int i = 0; i < _chars.Length; i++)
			{
				_chars[i].SetActive(i == index);
			}
			
			FirePlayerSelected(Names[index]);
			Debug.Log(Names[index]);
		}
	}
}