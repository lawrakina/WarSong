using System.Collections.Generic;
using System.Linq;
using Code.Data.Abilities;
using Code.Data.Unit.Player;
using Code.Extension;


namespace Code.Data.Unit{
    public class UnitAbilities{
        private ClassAbilities _settings;
        private int _currentLevel;
        private List<AbilityCell> _activeAbilities;
        private List<AbilityCell> _allAbilities;

        public List<AbilityCell> ActiveAbilities => _activeAbilities;
        public List<AbilityCell> AllAbilities => _allAbilities;

        public UnitAbilities(ClassAbilities settings, int currentLevel){
            _settings = settings;
            _currentLevel = currentLevel;
            
            _activeAbilities = new List<AbilityCell>();
            _allAbilities = new List<AbilityCell>();
            
            
            var specialCell = new AbilityCell(AbilityCellType.Special);
            if(currentLevel >= settings.activeAbilitiesFromCharacter.Special.requiredLevel)
                specialCell.Body = _settings.activeAbilitiesFromCharacter.Special;
            
            var action1 = new AbilityCell(AbilityCellType.Action1);
            if(currentLevel >= settings.activeAbilitiesFromCharacter.Action1.requiredLevel)
                action1.Body = _settings.activeAbilitiesFromCharacter.Action1;
            
            var action2 = new AbilityCell(AbilityCellType.Action2);
            if(currentLevel >= settings.activeAbilitiesFromCharacter.Action2.requiredLevel)
                action2.Body = _settings.activeAbilitiesFromCharacter.Action2;
            
            var action3 = new AbilityCell(AbilityCellType.Action3);
            if(currentLevel >= settings.activeAbilitiesFromCharacter.Action3.requiredLevel)
                action3.Body = _settings.activeAbilitiesFromCharacter.Action3;
            
            _activeAbilities.Add(specialCell);
            _activeAbilities.Add(action1);
            _activeAbilities.Add(action2);
            _activeAbilities.Add(action3);

            _allAbilities.AddRange(_activeAbilities);
            
            foreach (var ability in _settings.ListOfAbilities){
                AbilityCell abilityCell;
                if (CheckAllPermissions(ability))
                    abilityCell = new AbilityCell(AbilityCellType.IsStock);
                else
                    abilityCell = new AbilityCell(AbilityCellType.IsStock, false);
                abilityCell.Body = ability;
                _allAbilities.Add(abilityCell);
            }
        }

        private bool CheckAllPermissions(TemplateAbility ability){
            if (_currentLevel < ability.requiredLevel) return false;
            if (_settings.Owner != ability.requiredClass) return false;
            ///
            /// проверка на возможность использовать способность
            /// 
            return true;
        }

        public void ReplaceActiveAbility(TemplateAbility newAbility, AbilityCellType newElementCellType){
            Dbg.Warning($"ReplaceActiveAbility.Name:{newAbility.uiInfo.Title}, from CellType:{newElementCellType}");
        }
    }
}