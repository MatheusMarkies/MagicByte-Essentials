using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EssentialMechanics.Logic
{
    public static class PathFinding2D
    {

        public static List<Node> createPath(Node start, Node end, List<Tilemap> walkableTilemaps, List<Tilemap> blockedTilemaps)
        {
            List<Node> path = new List<Node>();

            List<Node> visited = new List<Node>();
            List<Node> unvisited = new List<Node>();
            
            unvisited.Add(start);

            bool finished = false;
            while (!finished)
            {
                if (unvisited[0] != null)
                {
                    Node nearNode = unvisited[0];
                    foreach (Node node in unvisited)
                    {
                        //EssentialMechanics.Tilemap2D.setTileColor(Color.red, node.Position, walkableTilemaps[0]);
                        if (nearNode.heuristica.f > node.heuristica.f)
                        {
                            nearNode = node;
                        }
                    }

                    List<Node> sideNodes = getSideNodes(nearNode, 1, walkableTilemaps);
                    unvisited.Remove(nearNode);
                    visited.Add(nearNode);

                    //foreach (Node node in visited)
                    //EssentialMechanics.Tilemap2D.setTileColor(Color.green, node.Position, walkableTilemaps[0]);

                    //EssentialMechanics.Tilemap2D.setTileColor(Color.blue, nearNode.Position, walkableTilemaps[0]);

                    foreach (Node node in sideNodes)
                    {
                        Node childNote = node;
                        childNote.dadNode = nearNode;

                        childNote.heuristica.g = nearNode.heuristica.g + Mathf.Abs(node.Position.x - nearNode.Position.x) + Mathf.Abs(node.Position.y - nearNode.Position.y);
                        childNote.heuristica.h = Mathf.Abs(node.Position.x - end.Position.x) + Mathf.Abs(node.Position.y - end.Position.y);
                        childNote.heuristica.f = childNote.heuristica.h + childNote.heuristica.g;

                        if (!nodeCompare(unvisited, childNote) && !nodeCompare(visited, childNote))
                            unvisited.Add(childNote);
                    }

                    if (nodeCompare(sideNodes, end) || visited.Count > 1000 || unvisited.Count <= 0)
                    {
                        visited.Add(end);
                        visited = refactorPath(visited, nearNode, start);
                        finished = true;
                        break;
                    }
                }
                else
                {
                    visited.Add(end);
                    finished = true;
                    break;
                }

            }
            
            return visited;
        }

        public static List<Node> refactorPath(List<Node> node, Node last, Node start)
        {
            List<Node> path = new List<Node>();

            path.Add(last);

            Node current = last;

            if(current.dadNode != null)
            while (!nodeCompare(current.dadNode,start))
            {
                path.Add(current.dadNode);
                current = current.dadNode;
            }

            return Arrays.invertList(path);
        }

        public static Node getNearWalkable(Vector2 current, List<Tilemap> walkableTilemaps)
        {
            List<Node> sideNodes = getSideNodes(current, walkableTilemaps);
            if (sideNodes.Count > 0)
            {
                Node nearNode = sideNodes[0];
                foreach (Node node in sideNodes)
                {
                    if (Vector2.Distance(current, node.Position) < Vector2.Distance(current, nearNode.Position))
                        nearNode = node;
                }
                return nearNode;
            }
            else
                return null;
        }

        public static bool nodeCompare(Node a,Node b)
        {
            try
            {
                if (a.Position == b.Position)
                    if (a.isWalkable == b.isWalkable)
                        return true;
                return false;
            }catch(System.Exception e)
            {
                return false;
            }
        }
        public static bool nodeCompare(List<Node> list, Node b)
        { 
            if (list.Count <= 0)
                return false;
            foreach (Node a in list) 
            if (a.Position == b.Position)
                if (a.isWalkable == b.isWalkable)
                    return true;
            return false;
        }
        public static List<Node> getSideNodes(Node current,int index,List<Tilemap> walkableTilemaps)
        {
            List<Node> nodeList = new List<Node>();
            foreach (Tilemap walk in walkableTilemaps)
                for (int x = -index; x <= index; x++)
                    for (int y = -index; y <= index; y++)
                    {
                        Node node = new Node();

                        if (Tilemap2D.getTile(walk, current.Position + new Vector2(x, y)) != null)
                        {
                            node.Position = current.Position + new Vector2(x, y);
                            node.isWalkable = true;
                            if (!nodeCompare(nodeList, node) && !nodeCompare(node, current))
                                nodeList.Add(node);
                        }

                    }
            return nodeList;
        }
        public static List<Node> getSideNodes(Node current, int index, Tilemap walkableTilemap)
        {
            List<Node> nodeList = new List<Node>();
                for (int x = -index; x <= index; x++)
                    for (int y = -index; y <= index; y++)
                    {
                        Node node = new Node();

                        if (Tilemap2D.getTile(walkableTilemap, current.Position + new Vector2(x, y)) != null)
                        {
                            node.Position = current.Position + new Vector2(x, y);
                            node.isWalkable = true;
                            if (!nodeCompare(nodeList, node) && !nodeCompare(node, current))
                                nodeList.Add(node);
                        }

                    }
            return nodeList;
        }
        public static List<Node> getSideNodes(Vector2 current, List<Tilemap> walkableTilemaps)
        {
            List<Node> nodeList = new List<Node>();
            foreach (Tilemap walk in walkableTilemaps)
                for (int x = -1; x <= 1; x++)
                    for (int y = -1; y <= 1; y++)
                    {
                        Node node = new Node();

                        if (Tilemap2D.getTile(walk, current + new Vector2(x, y)) != null)
                        {
                            node.Position = current + new Vector2(x, y);
                            node.isWalkable = true;
                            
                            nodeList.Add(node);
                        }

                    }
            return nodeList;
        }
        public static Node getNode(Vector2 position,List<Tilemap> walkableTilemaps, List<Tilemap> blockedTilemaps)
        {
            foreach (Tilemap walk in walkableTilemaps)
                if (Tilemap2D.getTile(walk, position) != null)
                {
                    Node node = new Node();
                    node.Position = new Vector2((int)position.x, (int)position.y);
                    node.isWalkable = true;

                    return node;
                }
            foreach (Tilemap block in blockedTilemaps)
                if (Tilemap2D.getTile(block, position) != null)
                {
                    Node node = new Node();
                    node.Position = new Vector2((int)position.x, (int)position.y);
                    node.isWalkable = false;

                    return node;
                }
            return null;
        }
        public static Node getNode(Vector3 position, List<Tilemap> walkableTilemaps, List<Tilemap> blockedTilemaps)
        {
            foreach (Tilemap walk in walkableTilemaps)
                if (Tilemap2D.getTile(walk, position) != null)
                {
                    Node node = new Node();
                    node.Position = new Vector2((int)position.x, (int)position.y);
                    node.isWalkable = true;

                    return node;
                }
            foreach (Tilemap block in blockedTilemaps)
                if (Tilemap2D.getTile(block, position) != null)
                {
                    Node node = new Node();
                    node.Position = new Vector2((int)position.x, (int)position.y);
                    node.isWalkable = false;

                    return node;
                }
            return null;
        }
        public static Node getNode(Vector3 position, List<Tilemap> walkableTilemaps)
        {
            foreach (Tilemap walk in walkableTilemaps)
                if (Tilemap2D.getTile(walk, position) != null)
                {
                    Node node = new Node();
                    node.Position = new Vector2((int)position.x, (int)position.y);
                    node.isWalkable = true;

                    return node;
                }
            return null;
        }
        public static Node getNode(Vector3 position, Tilemap walkableTilemap)
        {
                if (Tilemap2D.getTile(walkableTilemap, position) != null)
                {
                    Node node = new Node();
                    node.Position = new Vector2((int)position.x, (int)position.y);
                    node.isWalkable = true;

                    return node;
                }
            return null;
        }
        public static List<Node> createNodeList(List<Tilemap> walkableTilemaps, List<Tilemap> blockedTilemaps)
        {
            List<Node> nodeList = new List<Node>();

            foreach (Tilemap walk in walkableTilemaps)
            {
                foreach (Vector2 position in walk.getTilesPositions())
                {
                    if (Tilemap2D.getTile(walk, position) != null) {
                        Node node = new Node();
                        node.Position = position;
                        node.isWalkable = true;

                        nodeList.Add(node);
                    }
                } 
            }

            foreach (Tilemap block in blockedTilemaps)
            {
                foreach (Vector2 position in block.getTilesPositions())
                {
                    if (Tilemap2D.getTile(block, position) != null)
                    {
                        Node node = new Node();
                        node.Position = position;
                        node.isWalkable = false;

                        nodeList.Add(node);
                    }
                }
            }

            return nodeList;
        }

        public static List<Vector2> getPositionList(List<Node> nodes)
        {
            List<Vector2> positions = new List<Vector2>();
            foreach (Node node in nodes)
                positions.Add(node.Position);
            return positions;
        }
    }
    [System.Serializable]
    public class Node
    {
        public Vector2 Position;
        [SerializeField]
        public Heuristica heuristica = new Heuristica();
        public bool isWalkable;
        public Node dadNode;

        [System.Serializable]
        public class Heuristica
        {
            [SerializeField]
            float g_ = 0, h_ = 0, f_ = 0;
            public float g
            {
                get => g_;
                set => g_ = value;
            }
            public float h
            {
                get => h_;
                set => h_ = value;
            }
            public float f
            {
                get => f_;
                set => f_ = value;
            }
        }
    }
}