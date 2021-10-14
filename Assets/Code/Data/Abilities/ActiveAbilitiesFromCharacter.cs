using System;
using UnityEngine;


namespace Code.Data.Abilities{
    [Serializable]
    public class ActiveAbilitiesFromCharacter{
        [SerializeField]
        private TemplateAbility _special;

        [SerializeField]
        private TemplateAbility _action1;

        [SerializeField]
        private TemplateAbility _action2;

        [SerializeField]
        private TemplateAbility _action3;

        public TemplateAbility Special => _special;
        public TemplateAbility Action1 => _action1;
        public TemplateAbility Action2 => _action2;
        public TemplateAbility Action3 => _action3;
    }
}