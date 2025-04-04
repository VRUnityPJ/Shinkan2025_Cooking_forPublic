using UnityEngine;

namespace Shinkan2025_Cooking.Scripts.VRM
{
    public class MToon2URPSwitcher : MonoBehaviour
    {
        public GameObject[] targetObjects;  // 対象となるオブジェクトの配列
        public Shader urpShader;  // URPへ切り替えるためのシェーダー

        private void Start()
        {
            // 開始時に全ターゲットにシェーダーを適用
            foreach (var target in targetObjects)
            {
                ApplyShader(target);
            }
        }

        private void ApplyShader(GameObject target)
        {
            // シェーダーが指定されていない場合のエラー処理
            if (urpShader == null)
            {
                Debug.LogError("URP shader is not specified.");
                return;
            }

            // ターゲットオブジェクトが指定されていない場合のエラー処理
            if (target == null)
            {
                Debug.LogError("Target object is not specified.");
                return;
            }

            Renderer[] childRenderers = target.GetComponentsInChildren<Renderer>();

            // 各レンダラーに対してマテリアルを変更
            foreach (Renderer renderer in childRenderers)
            {
                Material[] materials = renderer.materials;

                for (int i = 0; i < materials.Length; i++)
                {
                    Material originalMaterial = materials[i];
                    Material newMaterial = new Material(urpShader);

                    // レンダリングとライティングのプロパティをコピー
                    CopyRenderingProperties(originalMaterial, newMaterial);
                    CopyLightingProperties(originalMaterial, newMaterial);

                    materials[i] = newMaterial;
                }

                renderer.materials = materials;
            }
        }

        // レンダリングプロパティのコピー
        private void CopyRenderingProperties(Material original, Material destination)
        {
            CopyProperties(original, destination, "_AlphaMode", "_TransparentWithZWrite", "_Cutoff", "_RenderQueueOffset", "_DoubleSided");
        }

        // ライティングプロパティのコピー
        private void CopyLightingProperties(Material original, Material destination)
        {
            CopyProperties(original, destination, "_Color", "_MainTex", "_ShadeColor", "_ShadeTex", "_BumpMap", "_BumpScale");
        }

        // 指定されたプロパティ名でマテリアルのプロパティをコピー
        private void CopyProperties(Material original, Material destination, params string[] propertyNames)
        {
            foreach (var propName in propertyNames)
            {
                if (original.HasProperty(propName) && destination.HasProperty(propName))
                {
                    if (propName == "_MainTex" || propName == "_ShadeTex" || propName == "_BumpMap")
                    {
                        destination.SetTexture(propName, original.GetTexture(propName));
                    }
                    else if (propName == "_Color" || propName == "_ShadeColor")
                    {
                        destination.SetColor(propName, original.GetColor(propName));
                    }
                    else
                    {
                        destination.SetFloat(propName, original.GetFloat(propName));
                    }
                }
            }
        }
    }
}