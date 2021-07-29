using System;


namespace Unit
{
    [Serializable] public class UnitCharacteristics : BasicCharacteristics
    {
        public float Speed = 5.0f;
        public float RotateSpeedPlayer = 5.0f;
        private float _dodge = 0.1f;
        private float _critChance = 0.1f;

        public float Dodge
        {
            get
            {
                if (_dodge > 0.75f)
                    return 0.75f;
                else
                    return _dodge;
            }
            set => _dodge = value;
        }

        public float CritChance
        {
            get
            {
                if (_critChance > 0.75f)
                    return 0.75f;
                else
                    return _critChance;
            }
            set => _critChance = value;
        }
    }
}