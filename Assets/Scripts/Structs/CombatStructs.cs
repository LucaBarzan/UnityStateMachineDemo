using UnityEngine;

public struct AttackInfo
{
    public DamageSO Damage;
    public Vector2 Point;
    public Vector2 Direction;
    public GameObject AttackerObject;

    public AttackInfo(DamageSO damage, Vector2 point, Vector2 direction, GameObject attackerObject)
    {
        Damage = damage;
        Point = point;
        Direction = direction;
        AttackerObject = attackerObject;
    }

    public AttackInfo(DamageSO damage) : this(damage, Vector2.zero, Vector2.zero, null)
    {

    }
}

public struct DamageableInfo
{
    public GameObject DamageableObject;
    public Health Health;

    public DamageableInfo(GameObject damageableObject, Health health)
    {
        DamageableObject = damageableObject;
        Health = health;
    }
}
