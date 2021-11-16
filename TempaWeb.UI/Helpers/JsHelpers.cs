using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempaWeb.Helpers
{
    public static class IJsHelpers
    {
        public static async ValueTask InitializeInactivityTimer<T>(this IJSRuntime js,
                DotNetObjectReference<T> dotNetObjectReference) where T : class
        {
            await js.InvokeVoidAsync("initializeInactivityTimer", dotNetObjectReference);
        }
    }
}
