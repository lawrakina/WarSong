using Windows;
using Data;


namespace Controller.Model
{
    public class GeneratorDungeonModel
    {
        private readonly SceneWindows _windows;
        private DungeonGeneratorData _data;
        public DungeonGeneratorData GeneratorData => _data;
        public SceneWindows Windows => _windows;

        public GeneratorDungeonModel(SceneWindows windows, DungeonGeneratorData generatorData)
        {
            _windows = windows;
            _data = generatorData;
        }
    }
}