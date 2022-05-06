using Leopotam.Ecs;


namespace Code.Unit{
    public struct InfoResource{
        private float _value;
        private EcsEntity _sender;

        public float Value => _value;
        public EcsEntity Sender => _sender;
        
        public InfoResource(float value, EcsEntity entity){
            _value = value;
            _sender = entity;
        }
    }
}