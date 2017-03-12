using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthController : MonoBehaviour {
    public Image bar;
    public float maxHP = 100;
    public float currentHP = 100;
    public float damage = 10;
    public GameObject damageIndicator;
    private Image dmgImage;

    private float hurtTime = 2;
    private float hurtTimeCounter = 0;
    private bool isRegen = false;
    private float healthRegen = 10;
    private float counter = 0f;
    private float percent = 0;
    private float dmgCounter = 0.0f;

    void Start()
    {
        dmgImage = damageIndicator.GetComponent<Image>();
    }

    void Update()
    {
        bar.fillAmount = currentHP / maxHP;
        if (Input.GetButtonDown("Jump"))
        {
            Damage();
        }
        Hurt();
        Regen();
    }

    void Hurt()
    {
        if (isRegen == false && currentHP < maxHP)
        {
            hurtTimeCounter += Time.deltaTime;
            if (hurtTimeCounter >= hurtTime)
            {
                isRegen = true;
                hurtTimeCounter = 0;
            }
        }
    }

    void Regen()
    {
        if (isRegen == true)
        {
            counter += Time.deltaTime;
            if (counter >= 1)
            {
                currentHP += healthRegen;
                counter = 0;
            }
        }

        if (currentHP >= maxHP)
        {
            Mathf.Clamp(currentHP, 0, maxHP);
            isRegen = false;
        }
    }
        
    public void Damage()
    {
        percent = 0;
        dmgCounter = 0;
        AudioManager.instance.PlaySound("Hurt", transform.position);
        StartCoroutine(DamageIndicator());
        currentHP -= damage;
        Mathf.Clamp(currentHP, 0, maxHP);
        isRegen = false;
    }

    IEnumerator DamageIndicator()
    {
        float time = 3.0f;

        while(percent <= 1)
        {
            damageIndicator.SetActive(true);
            dmgCounter += Time.deltaTime;
            percent = dmgCounter / time;

            dmgImage.color = Color.Lerp(Color.white,Color.clear,percent);

            if (percent >= 1)
            {
                damageIndicator.SetActive(false);
            }
            yield return null;
        }
    }

}
