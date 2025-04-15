using PetFamily.Volunteers.Application.Background;
using System.Threading.Channels;

namespace PetFamily.Volunteers.Infrastructure.MessageQueues;
public class FileCleanerQueue : IFileCleanerQueue
{
    private static readonly Channel<IEnumerable<string>> _channel = Channel.CreateUnbounded<IEnumerable<string>>();

    public async Task PublishAsync(IEnumerable<string> files, CancellationToken cancellationToken = default)
    {
        await _channel.Writer.WriteAsync(files, cancellationToken);
    }
    public async Task<IEnumerable<string>> ConsumeAsync(CancellationToken cancellationToken = default)
    {
        return await _channel.Reader.ReadAsync(cancellationToken);
    }
}
