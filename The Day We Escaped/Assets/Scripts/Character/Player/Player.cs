using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement _movementModule;

    private void Awake()
    {
        _movementModule = GetComponent<PlayerMovement>();
    }
}
