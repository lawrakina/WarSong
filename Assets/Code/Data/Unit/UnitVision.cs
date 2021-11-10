using System.Collections.Generic;
using Code.Unit;
using SensorToolkit;


namespace Code.Data.Unit{
    public class UnitVision{
        private bool _needUpdateList;
        private List<IEnemyView> _list = new List<IEnemyView>();
        public bool NeedUpdateList{
            get{
                if (_needUpdateList) _needUpdateList = false;
                return _needUpdateList;
            }
        }
        public Sensor Visor{ get; set; }
        public List<IEnemyView> List => _list;

        public void AddEnemy(EnemyView enemy){
            ListChanged();
            _list.Add(enemy);
        }

        public void DelEnemy(EnemyView enemy){
            ListChanged();
            _list.Remove(enemy);
        }

        private void ListChanged(){
            _needUpdateList = true;
        }
    }
}