using UnityEngine;

public class Entity : MonoBehaviour
{
    // 생명력과 최대 생명력 
    [SerializeField] byte hp;
    public byte Hp
    {
        get { return hp; }
        set { hp = (byte)Mathf.Clamp(value, 0, maxHp); }
    }
    [SerializeField] byte maxHp;

    // 이동속도
    [SerializeField] float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
}
