using System.Collections.Generic;

public class Node<T>
{
    private T _data;
    private List<Edge<Node<T>, T>> _edges;

    public Node(T data)
    {
        _data = data;
        _edges = new List<Edge<Node<T>, T>>();
    }

    public void AddNeighbour(Node<T> node2)
    {
        bool contained = false;
        foreach (Edge<Node<T>, T> edge in _edges)
        {
            if (edge.GetNext() == node2)
            {
                contained = true;
            }
        }

        if (!contained)
        {
            _edges.Add(new Edge<Node<T>, T>(node2));
        }
    }

    public List<Node<T>> GetNeighbours()
    {
        List<Node<T>> neighbours = new List<Node<T>>();
        foreach (Edge<Node<T>, T> edge in _edges)
        {
            neighbours.Add(edge.GetNext());
        }

        return neighbours;
    }

    public List<T> GetNeighboursData()
    {
        List<T> neighboursd = new List<T>();
        foreach (Edge<Node<T>, T> edge in _edges)
        {
            neighboursd.Add(edge.GetNext()._data);
        }

        return neighboursd;
    }

    public T GetData()
    {
        return _data;
    }

    public override string ToString()
    {
        return _data.ToString();
    }
}