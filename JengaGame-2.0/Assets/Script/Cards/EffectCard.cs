using Effects;
using Gameplay;
using GameRoot;

namespace Cards
{
    public class EffectCard<T> : Card 
        where T : BaseEffect
    {
        private T _effect;

        public EffectCard(T effect, BlockRegistry blockRegistry)
        {
            _effect = effect;
            _effect.InitEffect(blockRegistry);
        }

        public override string GetDescription(Localization localization)
        {
            var key = typeof(T).Name.Replace("Effect", "Card");
            return localization.GetText(key);
        }

        public override void PlayEffect()
        {
            _effect?.Execute();
        }
    }
}