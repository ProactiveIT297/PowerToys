﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using WindowsPackageManager.Interop;

namespace Microsoft.CmdPal.Ext.WinGet.WindowsPackageManager.Interop;

#nullable disable
internal sealed class ClassModel
{
    /// <summary>
    /// Gets the interface for the projected class type generated by CsWinRT
    /// </summary>
    public Type InterfaceType { get; init; }

    /// <summary>
    /// Gets the projected class type generated by CsWinRT
    /// </summary>
    public Type ProjectedClassType { get; init; }

    /// <summary>
    /// Gets the Clsids for each context (e.g. OutOfProcProd, OutOfProcDev)
    /// </summary>
    public IReadOnlyDictionary<ClsidContext, Guid> Clsids { get; init; }

    /// <summary>
    /// Get CLSID based on the provided context
    /// </summary>
    /// <param name="context">Context</param>
    /// <returns>CLSID for the provided context.</returns>
    /// <exception cref="InvalidOperationException">Throw an exception if the clsid context is not available for the current instance.</exception>
    public Guid GetClsid(ClsidContext context)
    {
        return !Clsids.TryGetValue(context, out var clsid)
            ? throw new InvalidOperationException($"{ProjectedClassType.FullName} is not implemented in context {context}")
            : clsid;
    }

    /// <summary>
    /// Get IID corresponding to the COM object
    /// </summary>
    /// <returns>IID.</returns>
    public Guid GetIid() => InterfaceType.GUID;
}
