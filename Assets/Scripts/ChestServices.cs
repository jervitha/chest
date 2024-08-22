using System.Collections.Generic;
using UnityEngine;
namespace ChestSystem.Chest
{
    public class ChestServices : MonoSingleton<ChestServices>
    {
        [SerializeField]
        private List<ChestRarity> chestList;
        [SerializeField]
        private Transform chestParentTransform;

        public Transform ChestParentTransform { get { return chestParentTransform; } private set { } }
        public void SpawnRandomChest()
        {
            ChestSlot slot = SlotServices.Instance.GetVacantSlot();
            if (slot == null)
            {
                UIServices.Instance.EnableSlotsFullPopUp();
                return;
            }

            int randomNumber = UnityEngine.Random.Range(1, 101);
            ChestRarity chestRarity = null;
            int totalProbability = 100;
            foreach (var i in chestList)
            {
                if (randomNumber >= (totalProbability - i.GetProbability()))
                {
                    chestRarity = i;
                    break;
                }
                else
                {
                    totalProbability -= i.GetProbability();
                }
            }
            ChestController controller = slot.GetController();
            controller.SetModel(chestRarity.GetModel());
            controller.SetChestView();
            controller.chestView.SlotSet(slot);
        }

        public bool IsAnyChestUnlocking()
        {
            int numberOfSlots = SlotServices.Instance.GetNumberOfSlots();
            for (int i = 0; i < numberOfSlots; i++)
            {
                ChestSlot slot = SlotServices.Instance.GetSlotAtPos(i);
                if (slot.GetController().currentState.GetState() == ChestStates.Unlocking)
                {
                    return true;
                }
            }
            return false;
        }
        private void Start()
        {
            Debug.Log(ChestServices.Instance.ChestParentTransform.name);
            chestList.Sort((p1, p2) => p1.GetProbability().CompareTo(p2.GetProbability()));
            CreateChestModels();
            CreateChestControllers();
        }
        private void CreateChestModels()
        {
            foreach (var i in chestList)
            {
                ChestModel model = new ChestModel(i.GetChestObject());
                i.SetModel(model);
            }
        }

        private void CreateChestControllers()
        {
            int numberOfSlots = SlotServices.Instance.GetNumberOfSlots();
            for (int i = 0; i < numberOfSlots; i++)
            {
                ChestController chestController = new ChestController();
                SlotServices.Instance.GetSlotAtPos(i).SetController(chestController);
            }
        }

    }

}
