using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class RainController : MonoBehaviour
    {
        [Header("Rain Settings")]
        public ParticleSystem rainParticleSystem;
        public float rainIntensity = 1f;

        void Start()
        {
            SetupRainParticles();
        }

        void SetupRainParticles()
        {
            var main = rainParticleSystem.main;
            var emission = rainParticleSystem.emission;
            var shape = rainParticleSystem.shape;

            // Основные настройки
            main.startSpeed = 20f;
            main.startLifetime = 2f;
            main.startSize = 0.05f;
            main.gravityModifier = 0.5f;
            main.maxParticles = 10000;

            // Форма эмиттера (плоскость над камерой/игроком)
            shape.shapeType = ParticleSystemShapeType.Box;
            shape.scale = new Vector3(50f, 1f, 50f);

            // Эмиссия
            emission.rateOverTime = 1000 * rainIntensity;
        }

        public void SetRainIntensity(float intensity)
        {
            rainIntensity = Mathf.Clamp01(intensity);
            var emission = rainParticleSystem.emission;
            emission.rateOverTime = 1000 * rainIntensity;
        }
    }
}
