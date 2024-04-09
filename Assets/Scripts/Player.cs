using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    public int _hp;
    public Animator _animator;

    private void Awake()
    {
        Damage.instance.Animator = _animator;
        _rb2d = GetComponent<Rigidbody2D>();
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

    public void Flip(float rotation)
    {
        _rb2d.transform.localRotation = Quaternion.Euler(0f, rotation, 0f);
    }

}
