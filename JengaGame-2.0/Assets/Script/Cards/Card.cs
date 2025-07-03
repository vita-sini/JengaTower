using GameRoot;

namespace Cards
{
    public abstract class Card
    {
        public abstract string GetDescription(Localization localization);

        public abstract void PlayEffect();
    }
}
