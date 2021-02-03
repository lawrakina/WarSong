using DungeonArchitect;
using UnityEngine;
using UnityEngine.UI;


public class MyUiDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _generator;

    [SerializeField]
    private InputField _seedNumber;

    public void GenerateRandomSeed()
    {
        _seedNumber.text = Random.Range(0, int.MaxValue).ToString();
    }

    public void SetSeed()
    {
        var dungeon = _generator.GetComponent<DungeonConfig>();
        var seed = int.Parse(_seedNumber.text);
        dungeon.Seed = (uint) seed;
    }
}