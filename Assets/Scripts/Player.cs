using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    // 경험치와 최대 경험치
    [SerializeField] int exp;
    [SerializeField] int maxExp;

    // 스태미나와 최대 스태미나
    [SerializeField] ushort stamina;
    [SerializeField] ushort maxStamina;

    // 포만감과 최대 포만감
    [SerializeField] ushort satiety;
    [SerializeField] ushort maxSatiety;

    // 수분과 최대 수분
    [SerializeField] ushort hydration;
    [SerializeField] ushort maxHydration;

    // 체온
    [SerializeField] float bodyTemperature;

    Vector2 moveDirection;

    new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext inputAction)
    {
        moveDirection = inputAction.ReadValue<Vector2>();
    }

    public void Move()
    {
        rigidbody2D.linearVelocity = moveDirection * WalkSpeed;
    }
}
