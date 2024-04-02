using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMenager : MonoBehaviour
{
    public float health;
    public float damage;

    bool colliderBusy = false;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !colliderBusy)
        {
            colliderBusy = true;
            collision.GetComponent<PlayerMenager>().GetDamage(damage);
        }
        else if (collision.tag == "Bullet")
        {
            GetDamage(collision.GetComponent<BulletMenager>().bulletDamage);
            Destroy(collision.gameObject);
        }
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            colliderBusy = false;
        }
    }

    public void GetDamage(float damage)
    {
        if (health - damage >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        slider.value = health;
        AmIDead();



    }
    void AmIDead()
    {
        if (health <= 0)
        {
            DataManager.Instance.EnemyKilled++;
            Destroy(gameObject);
        }
    }

}
