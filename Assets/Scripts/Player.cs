using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    idle, move, attack, die
} 

// Entity에서 상속을 받으며, 스태미나를 인터페이스 형태로 활용한다
public class Player : Entity, IStaticable, IDynamicable
{
    [SerializeField] byte level;
    [SerializeField] byte Level
    {
        get { return level; }
        set { level = (byte)Mathf.Clamp(value, 1, maxLevel); } 
    }
    [SerializeField] byte maxLevel;
    
    // 경험치와 최대 경험치
    [SerializeField] ushort exp;
    [SerializeField] ushort maxExp;

    // 스태미나와 최대 스태미나
    [SerializeField] byte stamina;
    public byte Stamina
    {
        get { return stamina; }
        set { stamina = (byte)Mathf.Clamp(value, 0, maxStamina); }
    }
    [SerializeField] byte maxStamina;

    // 포만감과 최대 포만감
    [SerializeField] byte satiety;
    public byte Satiety
    {
        get { return satiety; }
        set { satiety = (byte)Mathf.Clamp(value, 0, maxSatiety); }
    }
    [SerializeField] byte maxSatiety;

    // 수분과 최대 수분
    [SerializeField] byte quench;
    public byte Quench
    {
        get { return quench; }
        set { quench = (byte)Mathf.Clamp(value, 0, maxQuench); }
    }
    [SerializeField] byte maxQuench;

    // 체온
    [SerializeField] float bodyTemperature;

    Vector2 moveDirection;
    [SerializeField] bool isUsingStamina = false;

    new Rigidbody2D rigidbody2D;
    Animator animator;
    PlayerState playerState;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerState = PlayerState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        OnStatic(1);
    }

    // 방향키를 눌러서 4방향 이동
    public void OnMove(InputAction.CallbackContext inputAction)
    {
        // 입력 방향을 받아 저장한다
        moveDirection = inputAction.ReadValue<Vector2>();

        // 입력을 계속하고 있다면 애니메이션 파라미터 변화
        if (inputAction.performed)
        {
            // 플레이어 상태를 움직이는 상태로 바꾼다
            playerState = PlayerState.move;
            // isWalk 파라미터를 true로 변환
            animator.SetBool("isWalk", true);
            // 수평 이동 시 받은 값으로 파라미터 변화
            animator.SetFloat("moveX", moveDirection.x);
            // 수직 이동 시 받은 값으로 파라미터 변화
            animator.SetFloat("moveY", moveDirection.y);
        }
        // 입력을 중단하면 애니메이션 파라미터 변화
        else if (inputAction.canceled)
        {
            // 플레이어 상태를 가만 있는 상태로 바꾼다
            playerState = PlayerState.idle;
            // isWalk 파라미터를 false로 변환
            animator.SetBool("isWalk", false);
        }

    }

    // Left Shift 키를 눌러 스태미나 사용 여부 활성화
    public void OnUsingStamina(InputAction.CallbackContext inputAction)
    {
        // 입력을 지속하고 있다면 true 저장, 아니면 false 저장
        isUsingStamina = inputAction.performed ? true : false;
    }

    public void Move()
    {
        // Left Shift 키가 눌리지 않았다면 걸어간다
        if (isUsingStamina == false)
            rigidbody2D.linearVelocity = moveDirection * MoveSpeed;

        // Left Shift 키가 눌려 있다면 걸을 때의 2배 속력으로 달린다
        else if (playerState == PlayerState.move && isUsingStamina == true && Stamina > 0) 
        {
            rigidbody2D.linearVelocity = moveDirection * MoveSpeed * 2;
            OnDynamic(2);
        }
        else if(stamina <= 0)
        {
            rigidbody2D.linearVelocity = moveDirection * MoveSpeed * 0;
        }
    }

    public void OnStatic(byte staticValue)
    {
        if(isUsingStamina == false) 
            Stamina += staticValue;
    }

    public void OnDynamic(byte dynamicValue)
    {
        Stamina -= dynamicValue;
        
    }
}
