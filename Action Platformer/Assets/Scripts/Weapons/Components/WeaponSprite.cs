using Denchik.Weapon.Components;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
    {
        private SpriteRenderer _mainHeroSpriteRenderer;
        private SpriteRenderer _weaponSpriteRenderer;

        private int _currentWeaponSpriteIndex;

        protected override void Start()
        {
            base.Start();

            _mainHeroSpriteRenderer = weapon.MainHeroGameObject.GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();

            data = weapon.WeaponData.GetData<WeaponSpriteData>();

            _mainHeroSpriteRenderer.RegisterSpriteChangeCallback(HandleMainHeroSpriteChange);
        }

        private void HandleMainHeroSpriteChange(SpriteRenderer spriteRenderer)
        {
            if (!isAttackActive)
            {
                _weaponSpriteRenderer.sprite = null;
                return;
            }

            Sprite[] currentAttackSprites = currentAttackData.Sprites;

            if(_currentWeaponSpriteIndex >= currentAttackSprites.Length)
            {
                print($"{weapon.name} weapon sprites length mismatch");
                return;
            }

            _weaponSpriteRenderer.sprite = currentAttackSprites[_currentWeaponSpriteIndex];

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
        }
    }
}
