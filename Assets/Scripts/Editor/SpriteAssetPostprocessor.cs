using UnityEditor;
using UnityEngine;

namespace Craftory.Editor
{
    public class SpriteAssetPostprocessor : AssetPostprocessor
    {
        private void OnPostprocessTexture(Texture2D texture)
        {
            string path = assetPath.ToLower();
            if (path.Contains("sprite/"))
            {
                TextureImporter importer = (TextureImporter) assetImporter;
                importer.textureType = TextureImporterType.Sprite;

                if (path.Contains("single"))
                {
                    importer.spriteImportMode = SpriteImportMode.Single;
                }
                else if (path.Contains("multiple"))
                {
                    importer.spriteImportMode = SpriteImportMode.Multiple;
                }
            }
        }
    }
}
