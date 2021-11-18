using System.Collections.Generic;
using Code.Data.Unit;
using Code.Fight.EcsFight.Settings;
using UnityEngine;


namespace Code.Fight.EcsFight.Battle{
    public class ListWeapons : List<Weapon>{
        private Weapon _main;
        private Weapon _second;
        public float SqrDistance{
            get{
                if (!Equals(_main, null)){
                    return _main.Distance * _main.Distance;
                }

                if (!Equals(_second, null)){
                    return _second.Distance * _second.Distance;
                }

                return 1.0f;
            }
        }
        // public MeleeRangeSplash MeleeRangeSplash{ get; set; }
        public int WeaponTypeAnimation{ get; set; }
        public int AttackTypeAnimation{
            get{
                //https://docs.google.com/spreadsheets/d/1YvONoal9Z7BBDE6zL_1d-bnHOJLY_sTf7fCPLNPkMFs/edit#gid=894606032
                var max = 0; // кол-во анимаций
                if (WeaponTypeAnimation == 0) max = 4; //unarmed
                if (WeaponTypeAnimation == 5) max = 3; //oneHand
                if (WeaponTypeAnimation == 22) max = 4; //spear
                if (WeaponTypeAnimation == 23) max = 4; //sword
                if (WeaponTypeAnimation == 24) max = 4; //staff
                if (WeaponTypeAnimation == 25) max = 6; //bow
                if (WeaponTypeAnimation == 26) max = 6; //crosbow
                return Random.Range(0, max);
            }
        }
        public float WeaponRange{
            get{
                if (!Equals(_main, null)){
                    return _main.Distance;
                }

                if (!Equals(_second, null)){
                    return _second.Distance;
                }

                return 1.0f;
            }
        }

        public void AddMain(Weapon weapon){
            _main = weapon;
            Add(weapon);
        }

        public void AddSecond(Weapon weapon){
            _second = weapon;
            Add(weapon);
        }
    }
}