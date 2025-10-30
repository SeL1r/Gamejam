using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class Subtitle : MonoBehaviour
    {
        private int _index = 0;
        public Camera playerCamera;
        public bool billboard = true;
        private TextMeshPro sub;

        private InputAction nextSub;

        private Vector3 originalPosition;
        private Quaternion originalRotation;

        void Start()
        {
            nextSub = InputSystem.actions.FindAction("NextSubtitle");
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            sub = GetComponent<TextMeshPro>();
            
            if (playerCamera == null)
                playerCamera = Camera.main;
        }

        void LateUpdate()
        {
            if (billboard && playerCamera != null)
            { 
                transform.LookAt(transform.position + playerCamera.transform.forward);

            }
        }
    }
}
