using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class Subtitle : MonoBehaviour
    {
        public Camera playerCamera;
        public bool billboard = true;

        // Для сохранения оригинального поворота если нужно
        private Vector3 originalPosition;
        private Quaternion originalRotation;

        void Start()
        {
            // Сохраняем оригинальные трансформы
            originalPosition = transform.position;
            originalRotation = transform.rotation;

            // Автопоиск камеры если не назначена
            if (playerCamera == null)
                playerCamera = Camera.main;
        }

        void LateUpdate()
        {
            if (billboard && playerCamera != null)
            {
                // Вариант 1: Простой поворот к камере
                transform.LookAt(transform.position + playerCamera.transform.forward);

                // Вариант 2: Более точный поворот
                // transform.LookAt(playerCamera.transform);
                // transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.transform.position);
            }
        }
    }
}
