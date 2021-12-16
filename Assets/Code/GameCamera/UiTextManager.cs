using Guirao.UltimateTextDamage;
using UnityEngine;


namespace Code.GameCamera{
    public class UiTextManager{
        public UltimateTextDamageManager Manager{ get; set; }
        public Vector3 Offset{ get; set; }

        public void Show(string value, Vector3 uiPosition, string type, bool usingOffset){
            if (Manager){
                if (usingOffset){
                    Manager.Add(value, uiPosition + Offset, type);
                } else{
                    Manager.Add(value, uiPosition , type);
                }
            }
        }
    }
}