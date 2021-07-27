namespace Code.Data.Unit
{
    public class UnitResource
    {
        private int _maxValue;
        private ResourceEnum _resourceType;
        public int MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }

        public ResourceEnum ResourceType
        {
            get => _resourceType;
            set => _resourceType = value;
        }

        public float ResourceBaseValue { get; set; }
    }
}