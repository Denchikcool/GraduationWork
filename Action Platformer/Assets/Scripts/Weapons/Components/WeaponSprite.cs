using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class WeaponSprite : WeaponComponent
    {
        [SerializeField]
        private WeaponSprites[] _weaponSprites;

        private SpriteRenderer _mainHeroSpriteRenderer;
        private SpriteRenderer _weaponSpriteRenderer;

        private int _currentWeaponSpriteIndex;

        protected override void Awake()
        {
            base.Awake();

            _mainHeroSpriteRenderer = transform.Find("MainHeroMotion").GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();
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

            Sprite[] currentAttackSprites = _weaponSprites[weapon.CurrentAttackCounter].Sprites;

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

    [Serializable]
    public class WeaponSprites
    {
        [field: SerializeField]
        public Sprite[] Sprites { get; private set; }
    }
}
