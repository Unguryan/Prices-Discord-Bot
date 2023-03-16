namespace Core.Models
{
    /// <summary>
    /// Token model.
    /// </summary>
    /// <param name="Price">Token price.</param>
    /// <param name="Rate">Price change rate in %.</param>
    internal record CryptoToken(decimal Price, decimal Rate);
}
