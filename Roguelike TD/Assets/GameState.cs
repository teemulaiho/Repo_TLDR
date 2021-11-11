using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridUtils {
    const float GRID_SIZE = 5; // World units (meters?)

    public static Vector3 GridToWorld(Vector2Int grid_pos, float y) {
        Vector2 xz = (Vector2)grid_pos * GRID_SIZE;
        return new Vector3(xz.x, y, xz.y);
    }

    public static Vector2Int WorldToGrid(Vector3 world_pos) {
        Vector2 xy = new Vector2(world_pos.x, world_pos.z);
        xy /= GRID_SIZE;
        return new Vector2Int(Mathf.FloorToInt(xy.x), Mathf.FloorToInt(xy.y));
    }
} 

public class Entity {
    public GameObject unity_object;
    public Vector2Int position;
}

public class EntityList {
    public List<Entity> entities = new List<Entity>();

    public void AddEntity(Entity e) {
        entities.Add(e);
    }
}

public class GameState : MonoBehaviour
{
    public Dictionary<Vector2Int, EntityList> world_cells = new Dictionary<Vector2Int, EntityList>();
    public List<Entity> all_entities = new List<Entity>();

    public GameObject placeholder;
    
    void AddEntity(Entity e){
        EntityList list_for_cell;

        if(!world_cells.TryGetValue(e.position, out list_for_cell)){
            list_for_cell = new EntityList();            
            world_cells.Add(e.position, list_for_cell);
        }

        Debug.Assert(list_for_cell != null);

        list_for_cell.AddEntity(e);
        all_entities.Add(e);
    }

    void Start()
    {
        for(int x = 0; x < 10; x++){
            for(int y = 0; y < 10; y++){
                Entity e = new Entity();
                e.position = new Vector2Int(x-5, y-5);

                AddEntity(e);
            }
        }
    }

    void Update()
    {  
        foreach(Entity e in all_entities){
            if(e.unity_object == null){
                var g = Instantiate(placeholder, GridUtils.GridToWorld(e.position, 0), Quaternion.identity);
                e.unity_object = g;
            }
        }
    }
}