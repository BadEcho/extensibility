﻿//-----------------------------------------------------------------------
// <copyright>
//      Created by Matt Weber <matt@badecho.com>
//      Copyright @ 2021 Bad Echo LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;

namespace BadEcho.Odin.Extensibility.Hosting
{
    /// <summary>
    /// Provides a scoped context for accessing plugins loaded by Odin's Extensibility framework.
    /// </summary>
    internal sealed class PluginContext : IDisposable
    {
        private readonly CompositionHost _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginContext"/> class.
        /// </summary>
        /// <param name="strategy">The initialization strategy used to initialize the context.</param>
        public PluginContext(IPluginContextStrategy strategy) 
            => _container = strategy.CreateContainer();

        /// <summary>
        /// Retrieves all exports that fulfill the specified generic contract type.
        /// </summary>
        /// <typeparam name="TContract">The contract type whose exports should be loaded.</typeparam>
        /// <returns>A collection of exported <typeparamref name="TContract"/> values.</returns>
        public IEnumerable<TContract> Load<TContract>() 
            => _container.GetExports<TContract>();

        /// <summary>
        /// Retrieves all exports and accompanying metadata that fulfill the specified generic contract type.
        /// </summary>
        /// <typeparam name="TContract">The contract type whose exports should be loaded.</typeparam>
        /// <typeparam name="TMetadata">The metadata view type associated with the exported contract type.</typeparam>
        /// <returns>A collection of exported <see cref="Lazy{TContract,TMetadata}"/> values.</returns>
        public IEnumerable<Lazy<TContract, TMetadata>> Load<TContract, TMetadata>() 
            => _container.GetExports<Lazy<TContract, TMetadata>>();

        /// <summary>
        /// Injects exports into the provided attributed pluggable part.
        /// </summary>
        /// <param name="pluggablePart">An object containing loose import attributions.</param>
        public void Inject(object pluggablePart) 
            => _container.SatisfyImports(pluggablePart);

        /// <inheritdoc/>
        public void Dispose() 
            => _container.Dispose();
    }
}
