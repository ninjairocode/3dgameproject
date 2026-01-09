using System.Collections;
using UnityEngine;

namespace Bag
{
    public static class ProjectilePowerManager
    {
        public static int bonusDamage = 0;

        public static IEnumerator ApplyPower(int amount, float duration)
        {
            bonusDamage += amount;

            
            PowerDurationUI.Instance.ShowDuration(duration);

            yield return new WaitForSeconds(duration);

            bonusDamage -= amount;
            if (bonusDamage < 0)
                bonusDamage = 0;
        }
    }
}
