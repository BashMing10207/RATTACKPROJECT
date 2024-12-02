using System;
using UnityEngine;

namespace MING.Agent
{
    public class AgentMovment : MonoBehaviour
    {
        public event Action<Vector2> OnMovement;
        private Rigidbody _rbCompo;

        private float _moveSpeed = 3f;
        private Vector2 _movement;
        private Agent _agent;

        public void Initialize(Agent entity)
        {
            _agent = entity;
            _rbCompo = entity.GetComponent<Rigidbody>();
        }

        public void OnMove(Vector3 direction, float maxSpeed)
        {
            OnMovement?.Invoke(_rbCompo.linearVelocity);
            _rbCompo.AddForce(direction * Mathf.Lerp(1, 0, (Vector3.Project(direction, _rbCompo.linearVelocity) + _rbCompo.linearVelocity).magnitude / maxSpeed),ForceMode.Impulse);
        }

        public void SetMovement(float movment)
        {
           
        }
    }
}

