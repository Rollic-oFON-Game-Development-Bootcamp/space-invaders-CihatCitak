using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemys
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] EnemyManager enemyManager;
        [SerializeField] GameObject explosionPrefab;
        [SerializeField] GameObject laserRedPrefab;

        public void Fire()
        {
            Destroy(Instantiate(laserRedPrefab, transform.position, Quaternion.identity), 10f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerLaser"))
            {
                enemyManager.DeleteEnemyFromList(this);

                Destroy(collision.gameObject);

                Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 1f);

                Destroy(gameObject);
            }
        }
    }
}
