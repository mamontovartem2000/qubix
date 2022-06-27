using System.Collections;
using System.Collections.Generic;
using ME.ECS;
using Project.Common.Components;
using Project.Features.Player;
using Project.Modules.Network;
using UnityEngine;
using UnityEngine.UI;

public class SkillImageChangeScript : MonoBehaviour
{
    public GlobalEvent SkillImageChange;
    public GameObject FirtSkillImage;
    public GameObject SecondSkillImage;
    public GameObject ThirdSkillImage;
    public GameObject FourthSkillImage;
    
   
    private void Start()
    {
        SkillImageChange.Subscribe(SkillImageChanged);
    }

    private void SkillImageChanged(in Entity entity)
    {
        if (entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;
        ref readonly var skills = ref entity.Read<SkillEntities>();
        
        GetImage(FirtSkillImage, skills.FirstSkill.Read<SkillImage>().Value).SetActive(true);
        GetImage(SecondSkillImage, skills.SecondSkill.Read<SkillImage>().Value).SetActive(true);
        GetImage(ThirdSkillImage, skills.ThirdSkill.Read<SkillImage>().Value).SetActive(true);
        GetImage(FourthSkillImage, skills.FourthSkill.Read<SkillImage>().Value).SetActive(true);
    }

    private GameObject GetImage(GameObject parent, int number)
    {
        var totalChildren = parent.transform.childCount;

        var itemsArray = new GameObject[totalChildren];

        for(var i = 0; i < totalChildren; i++)
        {
            itemsArray[i] = parent.transform.GetChild(i).gameObject;
        }

        return itemsArray[number];
    }

    private void OnDestroy()
    {
        SkillImageChange.Unsubscribe(SkillImageChanged);
    }
}
