using Denchik.Weapon.Components.ComponentData;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class WeaponSprite : WeaponComponent
    {
        private SpriteRenderer _mainHeroSpriteRenderer;
        private SpriteRenderer _weaponSpriteRenderer;

        private WeaponSpriteData _weaponSpriteData;

        private int _currentWeaponSpriteIndex;

        protected override void Awake()
        {
            base.Awake();

            _mainHeroSpriteRenderer = transform.Find("MainHeroMotion").GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();

            _weaponSpriteData = weapon.WeaponData.GetData<WeaponSpriteData>();
            //TODO: fix this
            //_mainHeroSpriteRenderer = weapon.MainHeroGameObject.GetComponent<SpriteRenderer>();
            //_weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
        }

        private void HandleMainHeroSpriteChange(SpriteRenderer spriteRenderer)
        {
            if (!isAttackActive)
            {
                _weaponSpriteRenderer.sprite = null;
                return;
            }

            Sprite[] currentAttackSprites = _weaponSpriteData.AttackData[weapon.CurrentAttackCounter].Sprites;

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

        protected override void OnEnable()
        {
            base.OnEnable();

            _mainHeroSpriteRenderer.RegisterSpriteChangeCallback(HandleMainHeroSpriteChange);
            weapon.OnEnter += HandleEnter;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _mainHeroSpriteRenderer.RegisterSpriteChangeCallback(HandleMainHeroSpriteChange);
            weapon.OnEnter -= HandleEnter;
        }
    }
}
