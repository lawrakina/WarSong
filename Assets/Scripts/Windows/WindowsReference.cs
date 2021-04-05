using System;
using Enums;
using Interface;
using UniRx;
using UnityEngine;


namespace Windows
{
    // [Serializable] public sealed class WindowsReference : IInit, ICleanup
    // {
    //     #region Fields
    //
    //     private IReactiveProperty<EnumMainWindow> _activeWindow;
    //
    //     public CharacterWindow CharacterWindow;
    //     public EquipmentWindow EquipmentWindow;
    //     public BattleWindow BattleWindow;
    //     public SpellsWindow SpellsWindow;
    //     public TalentsWindow TalentsWindow;
    //     private IReactiveProperty<EnumBattleWindow> _battleState;
    //
    //     #endregion
    //
    //
    //     public void Cleanup()
    //     {
    //         CharacterWindow.Cleanup();
    //         EquipmentWindow.Cleanup();
    //         BattleWindow.Cleanup();
    //         SpellsWindow.Cleanup();
    //         TalentsWindow.Cleanup();
    //     }
    //
    //     public void Init()
    //     {
    //         CharacterWindow.Init();
    //         EquipmentWindow.Init();
    //         BattleWindow.Init();
    //         SpellsWindow.Init();
    //         TalentsWindow.Init();
    //     }
    //
    //
    //     #region ClassLiveCycles
    //
    //     public void Ctor(IReactiveProperty<EnumMainWindow> activeWindow,
    //         IReactiveProperty<EnumBattleWindow> battleState)
    //     {
    //         _battleState = battleState;
    //         _activeWindow = activeWindow;
    //         // CharacterWindow.Ctor();
    //         EquipmentWindow.Ctor();
    //         // BattleWindow.Ctor(_battleState);
    //         SpellsWindow.Ctor();
    //         TalentsWindow.Ctor();
    //
    //
    //         _activeWindow.Subscribe(_ => { ShowOnlyActiveWindow(); });
    //     }
    //
    //     #endregion
    //
    //
    //     private void ShowOnlyActiveWindow()
    //     {
    //         Debug.Log($"WindowsReference.ActiveWindow:{_activeWindow}");
    //         if (_activeWindow.Value == EnumMainWindow.Character) CharacterWindow.Show();
    //         else CharacterWindow.Hide();
    //         if (_activeWindow.Value == EnumMainWindow.Equip) EquipmentWindow.Show();
    //         else EquipmentWindow.Hide();
    //         if (_activeWindow.Value == EnumMainWindow.Battle) BattleWindow.Show();
    //         else BattleWindow.Hide();
    //         if (_activeWindow.Value == EnumMainWindow.Spells) SpellsWindow.Show();
    //         else SpellsWindow.Hide();
    //         if (_activeWindow.Value == EnumMainWindow.Talents) TalentsWindow.Show();
    //         else TalentsWindow.Hide();
    //     }
    // }
}