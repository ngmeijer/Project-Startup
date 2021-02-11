using System;
using UnityEngine;

namespace UnityTemplateProjects.PlayerTDEW
{
    public class PlayerTDEWController : Bolt.EntityBehaviour<IPlayerTDWEState>
    {
        [SerializeField] private Transform _renderTransform;

        [SerializeField] private float _pitch;
        [SerializeField] private float _yaw;

        [SerializeField] private bool _forward;
        [SerializeField] private bool _back;

        [SerializeField] private bool _left;
        [SerializeField] private bool _right;

        private IPlayerMovement _playerMovement;
        
        private static readonly int SpeedXAnimId = Animator.StringToHash("SpeedX");
        private static readonly int SpeedZAnimId = Animator.StringToHash("SpeedZ");
        
        public override void Attached()
        {
            state.SetTransforms(state.PlayerTransform, this.transform, _renderTransform);

            _playerMovement = GetComponent<IPlayerMovement>();

            state.SetAnimator(GetComponentInChildren<Animator>());
            
            if (entity.IsOwner)
            {
                AttacheMainCamera();
            }
        }

        public override void SimulateOwner()
        {
            PoolKeys();

            Vector3 direction = Vector3.zero;
            
            if (_forward)
            {
                direction += transform.forward;
            }
            else if (_back)
            {
                direction += -1 * transform.forward;
            }
            
            if (_left)
            {
                direction += -1 * transform.right;
            }
            else if (_right)
            {
                direction += transform.right;
            }

            _playerMovement.Rotate(_yaw);
            _playerMovement.Move(direction);
            
            state.Animator.SetFloat(SpeedXAnimId, _playerMovement.Direction.x);
            state.Animator.SetFloat(SpeedZAnimId, _playerMovement.Direction.z);
        }

        void PoolKeys()
        {
            _pitch += Input.GetAxis("Mouse Y");
            //_pitch %= 360f;

            _yaw += Input.GetAxis("Mouse X");
            _yaw %= 360f;

            _forward = Input.GetAxis("Vertical") > 0;
            _back = Input.GetAxis("Vertical") < 0;

            _right = Input.GetAxis("Horizontal") > 0;
            _left = Input.GetAxis("Horizontal") < 0;
        }

        public void AttacheMainCamera()
        {
            Camera.main.transform.parent = this.transform;
            Camera.main.transform.localPosition = new Vector3(0, 0.6f, -0.06f);
            Camera.main.transform.localRotation = Quaternion.identity;
        }
    }
}