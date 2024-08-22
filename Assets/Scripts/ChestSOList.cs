using ChestSystem.Chest;
using UnityEngine;

namespace ChestSystem.Chests
{
    [CreateAssetMenu(fileName = "Chest", menuName = "ScriptableObject/ChestList")]
    public class ChestSOList : ScriptableObject
    {
        public ChestSO[] ChestsSOs;

    }
}

