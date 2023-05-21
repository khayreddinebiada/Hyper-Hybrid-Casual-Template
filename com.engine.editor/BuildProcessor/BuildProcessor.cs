using UnityEditor.Build.Reporting;
using System.Diagnostics.Contracts;
using UnityEditor.Build;
using Engine.Linq;
using Editor;
using Engine;

class BuildProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report)
    {
        AssetsSettings settings = AssetHelper.FindScribtableObjectOfType<AssetsSettings>();

        Contract.Requires(!settings.IsNull());

        IBuild[] builds = FindHelper.FindAllAssetsOfType<IBuild>();

        foreach (IBuild build in builds)
        {
            build?.OnBuild();
        }

        if (settings.ResetData)
        {
            HeadTemplateEditor.ResetAllData();
        }

        if (settings.Validate)
        {
            HeadTemplateEditor.ValidateAll();
        }

        if (settings.SaveAssets)
        {
            HeadTemplateEditor.SaveAssets();
        }
    }
}
