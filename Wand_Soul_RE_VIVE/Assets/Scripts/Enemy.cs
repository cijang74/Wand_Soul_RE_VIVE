using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    protected int hp = 10;  //���� hp
    public UnityEvent DieEvent;
    public virtual void Damage(int damage)      //�÷��̾� ������ �Լ� ���� .Damage(20) �̷� ������ Ȱ��
    {
        hp -= damage;
    }
    protected virtual void Die()
    {
        DieEvent.Invoke();
        this.gameObject.SetActive(false);
    }
}
