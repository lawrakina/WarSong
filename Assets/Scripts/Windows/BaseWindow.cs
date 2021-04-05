using Interface;
using UnityEngine;


namespace Windows
{
    // public abstract class BaseWindow :  UiWindow, ICleanup
    // {
    //     #region Fields
    //     
    //     [SerializeField]
    //     private Camera _camera;
    //
    //     [SerializeField]
    //     protected GameObject _content;
    //
    //     #endregion
    //
    //
    //     #region Properties
    //
    //     public delegate Transform GetCharacterSpawn();
    //
    //     public GetCharacterSpawn CharacterSpawn;
    //     public Camera Camera => _camera;
    //
    //     #endregion
    //
    //
    //     private void Start()
    //     {
    //         Init();
    //     }
    //
    //     public override void Show()
    //     {
    //         base.Show();
    //         
    //         _content.SetActive(true);
    //         _camera.enabled = true;
    //     }
    //
    //     public override void Hide()
    //     {
    //         base.Hide();
    //         
    //         _content.SetActive(false);
    //         _camera.enabled = false;
    //     }
    //
    //     public void Cleanup()
    //     {
    //         
    //     }
    // }
}