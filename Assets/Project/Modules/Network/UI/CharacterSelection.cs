using TMPro;
using UnityEngine;

namespace Project.Modules.Network.UI
{
    public class CharacterSelection : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private GameObject[] _checkmarks;

        private int _selectedIndex = -1;
        private string[] _names = new[] { "Buller", "GoldHunter", "Powerf" };
        private string[] _descriptions = new[]
        {
            "Great shooter was born with a rifle in his hands. Causes a lot of inconvenience to enemies, hitting them from a distance.",
            "Loves gold and only gold! He is proficient with ranged and melee weapons, allowing him to effectively attack the enemy from various distances.",
            "A great warrior from distant islands, armed with government technology. The ancient way of life hardened the fighter, thanks to this he is insanely hardy."
        };

        public void Select(int index)
        {
            if (_selectedIndex == index) return;

            _selectedIndex = index;
            _nameText.SetText(_names[index]);
            _descriptionText.SetText(_descriptions[index]);

            Stepsss.SetCharacterRequest(_names[index]);

            Debug.Log("Chosen character: " + _names[index]);

            for (var i = 0; i < _checkmarks.Length; i++)
            {
                _checkmarks[i].SetActive(index == i);
            }
        }
    }
}
