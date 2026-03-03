using Unity.VisualScripting;
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
    byte Level
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

    InventoryBag inventoryBag;


    // 플레이어가 향한 방향
    Vector2 moveDirection;
    // 플레이어가 스태미나를 모두 사용해 지쳐버린 여부
    bool isTired = false;

    new Rigidbody2D rigidbody2D;
    Animator animator;

    InputAction moveAction;
    InputAction usingStaminaAction;

    PlayerState playerState;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 플레이어 상태를 기본 상태로 설정
        playerState = PlayerState.idle;
        
        // 플레이어 조작 상태를 설정
        moveAction = InputSystem.actions.FindAction("Move");
        usingStaminaAction = InputSystem.actions.FindAction("UsingStamina");
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

    // 플레이어 이동
    public void Move()
    {
        // 방향키가 눌려 있을 때
        if (moveAction.IsPressed())
        {
            // 입력 방향을 받아서 저장
            moveDirection = moveAction.ReadValue<Vector2>();

            // 플레이어 상태를 움직이는 상태로 바꾼다
            playerState = PlayerState.move;
            // isWalk 파라미터를 true로 변환
            animator.SetBool("isWalk", true);
            // 수평 이동 시 받은 값으로 파라미터 변화
            animator.SetFloat("moveX", moveDirection.x);
            // 수직 이동 시 받은 값으로 파라미터 변화
            animator.SetFloat("moveY", moveDirection.y);

            if (playerState == PlayerState.move)
            {
                if (usingStaminaAction.IsPressed() && isTired == false)
                {
                    if(isTired == false)
                    {
                        rigidbody2D.linearVelocity = moveDirection * MoveSpeed * 2;
                        Debug.Log(stamina);
                        Debug.Log(moveDirection);
                        OnDynamic(2);

                        if (Stamina <= 0)
                        {
                            Stamina = 0;
                            isTired = true;
                        }
                    }
                }
                else
                {
                    rigidbody2D.linearVelocity = moveDirection * MoveSpeed;

                    if (Stamina >= maxStamina)
                    {
                        isTired = false;
                    }
                }
            }
        }

        // 방향키가 눌려 있지 않으면
        else
        {
            // 플레이어 상태를 가만 있는 상태로 바꾼다
            playerState = PlayerState.idle;
            // isWalk 파라미터를 false로 변환
            animator.SetBool("isWalk", false);
            // 
            rigidbody2D.linearVelocity = moveDirection * MoveSpeed * 0;
        }   
    }

    public void OnStatic(byte staticValue)
    {
        if(!usingStaminaAction.IsPressed())
            Stamina += staticValue;
    }

    public void OnDynamic(byte dynamicValue)
    {
        Stamina -= dynamicValue;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IObjectItem contactInterface = collision.gameObject.GetComponent<IObjectItem>();

        if (contactInterface != null)
        {
            ItemData item = contactInterface.ContactItem();
            
            inventoryBag.AddItem(item);
            Destroy(collision.gameObject);
        }
    }
}
