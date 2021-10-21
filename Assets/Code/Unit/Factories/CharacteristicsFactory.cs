using System.Linq;
using Code.Data;
using Code.Data.Unit;
using Code.Data.Unit.Player;
using Code.Extension;


namespace Code.Unit.Factories{
    public sealed class CharacteristicsFactory{
        private readonly PlayerClassesData _settings;
        private readonly MathParser _parser = new MathParser();
        private UnitEquipment _equipment;
        private CharacterSettings _characterSettings;

        private int LvlCurrent{ get; set; }
        private int LvlMax{ get; set; }

        public CharacteristicsFactory(PlayerClassesData settings){
            _settings = settings;
        }

        public UnitCharacteristics GenerateCharacteristics(UnitCharacteristics characteristics,
            UnitEquipment equipment, UnitLevel level, CharacterSettings value){
            LvlCurrent = level.CurrentLevel;
            LvlMax = level.MaximumPossibleCharacterLevel;
            _equipment = equipment;
            _characterSettings = value;
            if (characteristics == null){
                characteristics = new UnitCharacteristics();
                var fromDataBase = _settings._classesStartCharacteristics.FirstOrDefault(
                    x => x.CharacterClass == value.CharacterClass);
                characteristics.Start = fromDataBase.Start;
                characteristics.ForOneLevel = fromDataBase.ForOneLevel;
                characteristics.CharacterClass = fromDataBase.CharacterClass;
            }

            //Base params
            characteristics.Values.Agility = _equipment.FullAgility + characteristics.Start.Agility +
                                             characteristics.ForOneLevel.Agility * level.CurrentLevel;
            characteristics.Values.Intellect = _equipment.FullIntellect + characteristics.Start.Intellect +
                                               characteristics.ForOneLevel.Intellect * level.CurrentLevel;
            characteristics.Values.Spirit = _equipment.FullSpirit + characteristics.Start.Spirit +
                                            characteristics.ForOneLevel.Spirit * level.CurrentLevel;
            characteristics.Values.Stamina = _equipment.FullStamina + characteristics.Start.Stamina +
                                             characteristics.ForOneLevel.Stamina * level.CurrentLevel;
            characteristics.Values.Strength = _equipment.FullStrength + characteristics.Start.Strength +
                                              characteristics.ForOneLevel.Strength * level.CurrentLevel;
            //Modifiers
            characteristics.AttackModifier = Calculate(GetModifier(TargetParam.AttackModifier));
            characteristics.SpeedAttackModifier = Calculate(GetModifier(TargetParam.SpeedAttackModifier));
            characteristics.LagBeforeAttackModifier = Calculate(GetModifier(TargetParam.LagBeforeAttackModifier));
            characteristics.DistanceModifier = Calculate(GetModifier(TargetParam.DistanceModifier));
            characteristics.CritChanceModifier = Calculate(GetModifier(TargetParam.CritChanceModifier));
            characteristics.DodgeChanceModifier = Calculate(GetModifier(TargetParam.DodgeChanceModifier));
            characteristics.ArmorModifier = Calculate(GetModifier(TargetParam.ArmorModifier));

            return characteristics;
        }

        private CharacteristicsModifier GetModifier(TargetParam param){
            return _settings._characteristicsModifiers.FirstOrDefault(x =>
                (x.Owner == _characterSettings.CharacterClass) && (x.TargetParam == param));
        }

        private ModificationOfObjectOfParam Calculate(CharacteristicsModifier modifier){
            var example = "";
            example = ReplaceFormulaByValue(modifier.Formula, modifier.Str, nameof(modifier.Str));
            example = ReplaceFormulaByValue(example, modifier.Agi, nameof(modifier.Agi));
            example = ReplaceFormulaByValue(example, modifier.Int, nameof(modifier.Int));
            example = ReplaceLevelInFormula(example, modifier,nameof(modifier.LvlRatio));
            
            var result = (float)_parser.Parse(example);
            
            Dbg.Log($"Characteristics {modifier.TargetParam} is calculated:{result}");

            return new ModificationOfObjectOfParam(result, modifier.TypeOfModification);
        }

        private string ReplaceLevelInFormula(string request, CharacteristicsModifier modifier, string lvlRatioName){
            request = request.Replace($"{lvlRatioName}", $"{modifier.LvlRatio:f3}");
            request = request.Replace($"{nameof(LvlMax)}", $"{LvlMax}");
            request = request.Replace($"{nameof(LvlCurrent)}", $"{LvlCurrent}");
            return request;
        }
        
        
        private string ReplaceFormulaByValue(string request, KvpValueRatio kvp, string name){
            request = request.Replace($"{name}{nameof(kvp.R)}", $"{kvp.R}");
            request = request.Replace($"{name}{nameof(kvp.V)}", $"{kvp.V}");
            return request;
        }
    }
}