using System;
using System.Runtime.CompilerServices;
using UnityEngine.Profiling;

namespace UniCorn.Utils
{
    public class ProfilerSection : IDisposable
    {
        public ProfilerSection([CallerFilePath] string callerPath = "", [CallerLineNumber] int lineNumber = 0)
            : this($"{callerPath} Line {lineNumber}")
        {
        }

        public ProfilerSection(string sectionName)
        {
            Profiler.BeginSample(sectionName);
        }

        public void Dispose()
        {
            Profiler.EndSample();
        }
    }
}
