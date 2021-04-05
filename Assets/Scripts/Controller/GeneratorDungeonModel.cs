using Data;


namespace Controller
{
    public class GeneratorDungeonModel
    {
        private DungeonGeneratorData _data;
        public DungeonGeneratorData GeneratorData => _data;

        public GeneratorDungeonModel(DungeonGeneratorData generatorData)
        {
            _data = generatorData;
        }
    }
}