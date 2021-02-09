using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterNS
{
    public class Player : Character
    {
        private PlayerMovement _movementModule;

        private void Awake()
        {
            _movementModule = GetComponent<PlayerMovement>();
        }
    }
}