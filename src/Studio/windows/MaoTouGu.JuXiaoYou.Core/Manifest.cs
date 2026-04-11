// ----------------------------------------------------------
//            文件：Manifest.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 21:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Indexing;

namespace MaoTouGu.JuXiaoYou
{
    public class Manifest : PluginManifest
    {
        /*
         *
            9930a0058cbf4fbda6c678e64d8a8aaf
            98a4cab5a28b442a9d283067f748d90a
            fe0b5004e271457fb07f471238595694
            037c375045bd484c83dcb9903200eb54
            4a8705f20dc74b4da623d38a2e8367f6
            5ea23977483a4070a7dfb74d287972eb
            44120cd21fdf4fa3a9a19dd45c84180f
            c84fbd059efa438c81c14dcdef0ed771
            5d4c6bbd8a6748b0a6dd33476216945e
            7ee723567a0c4b19977b8913a4f8ba27
            9e79fe5c2fb3442bb4ad95ab25e201d9
            0e2763927eb8452ead3d736350e0b5f6
            c192e079017d4b458fcb5e03c0a885c2
            a1fc9473f21d4446a4a98a0ce2c3acd9
            ffb0323d5b95441d81fcfe8be0d918f9
            914bfcbec1634383821fc54800f18828
            9ca3b07278ed49d09acf12984597cbef
            923d4739b02849c684442d0940532b46
            a7755a40d05e44049456727ab5a1240c

         */


        public const string Indexing = "393833c7f51c4fd5be6fe53405d91491";

        public override void RegisterVisualManagers()
        {
        }

        public override void RegisterFeatures()
        {
            // FeatureManager.UsePage<PrototypeEditorViewModel>(Feature_Prototype, "角色");
            // FeatureManager.UsePage<CardEditorViewModel>(Feature_Card, "新建角色");
            // FeatureManager.UsePage<DeckEditorViewModel>(Feature_Deck, "系列");

            // FeatureManager.UseExternalNavigation<IndexingNavigator>(Indexing, "分类系统");
        }
    }
}