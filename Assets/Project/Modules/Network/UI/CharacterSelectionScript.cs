using UnityEngine;

namespace Project.Modules.Network
{
    public class CharacterSelectionScript : MonoBehaviour
	{
		public static void FirePlayerSelected(string name) => Stepsss.SetCharacterRequest(name);

		public string[] Names;

		[SerializeField] private GameObject[] _chars;

		public void Select(int index)
		{
			for (int i = 0; i < _chars.Length; i++)
			{
				_chars[i].SetActive(i == index);
			}

			Stepsss.SetCharacterRequest(Names[index]);
			Debug.Log("Chosen character: " + Names[index]);
		}
	}
}