using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Floor: MonoBehaviour {

    private GameObject coin;
    private GameObject laserRadar;
    private GameObject obstacle;
    private GameObject floor;

    public Floor(GameObject coin, GameObject laserRadar, GameObject obstacle,GameObject floor):base()
    {
        
        this.coin = coin;
        this.laserRadar = laserRadar;
        this.obstacle = obstacle;
        this.floor = floor ;
    }


    public  void createGameObjectsOnFloor()
    {
        float xFloor = floor.transform.position.x;
        float zFloor = floor.transform.position.z;

        System.Random random = new System.Random();
        // generate from 5 to 10 objects
        int objectsNum = 10;
        // random number of laser radars from 1 to 5
        int obstaclesNum = random.Next(1,5);
        objectsNum -= obstaclesNum;
        // random number of obstacles from 1 to 5
        int laserRadarNum = random.Next(1,3);
        objectsNum -= laserRadarNum;
        // number of coins will be the rest of objects available
        int coinNum = objectsNum;

        ArrayList gameObjects = new ArrayList();
        for (int j = 0; j < objectsNum; j++)
        {
            if (laserRadarNum > 0)
            {
                gameObjects.Add(GameObjectsEnum.laserRadar);
                laserRadarNum--;
                continue;
            }

            if (obstaclesNum > 0)
            {
                gameObjects.Add(GameObjectsEnum.Obstacle);
                obstaclesNum--;
                continue;
            }

            if (coinNum > 0)
            {
                gameObjects.Add(GameObjectsEnum.Coin);
                coinNum--;
                
            }

        }

        // shuffle list
        shufleList(gameObjects);
        GameObjectsEnum[,] map = new GameObjectsEnum[20,3];
        int xTranslation, index;
        GameObjectsEnum o;
        GameObject g;
        // populate map
        for (int i = 0; i< 20; i+=4)
        {
            for(int j=0; j<3; j++)
            {
                if (gameObjects.Count == 0)
                    return;

                index = random.Next(0, gameObjects.Count);
                o = (GameObjectsEnum)gameObjects[index];
                if(isValidPlace(map,new Position(i, j),o))
                {
                    g = null;
                    gameObjects.RemoveAt(index);
                    if (o.Equals(GameObjectsEnum.laserRadar))
                    {
                       g = Instantiate(laserRadar);
                       g.transform.position = new Vector3(0, 1f, zFloor + i -5);
                        map[i, 0] = o;
                    }
                    else { 
}

                    if (o.Equals(GameObjectsEnum.Obstacle))
                    {
                        g = Instantiate(obstacle);
                        xTranslation = getXTranslation(j);
                        g.transform.position = new Vector3(xFloor + xTranslation, 1, zFloor + i -5);
                        map[i, j] = o;
                    }

                    if (o.Equals(GameObjectsEnum.Coin))
                    {
                        g = Instantiate(coin);
                        xTranslation = getXTranslation(j);
                        g.transform.position = new Vector3(xFloor + xTranslation, 1, zFloor + i -5);
                        map[i, j] = o;
                    }

                    if (g != null)
                    {
                        g.GetComponent<MeshRenderer>().enabled = true;
                        g.transform.parent = floor.transform;
                    }

                }
            }
        }

    }
    
    /**
     * get translation in X direction
     * 
     * j is in range [0,1,2]
     */
    private int getXTranslation(int j)
    {
        if (j == 0)
            return -4;

        if (j == 1)
            return 0;

        if (j == 2)
            return 4;

        return 0;
    }

    /**
     * Map is 20*3 matrix 
     */
    private bool isValidPlace(GameObjectsEnum[,] map, Position newPosition, GameObjectsEnum o)
    {

        if (o.Equals(GameObjectsEnum.Obstacle))
        {
            // no laser Rader at this row
            if (map[newPosition.getRow(),0].Equals(GameObjectsEnum.laserRadar))
                return false;

            //  make sure that if o is an obstacle, that we cannot have three obstacle one row 
            if (newPosition.getColumn() == 0 && map[newPosition.getRow(),1].Equals(GameObjectsEnum.Obstacle) && map[newPosition.getRow(),2].Equals(GameObjectsEnum.Obstacle))
                return false;

            if (newPosition.getColumn() == 1 && map[newPosition.getRow(),0].Equals(GameObjectsEnum.Obstacle) && map[newPosition.getRow(),2].Equals(GameObjectsEnum.Obstacle))
                return false;

            if (newPosition.getColumn() == 2 && map[newPosition.getRow(),0].Equals(GameObjectsEnum.Obstacle) && map[newPosition.getRow(),1].Equals(GameObjectsEnum.Obstacle))
                return false;

        }
        else
        {
            if (o.Equals(GameObjectsEnum.laserRadar))
            {
                // check that all columns in row r donot have any game objects, where r = newPosition.getRow()
                if (!map[newPosition.getRow(),0].Equals(GameObjectsEnum.Empty) ||
                    !map[newPosition.getRow(),1].Equals(GameObjectsEnum.Empty) ||
                    !map[newPosition.getRow(),2].Equals(GameObjectsEnum.Empty))
                    return false;

                // check that the nearest laserRadar is far 8 in z-axis (8 rows)
                if (newPosition.getRow() - 4 >= 0 && map[newPosition.getRow() - 4, 0].Equals(GameObjectsEnum.laserRadar))
                    return false;
            }
            else
            {
                if (o.Equals(GameObjectsEnum.Coin))
                {
                    // check that there is not laser radar on same row of newPosition.getRow()
                    if (map[newPosition.getRow(),0].Equals(GameObjectsEnum.laserRadar))
                        return false;

                    // check that there is no coins on the same row
                    if (newPosition.getColumn() == 0 && map[newPosition.getRow(),1].Equals(GameObjectsEnum.Coin) && map[newPosition.getRow(),2].Equals(GameObjectsEnum.Coin))
                        return false;

                    if (newPosition.getColumn() == 1 && map[newPosition.getRow(),0].Equals(GameObjectsEnum.Coin) && map[newPosition.getRow(),2].Equals(GameObjectsEnum.Coin))
                        return false;

                    if (newPosition.getColumn() == 2 && map[newPosition.getRow(),0].Equals(GameObjectsEnum.Coin) && map[newPosition.getRow(),1].Equals(GameObjectsEnum.Coin))
                        return false;
                }
            }
        }

        return true;
    }


    public static void shufleList(ArrayList list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GameObjectsEnum value = (GameObjectsEnum)list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}
