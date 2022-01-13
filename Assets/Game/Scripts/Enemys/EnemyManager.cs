using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemys
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] List<Enemy> enemys;
        [SerializeField] float flipRate, moveRate, fireRate = 5f;

        private float lastFireTime, lastFlipTime;

        private void Start()
        {
            InvokeRepeating("EnemysForwardMoveAnimatoin", moveRate, moveRate);
        }

        private void Update()
        {
            if (GameManagement.Instance.GameState == GameManagement.GameStates.START)
            {
                if (CanFire())
                {
                    Fire(PickRandomEnemyFromList());

                    lastFireTime = Time.time;
                }

                if (CanFlip())
                {
                    Flip(PickRandomEnemyFromList());

                    lastFlipTime = Time.time;
                }
            }
        }

        public void DeleteEnemyFromList(Enemy enemy)
        {
            enemys.Remove(enemy);

            if(enemys.Count == 0)
            {
                GameManagement.Instance.WinTheGame();
            }
        }

        #region Fire
        private void Fire(Enemy enemy)
        {
            if (enemy != null)
                enemy.Fire();
        }

        private bool CanFire()
        {
            if ((lastFireTime + fireRate) < Time.time)
            {
                return true;
            }
            else
                return false;
        }
        #endregion

        #region Flip
        private void Flip(Enemy enemy)
        {
            if (enemy != null)
                enemy.Flip();
        }

        private bool CanFlip()
        {
            if ((lastFlipTime + flipRate) < Time.time)
            {
                return true;
            }
            else
                return false;
        }
        #endregion

        private Enemy PickRandomEnemyFromList()
        {
            if (enemys.Count > 0)
                return enemys[Random.Range(0, enemys.Count)];
            else
                return null;
        }

        private void EnemysForwardMoveAnimatoin()
        {
            if (GameManagement.Instance.GameState == GameManagement.GameStates.START)
            {
                transform.DOMoveY(-2f, 2f).SetEase(Ease.InOutQuad).OnComplete(() =>
                {
                    transform.DOMoveY(0.5f, 2f).SetEase(Ease.InOutQuad).SetDelay(2f);
                });
            }
        }
    }
}