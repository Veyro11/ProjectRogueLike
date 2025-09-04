using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum StatType
    {
        Attack,
        MoveSpeed,
        Health,
    }
    [System.Serializable]
    public class StatModifier
    {
        public StatType statType;
        public float value;
    }
    [CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item")]
    public class ItemSO: ScriptableObject
    {
        public List<StatModifier> modifiers;
    }

