using Denchik.Weapon.Components;
using System.Linq;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
    {
        private SpriteRenderer _mainHeroSpriteRenderer;
        private SpriteRenderer _weaponSpriteRenderer;

        private Sprite[] _currentPhaseSprites;

        private int _currentWeaponSpriteIndex;

        protected override void Start()
        {
            base.Start();

            _mainHeroSpriteRenderer = weapon.MainHeroGameObject.GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();

            data = weapon.WeaponData.GetData<WeaponSpriteData>();

            _mainHeroSpriteRenderer.RegisterSpriteChangeCallback(HandleMainHeroSpriteChange);

            eventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
        }

        private void HandleEnterAttackPhase(AttackPhases attackPhases)
        {
            _currentWeaponSpriteIndex = 0;

            _currentPhaseSprites = currentAttackData.PhaseSprites.FirstOrDefault(data => data.Phases == attackPhases).Sprites;
        }

        private void HandleMainHeroSpriteChange(SpriteRenderer spriteRenderer)
        {
            if (!isAttackActive)
            {
                _weaponSpriteRenderer.sprite = null;
                return;
            }

            if(_currentWeaponSpriteIndex >= _currentPhaseSprites.Length)
            {
                print($"{weapon.name} weapon sprites length mismatch");
                return;
            }

            _weaponSpriteRenderer.sprite = _currentPhaseSprites[_currentWeaponSpriteIndex];

            _currentWeaponSpriteIndex++;
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();

            _currentWeaponSpriteIndex = 0;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _mainHeroSpriteRenderer.RegisterSpriteChangeCallback(HandleMainHeroSpriteChange);

            eventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;
        }
    }
}
