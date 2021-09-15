﻿using LogSuite.Shared.Models.Operativka;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.Operativka
{
    public partial class GisEdit
    {
        [Parameter] public EventCallback OnValueSubmit { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        [CascadingParameter] public GisDTO GisModel { get; set; }

        private async Task Cancel()
        {
            await OnCancel.InvokeAsync();
        }
    }
}