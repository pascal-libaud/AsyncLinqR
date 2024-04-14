namespace AsyncLinqR;

public static partial class AsyncLinq
{
    public static async IAsyncEnumerable<T> TakeLastAsync<T>(this IAsyncEnumerable<T> source, int count, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (count > 0)
        {
            var queue = new Queue<T>();
            await foreach (var item in source.WithCancellation(cancellationToken))
            {
                queue.Enqueue(item);
                if (queue.Count == count + 1)
                    queue.Dequeue();
            }

            while (queue.Count > 0)
                yield return queue.Dequeue();
        }
    }
}