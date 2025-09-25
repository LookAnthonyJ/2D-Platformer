using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iframes")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invulnerbility());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                

                //player
              //  if(GetComponent<PlayerMovement>() != null)
               //  GetComponent<PlayerMovement>().enabled = false;


                //ememy
              //  if(GetComponentInParent<EnemyPatrol>() != null)
                 //   GetComponentInParent<EnemyPatrol>().enabled = false;

                //if(GetComponent<MeleeEnemy>() != null)
               //     GetComponent<MeleeEnemy>().enabled = false;

                
                //Deactivate all attached comnent classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("Grounded", true);
                anim.SetTrigger("Die");

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }
      public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("Die");
        anim.Play("idle");
        StartCoroutine(Invulnerbility());

        //Deactivate all attached comnent classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }
    private IEnumerator Invulnerbility()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
