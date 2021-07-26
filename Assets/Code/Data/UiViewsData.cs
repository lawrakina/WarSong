﻿using Code.UI;
using Code.UI.Adventure;
using Code.UI.BottomNavigation;
using Code.UI.Character;
using Code.UI.CharacterList;
using Code.UI.Inventory;
using Code.UI.Shop;
using Code.UI.Tavern;
using Code.UI.TopNavigation;
using UnityEngine;


namespace Code.Data
{
    [CreateAssetMenu(fileName = "Config_UiViewsData", menuName = "Data/Config UiViews Data")]
    public class UiViewsData : ScriptableObject
    {
        [Header("List character")]
        public CharacterListView Create_CharacterList;
        public CreatePrototypeView Create_CreatePrototype;
        public EditPrototypeView Create_EditProtype;
        [Space]
        
        [Header("Main navigation")]
        public BottomNavigationView BottomNavigation;
        public TopNavigationView TopNavigation;
        public MainMenuView MainMenu;
        [Space]
        
        [Header("Adventure tab")]
        public AdventureView AdventureView;
        [Space]
        
        [Header("Character tab")]
        public CharacterView CharacterView;
        [Space] 
        
        [Header("Inventory tab")] 
        public InventoryView Inventory;
        [Space] 
        
        [Header("Tavern tab")] 
        public TavernView TavernView;
        [Space] 
        
        [Header("Shop tab")] 
        public ShopView Shop;

    }
}