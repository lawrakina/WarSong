using Code.Extension;
using UnityEditor;
using UnityEngine;


namespace Code.Editor
{
    [CustomEditor(typeof(LayerManager))]
    public class LayersManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var layersScripts = (LayerManager) target;
            if (GUILayout.Button(@"Check Layers"))
            {
                layersScripts.CheckLayers();
            }
        }
    }
}