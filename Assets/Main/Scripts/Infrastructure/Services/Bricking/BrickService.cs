using Main.Scripts.Logic.Swipe;

namespace Main.Scripts.Infrastructure.Services.Bricking
{
    public class BrickService : IBrickService
    {
        private readonly ISwiper _swiper;

        public BrickService(ISwiper swiper)
        {
            _swiper = swiper;
        }

        public void Brick()
        {
            _swiper.Block();
        }
    }
}