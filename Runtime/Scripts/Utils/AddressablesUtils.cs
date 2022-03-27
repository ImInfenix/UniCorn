using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UniCorn.Utils
{
    public static class AddressablesUtils
    {
        public static bool TryGetResourceLocation(string key, Type type, out IList<IResourceLocation> resourcesLocations)
        {
            return TryGetResourceLocation(new[] {key}, Addressables.MergeMode.None, type, out resourcesLocations);
        }
        
        public static bool TryGetResourceLocation(IEnumerable<string> keys, Addressables.MergeMode mergeMode, Type type, out IList<IResourceLocation> resourcesLocations)
        {
            resourcesLocations = Addressables.LoadResourceLocationsAsync(keys, mergeMode, type).WaitForCompletion();
            return resourcesLocations.Count > 0;
        }
    }
}
