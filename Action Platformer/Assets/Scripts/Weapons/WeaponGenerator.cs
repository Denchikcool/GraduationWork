using Denchik.Weapon.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Denchik.Weapon
{
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField]
        private Weapon _weapon;
        [SerializeField]
        private WeaponData _weaponData;

        private List<WeaponComponent> _componentAlreadyOnWeapon = new List<WeaponComponent>();
        private List<WeaponComponent> _componentsAddedToWeapon = new List<WeaponComponent>();
        private List<Type> _componentDependencies = new List<Type>();

        private void Start()
        {
            GenerateWeapon(_weaponData);
        }

        public void GenerateWeapon(WeaponData dataWeapon)
        {
            _weapon.SetData(dataWeapon);

            _componentAlreadyOnWeapon.Clear();
            _componentsAddedToWeapon.Clear();
            _componentDependencies.Clear();

            _componentAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();
            _componentDependencies = dataWeapon.GetAllDependencies();

            foreach(Type dependency in _componentDependencies)
            {
                if(_componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                {
                    continue;
                }

                WeaponComponent weaponComponent = _componentAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

                if(weaponComponent == null)
                {
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
                }

                weaponComponent.Init();

                _componentsAddedToWeapon.Add(weaponComponent);
            }

            IEnumerable<WeaponComponent> componentsToRemove = _componentAlreadyOnWeapon.Except(_componentsAddedToWeapon);

            foreach(WeaponComponent weaponComponent in componentsToRemove)
            {
                Destroy(weaponComponent);
            }
        }
    }
}
