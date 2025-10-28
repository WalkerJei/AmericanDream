using UnityEngine;

public class Entity : MonoBehaviour
{

    // 생명력과 최대 생명력 
    [SerializeField] ushort Hp;
    [SerializeField] ushort maxHp;

    //
    [SerializeField] float walkSpeed;
    public float WalkSpeed
    {
        get { return walkSpeed; }
        set { walkSpeed = value; }
    }

    [SerializeField] float runSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
