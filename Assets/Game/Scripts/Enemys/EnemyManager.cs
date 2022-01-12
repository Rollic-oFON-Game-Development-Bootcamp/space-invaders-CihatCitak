using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemys
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] List<Enemy> enemys;
        [SerializeField] float fireRate = 5f;

        private float lastFireTime;

        private void Update()
        {
            if (CanFire())
                Fire(PickRandomEnemyFromList());
        }

        public void DeleteEnemyFromList(Enemy enemy)
        {
            enemys.Remove(enemy);
        }

        private void Fire(Enemy enemy)
        {
            enemy.Fire();

            lastFireTime = Time.time;
        }

        private Enemy PickRandomEnemyFromList()
        {
            return enemys[Random.Range(0, enemys.Count)];
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

        private void EnemysForwardMoveAnimatoin()
        {

        }
    }
}