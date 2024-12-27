using UnityEngine;

namespace UniCorn.Utils
{
    public static class MaterialExtension
    {
        private static readonly int _baseMap = Shader.PropertyToID("_BaseMap");

        public static Texture GetDiffuse(this Material material)
        {
            return material.GetTexture(_baseMap);
        }

        public static void SetDiffuse(this Material material, Texture texture)
        {
            material.SetTexture(_baseMap, texture);
        }
    }
}
