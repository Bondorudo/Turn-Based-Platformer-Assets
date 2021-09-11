using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CharacterEquipment : MonoBehaviour
{
    private UI_CharacterEquipmentSlot skillSlot;
    private UI_CharacterEquipmentSlot charmSlot;

    private void Awake()
    {
        skillSlot = transform.Find("SkillSlot").GetComponent<UI_CharacterEquipmentSlot>();
        charmSlot = transform.Find("CharmSlot").GetComponent<UI_CharacterEquipmentSlot>();

        skillSlot.OnItemDropped += SkillSlot_OnItemDropped;
    }

    private void SkillSlot_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        // Item Dropped in skill slot
    }
}
