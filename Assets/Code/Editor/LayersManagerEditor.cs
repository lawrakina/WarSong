using Code.Extension;
using UnityEditor;
using UnityEngine;


namespace Code.Editor
{
    [CustomEditor(typeof(LayersManager))]
    public class LayersManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var layersScripts = (LayersManager) target;
            if (GUILayout.Button(@"Check Layers"))
            {
                layersScripts.CheckLayers();
            }
        }
    }
}