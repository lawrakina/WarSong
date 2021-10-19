namespace Code.Data.Abilities{
    public class AbilityCell{
        private readonly AbilityCellType _abilityCellType;
        private readonly bool _enabled;
        private TemplateAbility _body;
        private UI.UniversalTemplates.UiInfo _ui;
        private bool _isEmpty = true;

        public bool IsEmpty => _isEmpty;
        public AbilityCellType AbilityCellType => _abilityCellType;

        public TemplateAbility Body{
            get{
                return _body; 
            }
            set{
                if (value == null){
                    _isEmpty = true;
                }
                else{
                    _isEmpty = false;
                    _ui.Init(value.uiInfo.Title, value.uiInfo.Description,value.uiInfo.Icon);
                }
                _isEmpty = value == null;
                _body = value;
            }
        }

        public AbilityCell(AbilityCellType abilityCellType, bool enabled = true){
            _abilityCellType = abilityCellType;
            _enabled = enabled;

            _ui = new UI.UniversalTemplates.UiInfo();
        }
    }
}