using Denchik.Weapon.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Denchik.Weapon
{
    [CustomEditor(typeof(WeaponData))]
    public class WeaponDataEditor : Editor
    {
        private static List<Type> _dataComponentTypes = new List<Type>();

        private WeaponData _weaponData;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach(Type dataComponent in _dataComponentTypes)
            {
                if (GUILayout.Button(dataComponent.Name))
                {
                    var component = Activator.CreateInstance(dataComponent) as ComponentData;

                    if(component == null)
                    {
                        return;
                    }

                    _weaponData.AddData(component);
                }
            }
        }

        [DidReloadScripts]
        private static void OnRecompile()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            IEnumerable<Type> types = assemblies.SelectMany(assembly => assembly.GetTypes());
            IEnumerable<Type> filteredTypes = types.Where(type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass);

            _dataComponentTypes = filteredTypes.ToList();
        }

        private void OnEnable()
        {
            _weaponData = target as WeaponData;
        }
    }
}
