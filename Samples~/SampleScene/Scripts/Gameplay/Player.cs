using UnityEngine;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerStats))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private Animator _animator;

        private Rigidbody _rigidbody;
        private PlayerStats _playerStats;
        private float _vertical;
        private float _horizontal;
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");

        public PlayerStats PlayerStats => _playerStats;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerStats = GetComponent<PlayerStats>();
        }

        protected void Update()
        {
            InputHandler();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void InputHandler()
        {
            _vertical = Input.GetAxis("Vertical");
            _horizontal = Input.GetAxis("Horizontal");
        }

        private void Movement()
        {
            Vector3 movement = new Vector3(_horizontal, 0, _vertical) * _moveSpeed;
            _rigidbody.velocity = movement;
            HandleAnimation(movement);
            HandleRotation(movement);
        }

        private void HandleRotation(Vector3 movement)
        {
            Vector3 lookDirection = Vector3.RotateTowards(transform.forward, movement, _rotateSpeed * Time.fixedDeltaTime, .0f);
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        private void HandleAnimation(Vector3 movement)
        {
            float animationMoveSpeed = Vector3.Magnitude(movement.normalized);
            _animator.SetFloat(MoveSpeed, animationMoveSpeed);
        }
        
    }
}