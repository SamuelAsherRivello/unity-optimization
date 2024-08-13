// Render Leaf Map
// Aliyeredon@gmail.com
// Originally written in 2024

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ALIyerEdon
{
    public enum LM_Resolution
    {
        _256, _512, _1024, _2048, _4096
    }
    public class BillboardCaptureTool : EditorWindow
    {
        public LM_Resolution lmResolution = LM_Resolution._2048;
        public Camera targetCamera;
        public string targetTag = "Tree";
        public string path = "C:\\";
        public Color AmbientColor = Color.white;

        [MenuItem("Window/Billboard Capture Tool")]
        static void Init()
        {
            // Display window
            BillboardCaptureTool window = (BillboardCaptureTool)EditorWindow.GetWindow(typeof(BillboardCaptureTool));
            window.Show();
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        }
        public void Awake()
        {
            AmbientColor = RenderSettings.ambientLight;
        }

        void OnGUI()
        {
            // Top label ( window title and version name)
            GUILayout.Label("Billboard Capture Tool - Update 1.0 March 2024", EditorStyles.helpBox);

            EditorGUILayout.Space();

            EditorGUILayout.Space();

            targetCamera = EditorGUILayout.ObjectField("Target Camera", targetCamera, typeof(Camera), true) as Camera;

            EditorGUILayout.Space();

            lmResolution = (LM_Resolution)EditorGUILayout.EnumPopup("Resolution", lmResolution, GUILayout.Width(343));

            EditorGUILayout.Space();

            targetTag = EditorGUILayout.TextField("Render Layer", targetTag);

            EditorGUILayout.Space();

            var AmbientColorRef = AmbientColor;
            AmbientColor = EditorGUILayout.ColorField("Ambient Color", AmbientColor);
            if (AmbientColorRef != AmbientColor)
                RenderSettings.ambientLight = AmbientColor;

            /* EditorGUILayout.BeginHorizontal();

             path = EditorGUILayout.TextField("Save Path", path);


             EditorGUILayout.EndHorizontal();
            */

            EditorGUILayout.Space();

            if (GUILayout.Button("Render"))
            {

                targetCamera.cullingMask = LayerMask.GetMask(targetTag);

                string path = EditorUtility.SaveFilePanelInProject("Save As ...", "Billboard_"
                    + (UnityEditor.EditorPrefs.GetInt("BL_Number").ToString())
                    , "png", "");


                if (lmResolution == LM_Resolution._256)
                    Render_Billboard(targetCamera, 256, 256, path);
                if (lmResolution == LM_Resolution._512)
                    Render_Billboard(targetCamera, 512, 512, path);
                if (lmResolution == LM_Resolution._1024)
                    Render_Billboard(targetCamera, 1024, 1024, path);
                if (lmResolution == LM_Resolution._2048)
                    Render_Billboard(targetCamera, 2048, 2048, path);
                if (lmResolution == LM_Resolution._4096)
                    Render_Billboard(targetCamera, 4096, 4096, path);

                UnityEditor.EditorPrefs.SetInt("BL_Number", UnityEditor.EditorPrefs.GetInt("BL_Number") + 1);

                targetCamera.cullingMask = ~(0);

                AssetDatabase.Refresh();
            }

        }

        // CaptureScreenshot(Render_Billboard) is based on Brad Nelson's MIT-licensed AnimationToPng: http://wiki.unity3d.com/index.php/AnimationToPNG
        void Render_Billboard(Camera cam, int width, int height, string screengrabfile_path)
        {
            // This is slower, but seems more reliable.
            var bak_cam_targetTexture = cam.targetTexture;
            var bak_cam_clearFlags = cam.clearFlags;
            var bak_RenderTexture_active = RenderTexture.active;

            var tex_white = new Texture2D(width, height, TextureFormat.ARGB32, false);
            var tex_black = new Texture2D(width, height, TextureFormat.ARGB32, false);
            var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
            // Must use 24-bit depth buffer to be able to fill background.
            var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
            var grab_area = new Rect(0, 0, width, height);

            RenderTexture.active = render_texture;
            cam.targetTexture = render_texture;
            cam.clearFlags = CameraClearFlags.SolidColor;

            cam.backgroundColor = Color.black;
            cam.Render();
            tex_black.ReadPixels(grab_area, 0, 0);
            tex_black.Apply();

            cam.backgroundColor = Color.white;
            cam.Render();
            tex_white.ReadPixels(grab_area, 0, 0);
            tex_white.Apply();

            // Create Alpha from the difference between black and white camera renders
            for (int y = 0; y < tex_transparent.height; ++y)
            {
                for (int x = 0; x < tex_transparent.width; ++x)
                {
                    float alpha = tex_white.GetPixel(x, y).r - tex_black.GetPixel(x, y).r;
                    alpha = 1.0f - alpha;
                    Color color;
                    if (alpha == 0)
                    {
                        color = Color.clear;
                    }
                    else
                    {
                        color = tex_black.GetPixel(x, y) / alpha;
                    }
                    color.a = alpha;
                    tex_transparent.SetPixel(x, y, color);
                }
            }

            // Encode the resulting output texture to a byte array then write to the file
            byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
            File.WriteAllBytes(screengrabfile_path, pngShot);

            cam.clearFlags = bak_cam_clearFlags;
            cam.targetTexture = bak_cam_targetTexture;
            RenderTexture.active = bak_RenderTexture_active;
            RenderTexture.ReleaseTemporary(render_texture);

            Texture2D.DestroyImmediate(tex_black);
            Texture2D.DestroyImmediate(tex_white);
            Texture2D.DestroyImmediate(tex_transparent);
        }

        void Simple_Render_Billboard(Camera cam, int width, int height, string screengrabfile_path)
        {
            // Depending on your render pipeline, this may not work.
            var bak_cam_targetTexture = cam.targetTexture;
            var bak_cam_clearFlags = cam.clearFlags;
            var bak_RenderTexture_active = RenderTexture.active;

            var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
            // Must use 24-bit depth buffer to be able to fill background.
            var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
            var grab_area = new Rect(0, 0, width, height);

            RenderTexture.active = render_texture;
            cam.targetTexture = render_texture;
            cam.clearFlags = CameraClearFlags.SolidColor;

            // Simple: use a clear background
            cam.backgroundColor = Color.clear;
            cam.Render();
            tex_transparent.ReadPixels(grab_area, 0, 0);
            tex_transparent.Apply();

            // Encode the resulting output texture to a byte array then write to the file
            byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
            File.WriteAllBytes(screengrabfile_path, pngShot);

            cam.clearFlags = bak_cam_clearFlags;
            cam.targetTexture = bak_cam_targetTexture;
            RenderTexture.active = bak_RenderTexture_active;
            RenderTexture.ReleaseTemporary(render_texture);

            Texture2D.DestroyImmediate(tex_transparent);
        }

    }
}
#endif