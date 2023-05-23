﻿using System.Collections.Generic;
using Components.Scripts.Modules.Factory.Cameras;
using Components.Scripts.Modules.Factory.Menu;
using Components.Scripts.Modules.General.Ui;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Components.Scripts.Modules.Factory.Building.Floors
{
    public class BuildingFloorBase : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly FactoryCameraController _factoryCameraController;
        [Inject] private readonly FactoryUi _factoryUi;

        #endregion

        #region Components

        [SerializeField] private int floorNumber;
        [SerializeField] private List<UiType> menuButtons;
        
        [Header("Links")]
        [SerializeField] private Transform cameraPoint;

        [Header("Walls")]
        [SerializeField, UsedImplicitly] private MeshRenderer ground;
        [SerializeField, UsedImplicitly] private MeshRenderer roof;
        [SerializeField, UsedImplicitly] private List<MeshRenderer> leftWall;
        [SerializeField, UsedImplicitly] private List<MeshRenderer> rightWall;
        [SerializeField, UsedImplicitly] private List<MeshRenderer> frontWall;
        [SerializeField, UsedImplicitly] private List<MeshRenderer> backWall;

        [Header("Columns")]
        [SerializeField, UsedImplicitly] private MeshRenderer columnFrontLeft;
        [SerializeField, UsedImplicitly] private MeshRenderer columnFrontRight;
        [SerializeField, UsedImplicitly] private MeshRenderer columnBackLeft;
        [SerializeField, UsedImplicitly] private MeshRenderer columnBackRight;
        
        public int FloorNumber => floorNumber;
        public Transform CameraPoint => cameraPoint;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _factoryCameraController.FloorsDictionary.Add(floorNumber, this);
        }

        #endregion

        public void SetFloorActive()
        {
            SetFloorVisible(true);

            leftWall.ForEach(x => x.shadowCastingMode = ShadowCastingMode.ShadowsOnly);
            frontWall.ForEach(x => x.shadowCastingMode = ShadowCastingMode.ShadowsOnly);
            if (roof != null)
                roof.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            if (columnFrontLeft != null)
                columnFrontLeft.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
        }

        public void SetFloorVisible(bool value)
        {
            gameObject.SetActive(value);
            
            leftWall.ForEach(x => x.shadowCastingMode = ShadowCastingMode.On);
            frontWall.ForEach(x => x.shadowCastingMode = ShadowCastingMode.On);
            if (roof != null)
                roof.shadowCastingMode = ShadowCastingMode.On;
            if (columnFrontLeft != null)
                columnFrontLeft.shadowCastingMode = ShadowCastingMode.On;
        }

        public void SetMenuButtonsActive()
        {
            _factoryUi.MenuButtons.SetMenuButtonsActive(false);
            _factoryUi.MenuButtons.SetMenuButtonsActive(true, menuButtons);
        }
    }
}