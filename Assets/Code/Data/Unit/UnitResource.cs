using Code.Extension;


namespace Code.Data.Unit{
    public class UnitResource{
        private int _maxValue;
        private ResourceEnum _resourceType;
        private float _resourceBaseValue;
        
        public int MaxValue{
            get => _maxValue;
            set => _maxValue = value;
        }

        public ResourceEnum ResourceType{
            get => _resourceType;
            set => _resourceType = value;
        }

        public float ResourceBaseValue{
            get{ return _resourceBaseValue; }
            set{
                _resourceBaseValue = value;
                Dbg.Log($"Change resource value: {value}");
            }
        }

        public override string ToString(){
            return $"Resource points: {_resourceBaseValue}/{MaxValue}";
        }
    }
}