public class Edge<T,U> where T : Node<U>
{
    private T _next;

    public Edge(T node)
    {
        _next = node;
    }

    public T GetNext()
    {
        return _next;
    }
}