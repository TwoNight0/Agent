using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//각종 데이터와 캐릭터 스킬을 보유한 스크립트
//여기값은 데이터들의 원형이기때문에 가져가기 만하고 이 안의 값을 (변경하면 안됨)
public class CharacterData
{
    public CharacterData(PlayerMng.PlayAbleCharacter _value)
    {
        switch (_value)
        {
            case PlayerMng.PlayAbleCharacter.Paladin:
                Pubdmg = 10.0f;
                PubHp_max = 400.0f;
                Pubm_moveSpeed = 4.0f;
                PubDefense_physical = 20.0f;
                PubDefense_magical = 20.0f;
                skill_hill_cool = 5.0f;
                skill_nomal_cool = 6.0f;
                skill_Ultimate_cool = 25.0f;

                //이미지 등록 
                nomalSkillImg = Resources.Load<Sprite>("SkillIcon/Icon.6_26");
                UltimateSkillImg = Resources.Load<Sprite>("SkillIcon/Icon.4_79");
                MainWeaponImg = Resources.Load<Sprite>("SkillIcon/Icon.6_98");
                SubWeaponImg = Resources.Load<Sprite>("SkillIcon/Icon.8_21");
                break;
            case PlayerMng.PlayAbleCharacter.Archer:
                Pubdmg = 20.0f;
                PubHp_max = 200.0f;
                Pubm_moveSpeed = 5.0f;
                PubDefense_physical = 10.0f;
                PubDefense_magical = 10.0f;
                skill_hill_cool = 8.0f;
                skill_nomal_cool = 8.0f;
                skill_Ultimate_cool = 22.0f;

                //이미지 등록 (나중에 아쳐용 이미지로 바꾸자)
                nomalSkillImg = Resources.Load<Sprite>("SkillIcon/Icon.6_26");
                UltimateSkillImg = Resources.Load<Sprite>("SkillIcon/Icon.4_79");
                MainWeaponImg = Resources.Load<Sprite>("SkillIcon/Icon.4_79");
                SubWeaponImg = Resources.Load<Sprite>("SkillIcon/Icon.4_79");
                break;
        }
    }

    private float dmg;
    private float magic = 0;
    private float hp_max;
    private float m_moveSpeed;
    private float defense_physical;
    private float defense_magical;

    private float skill_hill_cool = 10.0f;
    private float skill_nomal_cool;
    private float skill_Ultimate_cool;

    public Sprite hillImg = Resources.Load<Sprite>("SkillIcon/Icon.6_86"); //기본값 
    public Sprite nomalSkillImg;
    public Sprite UltimateSkillImg;
    public Sprite MainWeaponImg;
    public Sprite SubWeaponImg;

    public float Pubdmg
    {
        get => dmg;
        set => dmg = value;
    }

    public float Pubmagic
    {
        get => magic;
        set => magic = value;
    }

    public float PubHp_max
    {
        get => hp_max;
        set => hp_max = value;
    }

    public float Pubm_moveSpeed
    {
        get => m_moveSpeed;
        set => m_moveSpeed = value;
    }

    public float PubDefense_physical
    {
        get => defense_physical;
        set => defense_physical = value;
    }

    public float PubDefense_magical
    {
        get => defense_magical;
        set => defense_magical = value;
    }

    public float Pubskill_hill_cool
    {
        get => skill_hill_cool;
        set => skill_hill_cool = value;
    }

    public float Pubskill_nomal_cool
    {
        get => skill_nomal_cool;
        set => skill_nomal_cool = value;
    }

    public float Pubskill_Ultimate_cool
    {
        get => skill_Ultimate_cool;
        set => skill_Ultimate_cool = value;
    }
}
