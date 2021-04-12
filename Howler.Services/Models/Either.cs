// <copyright file="Either.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Models
{
    /// <summary>
    /// A class intended to hold either a value of the
    /// <typeparamref name="TLeft"/> type or the <typeparamref name="TRight"/>
    /// type, but not both.
    /// </summary>
    /// <typeparam name="TLeft">The type of the left value.</typeparam>
    /// <typeparam name="TRight">The type of the right value.</typeparam>
    public class Either<TLeft, TRight>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Either{TLeft,TRight}"/> class with a left side.
        /// </summary>
        /// <param name="left">The value for the left side.</param>
        public Either(TLeft left) =>
            this.Left = left;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Either{TLeft,TRight}"/> class with a right side.
        /// </summary>
        /// <param name="right">The value for the right side.</param>
        public Either(TRight right) =>
            this.Right = right;

        /// <summary>
        /// Gets the left value, or null if Right is set.
        /// </summary>
        public TLeft? Left { get; }

        /// <summary>
        /// Gets the right value, or null if Left is set.
        /// </summary>
        public TRight? Right { get; }

        /// <summary>Implicit conversion to the left value.</summary>
        /// <param name="either">The value to convert.</param>
        public static implicit operator TLeft?(
            Either<TLeft, TRight> either) => either.Left;

        /// <summary>Implicit conversion to the right value.</summary>
        /// <param name="either">The value to convert.</param>
        public static implicit operator TRight?(
            Either<TLeft, TRight> either) => either.Right;
    }
}