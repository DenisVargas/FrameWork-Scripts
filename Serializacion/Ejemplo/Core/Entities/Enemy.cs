using System;

namespace Core
{
    namespace Entities
    {
        [Serializable]
        public class Enemy
        {
            public Stats stats;
        }

        [Serializable]
        public class Stats
        {
            public string name;
            public int hp;
            public int damage;
            public float movementSpeed;

            public Stats(string name, int hp, int damage, float movementSpeed)
            {
                this.name = name;
                this.hp = hp;
                this.damage = damage;
                this.movementSpeed = movementSpeed;
            }
        }
    }
}

