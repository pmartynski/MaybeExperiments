﻿using System;

namespace Mayhap.Error
{
    /// <summary>
    /// The problem title attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ProblemTitleAttribute : Attribute
    {
        /// <summary>
        /// The problem title.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates a <see cref="ProblemTitleAttribute"/> instance.
        /// </summary>
        /// <param name="title">The problem title.</param>
        public ProblemTitleAttribute(string title) => Value = title;
    }
}