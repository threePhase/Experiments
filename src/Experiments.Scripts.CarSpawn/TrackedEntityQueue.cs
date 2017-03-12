using GTA;
using System.Collections.Generic;

namespace Experiments.Scripts.CarSpawn
{
    class TrackedEntityQueue : Queue<Entity>
    {
        private const int MAX_ENTITIES = 100;

        public new void Enqueue(Entity entity)
        {
            while (Count >= MAX_ENTITIES)
            {
                Dequeue();
            }
            base.Enqueue(entity);
        }

        public new Entity Dequeue()
        {
            var entity = base.Dequeue();
            entity.Delete();
            return entity;
        }
    }
}
