using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region Public Fields

    [Header("Control Settings")]
    [Range(1.0f, 10.0f)]
    [Tooltip("Player Speed. Value between 1 and 10.")]
    public float Speed = 0.0f;
    public float JumpPower = 0.0f;
    [HideInInspector]
    public float MaxSpeed = 0.0f;

    [Header("Health Settings")]
    [Range(1, 5)]
    public int Lives = 0;
    [Range(50.0f, 100.0f)]
    public float MaxHealth = 0.0f;

    #endregion

    #region Private Fields

    [SerializeField]
    private float _health;
    private bool _grounded;
    private bool _jump;
    private bool _attack;

    #endregion
}
