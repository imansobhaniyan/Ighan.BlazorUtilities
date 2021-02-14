using Microsoft.JSInterop;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ighan.BlazorUtilities.Utilities
{
    public class LocalStorageHelper
    {
        private readonly IJSRuntime jsRuntime;

        public LocalStorageHelper(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task SetAsync(string key, string value)
        {
            await jsRuntime.InvokeVoidAsync("IghanSetLocalStorage", key, value);
        }

        public async Task<string> GetAsync(string key)
        {
            return await jsRuntime.InvokeAsync<string>("IghanGetLocalStorage", key);
        }

        public async Task RemoveAsync(string key)
        {
            await jsRuntime.InvokeVoidAsync("IghanRemoveLocalStorage", key);
        }
    }
}
