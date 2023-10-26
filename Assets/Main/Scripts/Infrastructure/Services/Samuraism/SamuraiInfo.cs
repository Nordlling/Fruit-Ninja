namespace Main.Scripts.Infrastructure.Services.Samuraism
{
    public class SamuraiInfo
    {
        public int BlockCountMultiplier = 1;
        public float PackFrequencyMultiplier = 1;
        public float BlockFrequencyMultiplier = 1;
        public void ResetValues()
        {
            BlockCountMultiplier = 1;
            PackFrequencyMultiplier = 1;
            BlockFrequencyMultiplier = 1;
        }
    }
}