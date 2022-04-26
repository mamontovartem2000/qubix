using UnityEngine;

public class CharacterSelectionScript : MonoBehaviour
{
	[HideInInspector] public int Index;
	
	[SerializeField] private GameObject[] _chars;

	public void Select(int index)
	{
		for (int i = 0; i < _chars.Length; i++)
		{
			_chars[i].SetActive(i == index);
		}

		Index = index;
	}
}
