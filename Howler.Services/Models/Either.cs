namespace Howler.Services.Models
{
    /// <summary>
    /// A class intended to hold either a value of the
    /// <typeparamref name="TLeft"/> type or the <typeparamref name="TRight"/>
    /// type, but not both.
    /// </summary>
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
        public static implicit operator TLeft? (Either<TLeft, TRight> either) => either.Left;

        /// <summary>Implicit conversion to the right value.</summary>
        /// <param name="either">The value to convert.</param>
        public static implicit operator TRight? (Either<TLeft, TRight> either) => either.Right;
    }
}