using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 클래스 설명 : 캐릭터에 데이터의 원본(변경하지 말것),
// PlayerMng에 있는 변수들에 복사해서 사용
public class CharacterData{
    public CharacterData(PlayerMng.PlayAbleCharacter _value){
        switch (_value){
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
                SubWeaponImg = Resources.Load<Sprite>("SkillIcon/Icons8_21");
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

                //이미지 등록
                nomalSkillImg = Resources.Load<Sprite>("SkillIcon/Icon.1_16");
                UltimateSkillImg = Resources.Load<Sprite>("SkillIcon/Icon.4_62");
                MainWeaponImg = Resources.Load<Sprite>("SkillIcon/Icon.4_60");
                SubWeaponImg = Resources.Load<Sprite>("SkillIcon/Icons8_21");
                break;
        }
    }

    #region [PlayerData]
    private float dmg;
    private float magic = 0;
    private float hp_max;
    private float m_moveSpeed;
    private float dash;
    private float defense_physical;
    private float defense_magical;
    #endregion

    #region [Cooltime]
    private float skill_hill_cool = 10.0f;
    private float skill_nomal_cool;
    private float skill_Ultimate_cool;
    #endregion

    #region (Sprite)
    public Sprite hillImg = Resources.Load<Sprite>("SkillIcon/Icon.6_86"); //기본값 
    public Sprite nomalSkillImg;
    public Sprite UltimateSkillImg;
    public Sprite MainWeaponImg;
    public Sprite SubWeaponImg;
    public Sprite DashImg = Resources.Load<Sprite>("SkillIcon/Icon.3_94");
    #endregion

    #region (Get,Set) PlayerData
    public float Pubdmg{
        get => dmg;
        set => dmg = value;
    }

    public float Pubmagic{
        get => magic;
        set => magic = value;
    }

    public float PubHp_max{
        get => hp_max;
        set => hp_max = value;
    }

    public float Pubm_moveSpeed{
        get => m_moveSpeed;
        set => m_moveSpeed = value;
    }

    public float PubDash{
        get => dash;
        set => dash = value;
    }

    public float PubDefense_physical{
        get => defense_physical;
        set => defense_physical = value;
    }

    public float PubDefense_magical{
        get => defense_magical;
        set => defense_magical = value;
    }

    public float Pubskill_hill_cool{
        get => skill_hill_cool;
        set => skill_hill_cool = value;
    }

    public float Pubskill_nomal_cool{
        get => skill_nomal_cool;
        set => skill_nomal_cool = value;
    }

    public float Pubskill_Ultimate_cool{
        get => skill_Ultimate_cool;
        set => skill_Ultimate_cool = value;
    }
    #endregion
}
