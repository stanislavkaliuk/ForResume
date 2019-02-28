using System;
using UnityEditor;
#if UNITY_EDITOR
[Serializable]
public struct ProjectData
{
    public string AndroidPackageName;
    public string KeyStorePassword;
    public string KeyAliasPassword;
    public TargetArchitecture AndroidArchitecture;
    
    public string IOSPackageName;

    public string ProductName;
    public string VersionTemplate;

    public NetVersion ScriptingRuntimeVersion;
    public ScriptingBackend BackendVersion;
}

public enum ScriptingBackend
{
    Mono = ScriptingImplementation.Mono2x, IL2CPP = ScriptingImplementation.IL2CPP
}

public enum NetVersion
{
    Legacy = ScriptingRuntimeVersion.Legacy,
    Latest = ScriptingRuntimeVersion.Latest,
}

public enum TargetArchitecture : uint
{
    None = AndroidArchitecture.None,
    Common = AndroidArchitecture.ARMv7 | AndroidArchitecture.X86,
    ARMv7 = AndroidArchitecture.ARMv7,
    ARM64 = AndroidArchitecture.ARM64,
    x86 = AndroidArchitecture.X86
}
#endif