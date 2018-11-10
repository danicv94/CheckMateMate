using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph<T>
{
    private List<Node<T>> _nodes;

    public Graph()
    {
        _nodes = new List<Node<T>>();
    }

    public Node<T> AddNode(T data)
    {
        Node<T> node = new Node<T>(data);
        _nodes.Add(node);
        return node;
    }

    public Node<T> AddNode(Node<T> node)
    {
        _nodes.Add(node);
        return node;
    }

    public void SetAsNeighbours(Node<T> node1, Node<T> node2)
    {
        node1.AddNeighbour(node2);
        node2.AddNeighbour(node1);
    }

    public Node<T> GetNode(int index)
    {
        if (index < _nodes.Count)
        {
            return _nodes[index];
        }

        return null;
    }

    public static Graph<TU> GenerateGraph<TU>(List<TU> items) where TU : IMapItem<TU>
    {
        Graph<TU> graph = new Graph<TU>();
        Hashtable nodes = new Hashtable();
        foreach (TU item1 in items)
        {
            foreach (TU item2 in items)
            {
                if (!item1.Equals(item2))
                {
                    int x1 = (int) item1.GetPosition().x;
                    int y1 = (int) item1.GetPosition().y;
                    int x2 = (int) item2.GetPosition().x;
                    int y2 = (int) item2.GetPosition().y;

                    int distance = x1 - x2 + y1 - y2;
                    if (Mathf.Abs((float) distance) <= 1 && Mathf.Abs((float) x1 - x2) <= 1 &&
                        Mathf.Abs((float) y1 - y2) <= 1)
                    {
                        Node<TU> node1;
                        Node<TU> node2;
                        if (nodes.ContainsKey(item1))
                        {
                            node1 = (Node<TU>) nodes[item1];
                        }
                        else
                        {
                            node1 = new Node<TU>(item1);
                            item1.SetBindedNode(node1);
                            nodes.Add(item1, node1);
                            graph.AddNode(node1);
                        }

                        if (nodes.ContainsKey(item2))
                        {
                            node2 = (Node<TU>) nodes[item2];
                        }
                        else
                        {
                            node2 = new Node<TU>(item2);
                            item2.SetBindedNode(node2);
                            nodes.Add(item2, node2);
                            graph.AddNode(node2);
                        }

                        graph.SetAsNeighbours(node1, node2);
                    }
                }
            }
        }

        return graph;
    }

    public List<Node<T>> FindPath(Node<T> origin, Node<T> target)
    {
        Hashtable from = new Hashtable();

        Queue<Node<T>> frontier = new Queue<Node<T>>();
        frontier.Enqueue(origin);

        while (frontier.Count > 0)
        {
            Node<T> current = frontier.Dequeue();

            if (current.Equals(target))
            {
                break;
            }

            foreach (Node<T> node in current.GetNeighbours())
            {
                if (!from.ContainsKey(node))
                {
                    frontier.Enqueue(node);
                    from.Add(node, current);
                }
            }
        }

        return RebuildPath(from, origin, target);
    }

    public static List<UT> GetNodeListAsData<UT>(List<Node<UT>> nodeList)
    {
        List<UT> list = new List<UT>();
        foreach (Node<UT> node in nodeList)
        {
            list.Add(node.GetData());
        }

        return list;
    }

    public List<Node<T>> GetAllNodesAtDistance(Node<T> initialNode, int distance)
    {
        List<Node<T>> temporaryList = new List<Node<T>>();
        temporaryList.Add(initialNode);
        HashSet<Node<T>> returnNodes;
        foreach (Node<T> node in temporaryList)
        {
            List<Node<T>> secondaryList = new List<Node<T>>();
            foreach (Node<T> neighbour in node.GetNeighbours())
            {

            }
        }

        return temporaryList;
    }

    private List<Node<T>> RebuildPath(Hashtable from, Node<T> start, Node<T> goal)
    {
        List<Node<T>> path = new List<Node<T>>();
        Node<T> current = goal;
        while (!current.Equals(start))
        {
            path.Add(current);
            current = (Node<T>) from[current];
        }

        path.Add(start);
        path.Reverse();
        return path;
    }

    public List<T> GetRandomPath()
    {
        List<T> path = new List<T>();
        path.Add(_nodes[0].GetData());
        path.Add(_nodes[1].GetData());
        path.Add(_nodes[2].GetData());
        return path;
    }
}

public interface IMapItem<T>
{
    Vector2 GetPosition();
    void SetBindedNode(Node<T> node);
}