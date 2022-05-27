using System.Collections;
using System.Collections.Generic;
using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Player;
using Project.Modules.Network;
using UnityEngine;
using UnityEngine.UI;

public class SkillImageChangeScript : MonoBehaviour
{
    public GlobalEvent SkillImageChange;
    public Image[] FirtSkillImage;
    public Image[] SecondSkillImage;
    public Image[] ThirdSkillImage;
    public Image[] FourthSkillImage;
   
    private void Start()
    {
        SkillImageChange.Subscribe(SkillImageChanged);
    }

    private void SkillImageChanged(in Entity entity)
    {
        if (entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;
        ref readonly var player = ref entity.Read<PlayerAvatar>().Value;
        ref readonly var skills = ref player.Read<SkillEntities>();
        
        FirtSkillImage[skills.FirstSkill.Read<SkillImage>().Value].gameObject.SetActive(true);
        SecondSkillImage[skills.SecondSkill.Read<SkillImage>().Value].gameObject.SetActive(true);
        ThirdSkillImage[skills.ThirdSkill.Read<SkillImage>().Value].gameObject.SetActive(true);
        FourthSkillImage[skills.FourthSkill.Read<SkillImage>().Value].gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        SkillImageChange.Unsubscribe(SkillImageChanged);
    }
}
