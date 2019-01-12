using System.Threading;
using System.Threading.Tasks;

namespace Peyk.Matrix.Client
{
    /// <summary>
    /// Matrix HTTP client
    /// </summary>
    public interface IMatrixClient
    {
//        bool IsLoggedIn { get; }

//        string User { get; }

//        Task LoginAsync(
//            CancellationToken cancellationToken = default
//        );

        Task<T> MakeRequestAsync<T>(
            IRequest<T> request,
            CancellationToken cancellationToken = default
        );
    }
}