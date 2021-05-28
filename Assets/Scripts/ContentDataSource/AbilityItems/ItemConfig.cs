using UnityEngine;


namespace ContentDataSource.AbilityItems
{
    [CreateAssetMenu(fileName = "Item", menuName = "Base Item for Items, Abilities", order = 0)]
    public class ItemConfig : ScriptableObject
    {
        public int id;
        public string title;
        public string description;
        public Sprite icon;
    }
}