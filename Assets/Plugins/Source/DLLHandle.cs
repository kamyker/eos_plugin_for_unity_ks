﻿//#define ENABLE_DLLHANDLE_PRINT

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class DLLHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    [Conditional("ENABLE_DLLHANDLE_PRINT")]
    private static void print(string toPrint)
    {
        UnityEngine.Debug.Log(toPrint);
    }

    private static string GetPackageName()
    {
        return "com.playeveryware.eos";
    }

    public static string GetPathToPlugins()
    {
        string pluginsPath = (Application.dataPath + "\\Plugins\\").Replace('/', '\\');
        string packagedPluginPath = Path.GetFullPath("Packages/" + GetPackageName());

        if(Directory.Exists(pluginsPath))
        {
            return pluginsPath;
        }
        return packagedPluginPath;
    }

    public static string GetVersionForLibrary(string libraryName)
    {
        string path = GetPathToPlugins();
        string ext = ".dll";
        print("looking in path: " + path);

        //TODO: change it to take the platform into consideration
        //TODO: probably make this more generic?
        foreach (var filesystemEntry in Directory.EnumerateFileSystemEntries(path, libraryName + ext, SearchOption.AllDirectories))
        {
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(filesystemEntry);
            print("Found : " + filesystemEntry);
            if (info != null)
            {
                return string.Format("{0}.{1}.{2}", info.FileMajorPart, info.FileMinorPart, info.FileBuildPart);
            }
        }
        return null;
    }

    public static DLLHandle LoadDynamicLibrary(string libraryName)
    {
        print("Loading Library " + libraryName);
        string path = GetPathToPlugins();
        string ext =
#if UNITY_ANDROID
            ".so";
#else
            ".dll";
#endif

        //TODO: change it to take the platform into consideration
        //TODO: probably make this more generic?
        foreach (var filesystemEntry in Directory.EnumerateFileSystemEntries(path, libraryName + ext, SearchOption.AllDirectories))
        {
            IntPtr libraryHandle = SystemDynamicLibrary.LoadLibrary(filesystemEntry);
            print("Trying to load with entry " + filesystemEntry);

            if (libraryHandle != IntPtr.Zero)
            {
                print("found library");
                return new DLLHandle(libraryHandle);
            }
            else
            {
                throw new System.ComponentModel.Win32Exception();
            }
        }
        print("Library not found");
        return null;
    }

    public DLLHandle(IntPtr intPtr, bool value = true) : base(intPtr, true)
    {
        print("Creating a dll handle");
        SetHandle(intPtr);
    }

    protected override bool ReleaseHandle()
    {
        if(handle == IntPtr.Zero)
        {
            return true;
        }
        bool didUnload = true;
#if !UNITY_EDITOR
        didUnload = SystemDynamicLibrary.FreeLibrary(handle);
        print("Unloading a Dll with result : " + didUnload);
#endif
        SetHandle(IntPtr.Zero);
        return didUnload;
    }


    public Delegate LoadFunctionAsDelegate(Type functionType, string functionName)
    {
        return LoadFunctionAsDelegate(handle, functionType, functionName);
    }

    public System.IntPtr LoadFunctionAsIntPtr(string functionName)
    {
        IntPtr functionPointer = SystemDynamicLibrary.GetProcAddress(handle, functionName);
        return functionPointer;
    }

    public void ConfigureFromLibraryDelegateFieldOnClassWithFunctionName(Type clazz, Type delegateType, string functionName)
    {
        ConfigureFromLibraryDelegateFieldOnClassWithFunctionName(handle, clazz, delegateType, functionName);
    }

    // TODO better name
    private static void ConfigureFromLibraryDelegateFieldOnClassWithFunctionName(IntPtr libraryHandle, Type clazz, Type delegateType, string functionName)
    {
        var aDelegate = LoadFunctionAsDelegate(libraryHandle, delegateType, functionName);
        //print("Delegate found is : " + aDelegate);
        var field = clazz.GetField(functionName);
        field.SetValue(null, aDelegate);
    }

    //-------------------------------------------------------------------------
    static public Delegate LoadFunctionAsDelegate(IntPtr libraryHandle, Type functionType, string functionName)
    {
        print("Attempt to load " + functionName);
        if (libraryHandle == IntPtr.Zero)
        {
            throw new Exception("libraryHandle is null");
        }
        if (functionType == null)
        {
            throw new Exception("null function type?");
        }

        IntPtr functionPointer = SystemDynamicLibrary.GetProcAddress(libraryHandle, functionName);
        if (functionPointer == IntPtr.Zero)
        {
            throw new System.ComponentModel.Win32Exception();
        }

        return Marshal.GetDelegateForFunctionPointer(functionPointer, functionType);
    }
}