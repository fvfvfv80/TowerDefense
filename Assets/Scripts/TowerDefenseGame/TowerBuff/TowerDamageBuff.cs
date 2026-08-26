using System.Collections.Generic;

namespace Assets.Scripts.TowerDefenseGame.TowerBuff
{
    public interface ITowerBuffStack { float Multiple { get; } }

    public interface ITowerBuff { }

    public class TowerDamageBuffStack : ITowerBuffStack
    {
        private class BuffEntry
        {
            public TowerDamageBuff buff;
            public int count;

            public BuffEntry(TowerDamageBuff buff)
            {
                this.buff = buff;
                count = 1;
            }
        }

        private readonly Dictionary<int, BuffEntry> _entries = new();

        //그리고 반환될 정보들 지금은 간단하게 곱셈부터시작
        public float Multiple { get; private set; } = 0f;

        public TowerDamageBuffStack()
        {

        }

        public void AddBuff(TowerDamageBuff buff)
        {
            if (_entries.TryGetValue(buff.level, out var entry))
            {
                entry.count++;
            }
            else
            {
                _entries.Add(buff.level, new BuffEntry(buff));
            }

            Refresh();
        }

        public void RemoveBuff(TowerDamageBuff buff)
        {
            if (!_entries.TryGetValue(buff.level, out var entry))
                return;

            entry.count--;

            if (entry.count <= 0)
            {
                _entries.Remove(buff.level);
            }

            Refresh();
        }

        private void Refresh()
        {
            var strongestLevel = -1;
            var multiple = 0f;

            foreach (var entry in _entries.Values)
            {
                if (entry.buff.level <= strongestLevel)
                    continue;

                strongestLevel = entry.buff.level;
                multiple = entry.buff.value;
            }

            Multiple = multiple;
        }

    }

    public struct TowerDamageBuff
    {
        public readonly int level;

        public readonly float value;

        public TowerDamageBuff(int level, float value)
        {
            this.level = level;
            this.value = value;
        }
    }

}
