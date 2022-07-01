using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Modules.Network.UI
{
    public class CharacterSelection : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private GameObject[] _skillDescriptions;
        public Character[] PlayerCharacter;

        [System.Serializable] public struct Character
        {
            public string name;
            public string characterDescription;
            public GameObject checkmark;
            public Sprite[] skillIcon;
            public string[] skillDescription;
        }
        
        private int _selectedIndex = -1;

        private void OnEnable()
        {
            var rnd = Random.Range(0, PlayerCharacter.Length);
            // rnd = 3; 
            SelectCharacter(rnd);
        }

        public void SelectCharacter(int index)
        {
            ConnectionSteps.SetCharacterRequest(PlayerCharacter[index].name);
            
            if (_selectedIndex == index) return;

            _selectedIndex = index;
            _nameText.SetText(PlayerCharacter[index].name);
            _descriptionText.SetText(PlayerCharacter[index].characterDescription);

            for (var i = 0; i < PlayerCharacter.Length; i++)
            {
                PlayerCharacter[i].checkmark.SetActive(index == i);
            }
            
            ChangeSkillDescription(index);
        }

        private void ChangeSkillDescription(int index)
        {
            for (var i = 0; i < _skillDescriptions.Length; i++)
            {
                var icon = _skillDescriptions[i].transform.GetChild(0).GetComponent<Image>();
                var descr = _skillDescriptions[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                
                icon.sprite = PlayerCharacter[index].skillIcon[i];
                descr.SetText(PlayerCharacter[index].skillDescription[i]);
            }
        }
    }
}
