using BepInEx.Logging;

namespace DystopiaPolyScript;

public static class Main
{
    public static void Load(ManualLogSource logger)
    {
        logger.LogMessage("Here we go!");
    }
}
