using BepInEx.Logging;

namespace DystopiaPolyScript;

public static class Main
{
    public static void Load(ManualLogSource logger)
    {
        BuildConfigHelper.GetSelectedBuildConfig().buildServerURL = BuildServerURL.Custom;
        BuildConfigHelper.GetSelectedBuildConfig().customServerURL = "http://localhost:5051";
    }
}
