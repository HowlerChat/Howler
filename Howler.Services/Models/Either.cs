namespace Howler.Services.Models
{
    public class Either<TLeft, TRight>
    {
        public Either(TLeft left) =>
            (this.Left) = left;

        public Either(TRight right) =>
            (this.Right) = right;

        public TLeft? Left { get; }

        public TRight? Right { get; }
    }
}