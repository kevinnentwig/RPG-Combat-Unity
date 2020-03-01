using UnityEngine;
using RPG.Stats;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        // DEBUG - REMOVE
        private void Update() 
        {
            if (gameObject.tag == "Player")
            {
                print(GetLevel());
            }
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;
            
            float currentExperience = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);

            for (int level = 1; level <= penultimateLevel; level++)
            {
                float ExperienceToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (ExperienceToLevelUp > currentExperience)
                {
                    return level;
                }
            }
            return penultimateLevel + 1;
        }
    }
}