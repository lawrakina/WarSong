using System.Collections.Generic;
using System.Linq;
using Code.Data.Abilities;
using Code.Data.Unit.Player;


namespace Code.Data.Unit{
    public class UnitAbilities{
        private readonly PlayerClassesData _db;
        private ClassAbilities _settings;
        private int _currentLevel;
        private List<AbilityCell> _activeAbilities;
        private List<AbilityCell> _allAbilities;

        public List<AbilityCell> ActiveAbilities => _activeAbilities;
        public List<AbilityCell> AllAbilities => _allAbilities;

        public UnitAbilities(PlayerClassesData db, ClassAbilities settings, int currentLevel){
            _db = db;
            _settings = settings;
            _currentLevel = currentLevel;

            _activeAbilities = new List<AbilityCell>();
            _allAbilities = new List<AbilityCell>();


            var specialCell = GetOfCreateAbilityCell(AbilityCellType.Special);
            var action1 = GetOfCreateAbilityCell(AbilityCellType.Action1);
            var action2 = GetOfCreateAbilityCell(AbilityCellType.Action2);
            var action3 = GetOfCreateAbilityCell(AbilityCellType.Action3);

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

        private AbilityCell GetOfCreateAbilityCell(AbilityCellType cellType){
            var result = new AbilityCell(cellType);
            var kvpFromDb = _settings.activeAbilitiesFromCharacter
                .FirstOrDefault(x => x.CellType == cellType);
            if (kvpFromDb != null){
                result.Body = kvpFromDb.Ability;
            } else{
                _settings.activeAbilitiesFromCharacter.Add(new KvpAbilityAndCellType(null, cellType));
            }

            return result;
        }

        private bool CheckAllPermissions(TemplateAbility ability){
            if (_currentLevel < ability.requiredLevel) return false;
            if (_settings.Owner != ability.requiredClass) return false;
            ///
            /// проверка на возможность использовать способность
            /// 
            return true;
        }

        public void ReplaceActiveAbility(TemplateAbility newAbility, AbilityCellType newCellType){
            var abilityInDb =
                _settings.activeAbilitiesFromCharacter.FirstOrDefault(x => x.CellType == newCellType);
            if (abilityInDb != null)
                abilityInDb.Ability = newAbility;
            else{
                _settings.activeAbilitiesFromCharacter.Add(new KvpAbilityAndCellType(newAbility, newCellType));
            }
        }
    }
}