using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "UnitLevelData", menuName = "Data/Units level Data")]
    public sealed class UnitLevelData : ScriptableObject
    {
        // https://external-preview.redd.it/o2qSIhPtNsS5sbenkl6SRUj5djEJm8kFm0GIsVvqu2U.jpg?auto=webp&s=f46557d59ed004ddef28fbfcd3231c31e7aa62c0
        [SerializeField]
        public LevelValue[] Levels =
        {
            new LevelValue()
            {
                Number = 1,
                MaxiPointsExperience = 400
            }, 
            new LevelValue()
            {
                Number = 2,
                MaxiPointsExperience = 900
            }, 
            new LevelValue()
            {
                Number = 3,
                MaxiPointsExperience = 1400
            }, 
            new LevelValue()
            {
                Number = 4,
                MaxiPointsExperience = 2100
            }, 
            new LevelValue()
            {
                Number = 5,
                MaxiPointsExperience = 2800
            }, 
            new LevelValue()
            {
                Number = 6,
                MaxiPointsExperience = 3600
            }, 
            new LevelValue()
            {
                Number = 7,
                MaxiPointsExperience = 4500
            }, 
            new LevelValue()
            {
                Number = 8,
                MaxiPointsExperience = 5400
            }, 
            new LevelValue()
            {
                Number = 9,
                MaxiPointsExperience = 6500
            }, 
            new LevelValue()
            {
                Number = 10,
                MaxiPointsExperience = 7600
            }, 
            new LevelValue()
            {
                Number = 11,
                MaxiPointsExperience = 8800
            }, 
            new LevelValue()
            {
                Number = 12,
                MaxiPointsExperience = 10100
            }, 
            new LevelValue()
            {
                Number = 13,
                MaxiPointsExperience = 11400
            }, 
            new LevelValue()
            {
                Number = 14,
                MaxiPointsExperience = 12900
            }, 
            new LevelValue()
            {
                Number = 15,
                MaxiPointsExperience = 14400
            }, 
            new LevelValue()
            {
                Number = 16,
                MaxiPointsExperience = 16000
            }, 
            new LevelValue()
            {
                Number = 17,
                MaxiPointsExperience = 17700
            }, 
            new LevelValue()
            {
                Number = 18,
                MaxiPointsExperience = 19400
            }, 
            new LevelValue()
            {
                Number = 19,
                MaxiPointsExperience = 21300
            }, 
            new LevelValue()
            {
                Number = 20,
                MaxiPointsExperience = 23200
            }, 
        };
    }
}