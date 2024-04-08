using UnityEngine;

public class Player : MonoBehaviour
{
    public int _hp;
    public Animator _animator;

    private void Awake()
    {
        Damage.instance.Animator = _animator;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Debug.Log(_hp);
            if (Damage.instance.IsInvincible == false)
            {
                Damage.instance.IsInvincible = true;
                _hp -= Damage.instance.DamagePlayer(1);
                StartCoroutine(Damage.instance.Invincibility());
            }
            else return;
        }
    }

}
