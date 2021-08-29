// <copyright file="Either.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Models
{
    using System;

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

        /// <summary>Performs a fold operation over the either.</summary>
        /// <param name="foldLeft">The binding when left is set.</param>
        /// <param name="foldRight">The binding when right is set.</param>
        /// <typeparam name="TL">The new left type.</typeparam>
        /// <typeparam name="TR">The new right type.</typeparam>
        /// <returns>The folded either.</returns>
        /// <remarks>
        /// For traditional monadic evaluations with right bias, use
        /// <see cref="Map{TR}"/>.
        /// </remarks>
        public Either<TL, TR> Fold<TL, TR>(
            Func<TLeft, Either<TL, TR>> foldLeft,
            Func<TRight, Either<TL, TR>> foldRight) =>
            this.Left != null ?
                foldLeft(this.Left) :
                this.Right != null ?
                    foldRight(this.Right) :

                    // This should not be possible without reflection.
                    throw new ArgumentNullException("neither");

        /// <summary>
        /// Performs a right fold operation.
        /// </summary>
        /// <param name="func">The fold operation.</param>
        /// <typeparam name="TR">The new right type.</typeparam>
        /// <returns>
        /// Either the original left, or the right-folded either.
        /// </returns>
        public Either<TLeft, TR> Map<TR>(
            Func<TRight, Either<TLeft, TR>> func) =>
            this.Left != null ?
                new Either<TLeft, TR>(this.Left) :
                this.Right != null ?
                    func(this.Right) :

                    // This should not be possible without reflection.
                    throw new ArgumentNullException("neither");
    }
}
