using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Modules.Network.UI
{
    public class CharacterSelection : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private GameObject[] _checkmarks;
        [SerializeField] private Transform[] _skillDescriptions;

        private int _selectedIndex = -1;
        private string[] _names = { "Buller", "GoldHunter", "Powerf" };
        private string[] _descriptions = {
            "Great shooter was born with a rifle in his hands. Causes a lot of inconvenience to enemies, hitting them from a distance.",
            "Loves gold and only gold! He is proficient with ranged and melee weapons, allowing him to effectively attack the enemy from various distances.",
            "A great warrior from distant islands, armed with government technology. The ancient way of life hardened the fighter, thanks to this he is insanely hardy."
        };

        public Sprite[] _bullerIcons, _goldIcons, _powerIcons;
        public string[] _bullerDes, _goldDes, _powerDes;

        private void OnEnable()
        {
            var rnd = Random.Range(0, _names.Length);
            SelectCharacter(rnd);
        }

        public void SelectCharacter(int index)
        {
            if (_selectedIndex == index) return;

            _selectedIndex = index;
            _nameText.SetText(_names[index]);
            _descriptionText.SetText(_descriptions[index]);

            ConnectionSteps.SetCharacterRequest(_names[index]);

            Debug.Log("Chosen character: " + _names[index]);

            for (var i = 0; i < _checkmarks.Length; i++)
            {
                _checkmarks[i].SetActive(index == i);
            }
            
            ChangeSkillDescription(index);
        }

        private void ChangeSkillDescription(int index)
        {
            if(index < 0 || index > _skillDescriptions.Length - 1) return;
            
            var tmpArr = new Transform[_skillDescriptions.Length];
            
            for (int i = 0; i < _skillDescriptions.Length; i++)
            {
                tmpArr[i] = _skillDescriptions[i];
            }

            for (int i = 0; i < tmpArr.Length; i++)
            {
                var icon = tmpArr[i].GetChild(0).GetComponent<Image>();
                var descr = tmpArr[i].GetChild(1).GetComponent<TextMeshProUGUI>();

                switch (index)
                {
                    case 0:
                    {
                        icon.sprite = _bullerIcons[i];
                        descr.SetText(_bullerDes[i]);
                        break;
                    }
                    case 1:
                    {
                        icon.sprite = _goldIcons[i];
                        descr.SetText(_goldDes[i]);
                        break;
                    }
                    case 2:
                    {
                        icon.sprite = _powerIcons[i];
                        descr.SetText(_powerDes[i]);
                        break;
                    }
                }
            }
        }
    }
}
