using Effects;
using Gameplay;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Cards
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private BlockRegistry _blockRegistry;
        [SerializeField] private CardUI _cardUI;
        [SerializeField] private float _probability;
        [SerializeField] private WindEffect _windEffect;
        [SerializeField] private EarthquakeEffect _earthquakeEffect;
        [SerializeField] private GlitchEffect _glitchEffect;
        [SerializeField] private RotatingPlatformEffect _rotatingPlatformEffect;
        [SerializeField] private MagneticEffect _magneticEffect;
        [SerializeField] private GhostEffect _ghostEffect;
        [SerializeField] private HeavyEffect _heavyEffect;
        [SerializeField] private SlipperyEffect _slipperyEffect;
        [SerializeField] private ExplosiveEffect _explosiveEffect;
        [SerializeField] private SmokeEffect _smokeEffect;

        private List<Card> _cards = new List<Card>();
        private Random _random = new Random();

        private void Awake()
        {
            InitializeDeck();
            ShuffleDeck();
        }

        public void OnTurnEnd()
        {
            float chance = UnityEngine.Random.Range(0f, 1f);

            if (chance <= _probability)
                DrawCard();
        }

        private void InitializeDeck()
        {
            _cards.Add(new EffectCard<SmokeEffect>(_smokeEffect, _blockRegistry));
            _cards.Add(new EffectCard<WindEffect>(_windEffect, _blockRegistry));
            _cards.Add(new EffectCard<GhostEffect>(_ghostEffect, _blockRegistry));
            _cards.Add(new EffectCard<RotatingPlatformEffect>(_rotatingPlatformEffect, _blockRegistry));
            _cards.Add(new EffectCard<EarthquakeEffect>(_earthquakeEffect, _blockRegistry));
            _cards.Add(new EffectCard<GlitchEffect>(_glitchEffect, _blockRegistry));
            _cards.Add(new EffectCard<MagneticEffect>(_magneticEffect, _blockRegistry));
            _cards.Add(new EffectCard<HeavyEffect>(_heavyEffect, _blockRegistry));
            _cards.Add(new EffectCard<SlipperyEffect>(_slipperyEffect, _blockRegistry));
            _cards.Add(new EffectCard<ExplosiveEffect>(_explosiveEffect, _blockRegistry));
        }

        private void ShuffleDeck()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                int randomPosition = _random.Next(_cards.Count);
                Card temporary = _cards[randomPosition];
                _cards[randomPosition] = _cards[i];
                _cards[i] = temporary;
            }
        }

        private void DrawCard()
        {
            if (_cards.Count > 0)
            {
                Card drawnCard = _cards[0];
                _cards.RemoveAt(0);

                _cardUI.ShowCard(drawnCard);
                drawnCard.PlayEffect();
                _cards.Add(drawnCard);
                ShuffleDeck();
            }
        }
    }
}
