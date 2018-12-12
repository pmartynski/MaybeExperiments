﻿using System;
using System.Threading.Tasks;

namespace Mayhap
{
    /// <summary>
    /// Maybe chaining extension methods.
    /// </summary>
    public static class Track
    {
        /// <summary>
        /// If previous successful, continue with continuation functor.
        /// </summary>
        /// <typeparam name="TPrev">Previous Maybe value type</typeparam>
        /// <typeparam name="TNext">Target Maybe value type</typeparam>
        /// <param name="previous">Previous Maybe value</param>
        /// <param name="continuation">Continuation func</param>
        /// <returns>Target Maybe</returns>
        public static Maybe<TNext> Continue<TPrev, TNext>(
            this Maybe<TPrev> previous,
            Func<TPrev, Maybe<TNext>> continuation)
            => previous ? continuation(previous) : previous.To<TNext>();

        /// <summary>
        /// If previous successful, continue with continuation functor.
        /// </summary>
        /// <typeparam name="TPrev">Previous Maybe value type</typeparam>
        /// <typeparam name="TNext">Target Maybe value type</typeparam>
        /// <param name="previous">Previous Maybe value</param>
        /// <param name="continuation">Continuation func</param>
        /// <returns>Target Maybe</returns>
        public static Task<Maybe<TNext>> Continue<TPrev, TNext>(
            this Maybe<TPrev> previous, 
            Func<TPrev, Task<Maybe<TNext>>> continuation)
            => previous ? continuation(previous) : Task.FromResult(previous.To<TNext>());

        /// <summary>
        /// If previous successful, continue with continuation functor.
        /// </summary>
        /// <typeparam name="TPrev">Previous Maybe value type</typeparam>
        /// <typeparam name="TNext">Target Maybe value type</typeparam>
        /// <param name="previous">Previous Maybe value</param>
        /// <param name="continuation">Continuation func</param>
        /// <param name="next">Target Maybe value output parameter</param>
        /// <returns>Target Maybe</returns>
        public static Maybe<TNext> Continue<TPrev, TNext>(
            this Maybe<TPrev> previous,
            Func<TPrev, Maybe<TNext>> continuation,
            out Maybe<TNext> next)
            => next = Continue(previous, continuation);

        /// <summary>
        /// If previous successful, continue with continuation functor.
        /// </summary>
        /// <typeparam name="TPrev">Previous Maybe value type</typeparam>
        /// <typeparam name="TNext">Target Maybe value type</typeparam>
        /// <param name="previous">Previous Maybe value</param>
        /// <param name="continuation">Continuation func</param>
        /// <param name="next">Target Maybe value output parameter</param>
        /// <returns>Target Maybe</returns>
        public static Task<Maybe<TNext>> Continue<TPrev, TNext>(
            this Maybe<TPrev> previous,
            Func<TPrev, Task<Maybe<TNext>>> continuation,
            out Task<Maybe<TNext>> next)
            => next = Continue(previous, continuation);
    }
}