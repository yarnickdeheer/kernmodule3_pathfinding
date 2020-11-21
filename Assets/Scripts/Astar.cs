using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Astar
{
    /// <summary>
    /// TODO: Implement this function so that it returns a list of Vector2Int positions which describes a path
    /// 
    ///
    /// Note that you will probably need to add some helper functions
    /// from the startPos to the endPos
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="grid"></param>
    /// <returns></returns> Agent.gameObject.transform.position

    List<Node> Useablecells;

    //List<Node> Neighbours;
    List<Vector2Int> positions;
    public Node[,] nodegrid;
    //Node CurrentNode;

    public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
    {
        Useablecells = new List<Node>();
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Useablecells.Add(new Node(grid[i, j].gridPosition, null, int.MaxValue, (int)Vector2Int.Distance(grid[i, j].gridPosition, endPos)));
            }
        }
        positions = new List<Vector2Int>();

        foreach (Node node in Useablecells)
        {
            if (node.position == startPos)
            {
                node.GScore = 0;
            }
        }
        while (Useablecells.Count != 0)
        {
            Node currentNode = null;
            for (int i = 0; i < Useablecells.Count; i++)
            {

                int bottomS = int.MaxValue;
                if (Useablecells[i].FScore < bottomS)
                {
                    bottomS = (int)Useablecells[i].FScore;
                    currentNode = Useablecells[i];

                }

            }
            if (currentNode.position == endPos)
            {
                while (currentNode.parent != null)
                {
                    positions.Add(currentNode.position);
                    currentNode = currentNode.parent;
                }
                positions.Add(startPos);
                positions.Reverse();
                Useablecells = null;

                return positions;
            }
            Useablecells.Remove(currentNode);
            SellectNeighbour(currentNode,grid);
          

        }
        return positions;
    }

     void SellectNeighbour(Node currentNode , Cell[,] grid)
    {
            Cell cel = new Cell();
            List<Node> Neighbours = new List<Node>();
            for (int i = 0; i < Useablecells.Count; i++)
            {
                if (Useablecells[i].position == grid[Useablecells[i].position.x, Useablecells[i].position.y].gridPosition)
                {
                    cel = grid[Useablecells[i].position.x, Useablecells[i].position.y];
                }
                if ((Useablecells[i].position.x == currentNode.position.x - 1 && Useablecells[i].position.y == currentNode.position.y && !grid[Useablecells[i].position.x, Useablecells[i].position.y].HasWall(Wall.RIGHT)))
                {

                    Neighbours.Add(Useablecells[i]);

                }
                if ((Useablecells[i].position.x == currentNode.position.x + 1 && Useablecells[i].position.y == currentNode.position.y && !grid[Useablecells[i].position.x, Useablecells[i].position.y].HasWall(Wall.LEFT)))
                {

                    Neighbours.Add(Useablecells[i]);

                }
                if ((Useablecells[i].position.x == currentNode.position.x && Useablecells[i].position.y == currentNode.position.y - 1 && !grid[Useablecells[i].position.x, Useablecells[i].position.y].HasWall(Wall.UP)))
                {

                    Neighbours.Add(Useablecells[i]);

                }
                if ((Useablecells[i].position.x == currentNode.position.x && Useablecells[i].position.y == currentNode.position.y + 1 && !grid[Useablecells[i].position.x, Useablecells[i].position.y].HasWall(Wall.DOWN)))
                {

                    Neighbours.Add(Useablecells[i]);

                }

            }
            foreach (Node NeighbourNode in Neighbours)
            {
                //    Debug.Log("neighbour node pos "+NeighbourNode.position);
                float tempGScore = currentNode.GScore + 1;
                if (tempGScore < NeighbourNode.GScore)
                {
                    NeighbourNode.GScore = tempGScore;
                    NeighbourNode.parent = currentNode;
                }
                //  NeighbourNode.GScore = int .MaxValue;
            }
        }
    

    // move functie 

        // check for upstruction funtion cell.haswall

        /// <summary>
        /// This is the Node class you can use this class to store calculated FScores for the cells of the grid, you can leave this as it is
        /// </summary>
    public class Node
    {
        public Vector2Int position; //Position on the grid
        public Node parent; //Parent Node of this node

        public float FScore { //GScore + HScore
            get { return GScore + HScore; }
        }
        public float GScore; //Current Travelled Distance
        public float HScore; //Distance estimated based on Heuristic huidige node tot het einde

        public Node() { }
        public Node(Vector2Int position, Node parent, int GScore, int HScore)
        {
            this.position = position;
            this.parent = parent;
            this.GScore = GScore;
            this.HScore = HScore;
        }
    }
}

//for (int i = 0; i < nodegrid.GetLength(0); i++)
//{
//    for (int j = 0; j < nodegrid.GetLength(1); j++)
//    {
//        if (parent == nodegrid[i,j].position)
//        {

//            if (nodegrid[i, j].position.x == currentNode.position.x - 1 && nodegrid[i, j].position.y == currentNode.position.y && !cellgrid[i,j].HasWall(Wall.RIGHT))       // rechts
//            {
//                Neighbours.Add(nodegrid[i, j]);

//            }
//            if (nodegrid[i, j].position.x == currentNode.position.x + 1 && nodegrid[i, j].position.y == currentNode.position.y && !cellgrid[i, j].HasWall(Wall.LEFT))      //links
//            {
//                Neighbours.Add(nodegrid[i, j]);

//            }

//            if (nodegrid[i, j].position.x == currentNode.position.x && nodegrid[i, j].position.y == currentNode.position.y - 1 && !cellgrid[i, j].HasWall(Wall.UP))  //omhoog
//            {
//                Neighbours.Add(nodegrid[i, j]); 


//            }

//            if (nodegrid[i, j].position.x == currentNode.position.x && nodegrid[i, j].position.y == currentNode.position.y + 1 && !cellgrid[i, j].HasWall(Wall.DOWN))//omlaag
//            {
//                Neighbours.Add(nodegrid[i, j]);

//            }
//        }
//    }

//}