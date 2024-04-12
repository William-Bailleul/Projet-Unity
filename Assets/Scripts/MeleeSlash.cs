using UnityEngine;
using System.Collections; 

public class MeleeSlash : MonoBehaviour
{
    public introscript _player;
    public Transform target; // Le transform du personnage
    public MeleeAttackScript _meleeAttack;
    public float rotationSpeed; // Vitesse de rotation

    private Vector3 startPosition;
    private bool isSlashing = false;
    private TrailRenderer _trailRenderer;

    private void Start()
    {
        startPosition = _player.transform.position;
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (_meleeAttack.isAttacking && !isSlashing)
        {
            isSlashing = true;
            StartCoroutine(PerformSlash());
        }
    }

    private IEnumerator PerformSlash()
    {
        float rotationAmount = 0f;
        _trailRenderer.enabled = true;
        while (rotationAmount < 360f)
        {
            
            transform.RotateAround(target.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            rotationAmount += rotationSpeed * Time.deltaTime;
            yield return null;
        }

        // Réinitialiser la position et la rotation une fois l'attaque terminée
        _trailRenderer.enabled = false;
        startPosition = _player.transform.position;
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        isSlashing = false;
        
    }
}