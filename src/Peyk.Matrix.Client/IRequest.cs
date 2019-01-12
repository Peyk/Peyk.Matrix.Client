using System.Net.Http;

namespace Peyk.Matrix.Client
{
    public interface IRequest<TResponse>
    {
        bool RequiresAuth { get; }

        HttpMethod HttpMethod { get; }

        string Url { get; }

        HttpContent ToHttpContent();
    }
}