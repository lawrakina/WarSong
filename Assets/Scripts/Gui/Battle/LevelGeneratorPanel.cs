using System;
using Battle;
using CoreComponent;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;


namespace Gui.Battle
{
    public sealed class LevelGeneratorPanel : BasePanel
    {
        #region Fields

        private IGeneratorDungeon _generatorDungeon;

        [SerializeField]
        private Button IntoBattleButton;

        [SerializeField]
        private Button RandomSeedButton;

        [SerializeField]
        private Text SeedInputField;

        [SerializeField]
        private Button GenerateMapButton;

        private IBattleInit _battleInit;

        #endregion
        
        
        public void SetReference(IGeneratorDungeon generatorDungeon)
        {
            _generatorDungeon = generatorDungeon;

            IntoBattleButton.OnPointerClickAsObservable().Subscribe(_ => { _battleInit.StartBattle(); })
                            .AddTo(_subscriptions);

            _generatorDungeon.Seed.SubscribeToText(SeedInputField).AddTo(_subscriptions);

            var setRandomSeedCommand = new AsyncReactiveCommand();
            setRandomSeedCommand.Subscribe(_ =>
            {
                _generatorDungeon.SetRandomSeed();
                return Observable.Timer(TimeSpan.FromSeconds(1)).AsUnitObservable();
            }).AddTo(_subscriptions);
            setRandomSeedCommand.BindTo(RandomSeedButton).AddTo(_subscriptions);

            GenerateMapButton.OnPointerClickAsObservable().Subscribe(_ => { _generatorDungeon.BuildDungeon(); })
                             .AddTo(_subscriptions);
        }


        public void SetReference(IBattleInit battleInit)
        {
            _battleInit = battleInit;
        }
    }
}