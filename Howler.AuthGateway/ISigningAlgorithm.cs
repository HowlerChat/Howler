namespace Howler.AuthGateway
{
    public interface ISigningAlgorithm
    {
        string Sign(string payload);
    }
}