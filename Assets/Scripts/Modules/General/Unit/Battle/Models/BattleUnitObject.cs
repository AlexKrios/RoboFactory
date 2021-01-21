using System;
using System.Collections.Generic;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Unit.Models.Object;
using UnityEngine;

namespace Modules.General.Unit.Battle.Models
{
    public class BattleUnitObject
    {
        public Action OnSetAttack { get; set; }
        public Action OnSetHealth { get; set; }
        public Action OnSetDefense { get; set; }
        public Action OnSetInitiative { get; set; }

        public BattleUnitTeamType Team { get; set; }
        public Dictionary<SpecType, int> CurrentSpecification { get; set; }
        
        public bool IsAlive { get; private set; }
        public bool IsEnded { get; set; }
        
        public int Place { get; set; }

        public GameObject Model { get; set; }

        public UnitObject Info { get; set; }

        public int CurrentAttack
        {
            get => CurrentSpecification[SpecType.Attack];
            set
            {
                CurrentSpecification[SpecType.Attack] += value;
                OnSetAttack?.Invoke();
            }
        }
        
        public int CurrentHealth
        {
            get => CurrentSpecification[SpecType.Health];
            set
            {
                CurrentSpecification[SpecType.Health] = value;
                
                if (CurrentHealth > Info.Health)
                    CurrentSpecification[SpecType.Health] = Info.Health;

                if (CurrentHealth <= 0)
                {
                    CurrentSpecification[SpecType.Health] = 0;
                    IsAlive = false;
                }

                OnSetHealth?.Invoke();
            }
        }
        public int CurrentDefense
        {
            get => CurrentSpecification[SpecType.Defense];
            set
            {
                CurrentSpecification[SpecType.Defense] += value;
                OnSetDefense?.Invoke();
            }
        }
        public int CurrentInitiative
        {
            get => CurrentSpecification[SpecType.Initiative];
            set
            {
                CurrentSpecification[SpecType.Initiative] += value;
                OnSetInitiative?.Invoke();
            }
        }

        public BattleUnitObject()
        {
            IsAlive = true;
        }
    }
}