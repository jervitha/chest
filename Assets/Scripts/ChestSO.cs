using ChestSystem.Chests;
using UnityEngine;

namespace ChestSystem.Chest
{
    [CreateAssetMenu(fileName = "Chest", menuName = "ScriptableObject/NewChest")]
    public class ChestSO : ScriptableObject
    {
        public Sprite ChestClosedImage;
        public Sprite ChestOpenImage;

        public int CoinsNeeded;
        public int CoinsMin;
        public int CoinsMax;
        public int GemsMin;
        public int GemsMax;

        public ChestType Type;
        public int UnlockDuration;
    }
}

