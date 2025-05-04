using Denchik.Weapon.Components;
using System;
using System.Collections.Generic;
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

        private bool _showForceUpdateButtons;
        private bool _showAddComponentButtons;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set number of attacks"))
            {
                foreach (ComponentData data in _weaponData.ComponentData)
                {
                    data.InitializeAttackData(_weaponData.NumberOfAttacks);
                }
            }

            _showAddComponentButtons = EditorGUILayout.Foldout(_showAddComponentButtons, "Add components");

            if (_showAddComponentButtons)
            {
                foreach (Type dataComponent in _dataComponentTypes)
                {
                    if (GUILayout.Button(dataComponent.Name))
                    {
                        var component = Activator.CreateInstance(dataComponent) as ComponentData;

                        if (component == null)
                        {
                            return;
                        }

                        component.InitializeAttackData(_weaponData.NumberOfAttacks);

                        _weaponData.AddData(component);
                    }
                }
            }

            _showForceUpdateButtons = EditorGUILayout.Foldout(_showForceUpdateButtons, "Update buttons");

            if (_showForceUpdateButtons)
            {
                if (GUILayout.Button("Update component names"))
                {
                    foreach (ComponentData data in _weaponData.ComponentData)
                    {
                        data.SetComponentName();
                    }
                }

                if (GUILayout.Button("Update attack names"))
                {
                    foreach (ComponentData data in _weaponData.ComponentData)
                    {
                        data.SetAttackDataNames();
                    }
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
