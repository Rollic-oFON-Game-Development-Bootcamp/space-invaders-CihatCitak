using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemys
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] EnemyManager enemyManager;
        [SerializeField] GameObject explosionPrefab;
        [SerializeField] GameObject laserRedPrefab;
        [SerializeField] float flipDuration;

        private Vector3 starLocalPosition;

        private void Start()
        {
            starLocalPosition = transform.localPosition;
        }

        public void Fire()
        {
            Destroy(Instantiate(laserRedPrefab, transform.position, Quaternion.identity), 5f);
        }

        public void Flip()
        {
            var flipPosition = new Vector3(Random.Range(-1.75f, 1.75f), 0f, 0f);

            transform.DOLocalMove(flipPosition, flipDuration);
            transform.DOLocalMove(starLocalPosition, flipDuration).SetDelay(flipDuration);

            transform.DOLocalRotate(new Vector3(0f, 0, 0), flipDuration, RotateMode.LocalAxisAdd).SetDelay(flipDuration);
            transform.DOLocalRotate(new Vector3(0f, 30f, 0), flipDuration, RotateMode.LocalAxisAdd).OnComplete(() => Fire());
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
