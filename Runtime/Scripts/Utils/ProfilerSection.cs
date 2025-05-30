using System;
using UnityEngine.Profiling;

namespace UniCorn.Utils
{
    public class ProfilerSection : IDisposable
    {
        public ProfilerSection(object objectBeingTracked) : this(objectBeingTracked.GetType().ToString())
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
