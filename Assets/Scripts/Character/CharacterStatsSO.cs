using UnityEngine;

namespace RPG.Character
{
    [CreateAssetMenu(
        fileName = "Character Stats",
        menuName = "RPG/Character Stats SO",
        order = 0)]
    public class CharacterStatsSO : ScriptableObject
    {
        [field: SerializeField] public float Health { get; set; } = 100f;
        [field: SerializeField] public float Damage { get; set; } = 10f;
        [field: SerializeField] public float WalkSpeed { get; set; } = 1f;
        [field: SerializeField] public float RunSpeed { get; set; } = 1.25f;
    }
}
