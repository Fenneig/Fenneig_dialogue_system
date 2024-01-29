using UnityEngine;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.IngameUI
{
    [RequireComponent(typeof(Canvas))]
    public class UIFollowCamera : MonoBehaviour
    {
        private Canvas _canvas;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _canvas = GetComponent<Canvas>();
        }

        private void Update()
        {
            _canvas.transform.LookAt(_camera.transform);
        }
    }
}