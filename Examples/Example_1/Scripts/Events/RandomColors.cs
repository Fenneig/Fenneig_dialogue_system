using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Events
{
    public class RandomColors : MonoBehaviour
    {
        [SerializeField] private int _myNumber;
        private List<Material> _materials = new();

        private void Awake()
        {
            SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
            {
                foreach (var material in skinnedMeshRenderer.materials)
                {
                    _materials.Add(material);
                }
            }
        }

        private void Start()
        {
            GameEvents.Instance.RandomColorModel += DoRandomColorModel;
        }

        private void DoRandomColorModel(int number)
        {
            if (_myNumber == number)
            {
                foreach (var material in _materials)
                {
                    material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                }
            }
        }

        private void OnDestroy()
        {
            GameEvents.Instance.RandomColorModel -= DoRandomColorModel;
        }
    }
}