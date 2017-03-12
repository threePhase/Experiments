using GTA;
using System.Collections.Generic;

namespace Experiments.Scripts.CarSpawn
{
    class TrackedEntityQueue : Queue<Entity>
    {
        // TODO: this is probably no longer necessary
        private const int MAX_ENTITIES = 100;
        private int _oldCount = 0;

        public new void Enqueue(Entity entity)
        {
            while (Count > 0 &&
                (Count >= MAX_ENTITIES || Count == _oldCount))
            {
                _oldCount = Count;
                Dequeue();
            }
            base.Enqueue(entity);
            _oldCount = Count;
        }

        public new Entity Dequeue()
        {
            var entity = base.Dequeue();
            _oldCount = Count;
            entity.Delete();
            return entity;
        }
    }
}
