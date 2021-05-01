﻿// <copyright file="ObserverUnsubscriber.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace Raptor.Observables.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An observer unsubscriber for unsubscribing from an <see cref="Observable{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of data that is pushed to all of the subscribed <see cref="Observer{T}"/>s.
    /// </typeparam>
    public class ObserverUnsubscriber<T> : IDisposable
    {
        private readonly List<IObserver<T>> observers = new ();
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObserverUnsubscriber{T}"/> class.
        /// </summary>
        /// <param name="observers">The list of observer subscriptions.</param>
        /// <param name="observer">The observer that has been subscribed.</param>
        internal ObserverUnsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this.observers = observers;
            Observer = observer;
        }

        /// <summary>
        /// Gets the observer of this unsubscription.
        /// </summary>
        public IObserver<T> Observer { get; }

        /// <summary>
        /// Gets the total number of current subscriptions that an <see cref="Observable{T}"/> has.
        /// </summary>
        public int TotalObservers => this.observers.Count;

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to dispose of managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    if (Observer != null && this.observers.Contains(Observer))
                    {
                        this.observers.Remove(Observer);
                    }
                }

                this.isDisposed = true;
            }
        }
    }
}
