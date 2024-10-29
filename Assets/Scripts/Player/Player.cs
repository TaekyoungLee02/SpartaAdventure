using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerController controller;
    [HideInInspector] public PlayerStatus status;
    [HideInInspector] public PlayerInteraction interaction;
    [HideInInspector] public const string PLAYER_TAG = "Player";

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        status = GetComponent<PlayerStatus>();
        interaction = GetComponent<PlayerInteraction>();
    }
}
