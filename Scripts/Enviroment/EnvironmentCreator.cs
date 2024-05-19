using UnityEngine;

public class EnvironmentCreator : MonoBehaviour
{
    public GameObject wallPrefab;    // Prefab for wall sections
    public GameObject floorPrefab;   // Prefab for floor tiles
    public GameObject counterPrefab; // Prefab for the coffee counter
    public GameObject tablePrefab;   // Prefab for tables
    public GameObject chairPrefab;   // Prefab for chairs

    private int shopWidth = 10; // Width of the coffee shop (in units)
    private int shopDepth = 8;  // Depth of the coffee shop (in units)

    void Start()
    {
        CreateFloor();
        CreateWalls();
        CreateCounter();
        CreateTablesAndChairs();
    }

    private void CreateFloor()
    {
        for (int x = -shopWidth / 2; x < shopWidth / 2; x++)
        {
            for (int z = -shopDepth / 2; z < shopDepth / 2; z++)
            {
                Instantiate(floorPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }

    private void CreateWalls()
    {
        // Create walls around the perimeter (you might need to adjust the y-position of the walls depending on the wallPrefab's dimensions)
        for (int i = -shopWidth / 2; i <= shopWidth / 2; i++)
        {
            Instantiate(wallPrefab, new Vector3(i, 2.5f, -shopDepth / 2), Quaternion.identity); // Back wall
            Instantiate(wallPrefab, new Vector3(i, 2.5f, shopDepth / 2), Quaternion.identity);  // Front wall
        }
        for (int i = -shopDepth / 2; i <= shopDepth / 2; i++)
        {
            Instantiate(wallPrefab, new Vector3(-shopWidth / 2, 2.5f, i), Quaternion.Euler(0, 90, 0)); // Left wall
            Instantiate(wallPrefab, new Vector3(shopWidth / 2, 2.5f, i), Quaternion.Euler(0, 90, 0));  // Right wall
        }
    }

    private void CreateCounter()
    {
        // Place the counter (adjust position as needed)
        Instantiate(counterPrefab, new Vector3(0, 1, -shopDepth / 2 + 1), Quaternion.identity);
    }

    private void CreateTablesAndChairs()
    {
        // Place tables and chairs (adjust positions as needed)
        int numTables = 2; 
        for (int i = 0; i < numTables; i++)
        {
            float xOffset = -2 + 4 * i;  // Adjust table spacing
            Instantiate(tablePrefab, new Vector3(xOffset, 0.5f, 1), Quaternion.identity); 
            Instantiate(chairPrefab, new Vector3(xOffset - 1, 0.5f, 1), Quaternion.Euler(0, 180, 0)); 
            Instantiate(chairPrefab, new Vector3(xOffset + 1, 0.5f, 1), Quaternion.identity); 
        }
    }
}
