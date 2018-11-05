﻿using System;
using System.IO;
using Xunit;

namespace CoreHook.Tests
{
    public class RemoteInjectionTest64
    {
        [Fact]
        private void TestRemoteInject64()
        {
            const string TestHookLibrary = "CoreHook.Tests.SimpleHook1.dll";
            const string TestMessage = "Berner";

            var testProcess = Resources.StartProcess(Path.Combine(
                            Environment.ExpandEnvironmentVariables("%Windir%"),
                            "System32",
                            "notepad.exe"
                        ));

            Resources.InjectDllIntoTarget(testProcess,
               Resources.GetTestDllPath(
               TestHookLibrary
               ),
               Resources.GetUniquePipeName(),
               TestMessage);

            Assert.Equal(TestMessage, Resources.ReadFromProcess(testProcess));

            Resources.EndProcess(testProcess);
        }

        //[Fact]
        private void TestTargetAppRemoteInject()
        {
            const string TestHookLibrary = "CoreHook.Tests.SimpleHook1.dll";
            const string TestMessage = "Berner";

            Resources.InjectDllIntoTarget(Resources.TargetProcess,
               Resources.GetTestDllPath(
               TestHookLibrary
               ),
               Resources.GetUniquePipeName(),
               TestMessage);

            Assert.Equal(TestMessage, Resources.ReadFromProcess(Resources.TargetProcess));

            Resources.EndTargetAppProcess();
        }
    }

    public class RemoteInjectionTest32
    {
        [Fact]
        private void TestRemoteInject32()
        {
            const string TestHookLibrary = "CoreHook.Tests.SimpleHook1.dll";
            const string TestMessage = "Berner";

            var testProcess = Resources.StartProcess(Path.Combine(
                            Environment.ExpandEnvironmentVariables("%Windir%"),
                            "SysWOW64",
                            "notepad.exe"
                        ));

            Resources.InjectDllIntoTarget(testProcess,
               Resources.GetTestDllPath(
               TestHookLibrary
               ),
               Resources.GetUniquePipeName(),
               TestMessage);

            Assert.Equal(TestMessage, Resources.ReadFromProcess(testProcess));

            Resources.EndProcess(testProcess);
        }
    }
}
