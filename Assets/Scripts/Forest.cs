using System.Collections.Generic;

using UnityEngine;
[ExecuteAlways]
public class Forest : MonoBehaviour
{
    [SerializeField] private Mesh m;
    [SerializeField] private Material mat;
    [SerializeField] private float sampleDensity;
    [SerializeField] private float treeDensity;
    [SerializeField] private float range;
    [SerializeField] private LayerMask lm;
    [SerializeField] private Vector2 sizeRange;
    [SerializeField] private float noiseScale;
    [SerializeField] private float noiseImpact;
    [SerializeField] private float yThreshhold;
    [SerializeField] private float checkHeight;
    [SerializeField] private List<GameObject> structures = new List<GameObject>();
    private List<int> ignoreList = new List<int>();
    public Matrix4x4[] matricies;
    public Matrix4x4[] filteredMatricies;
    [SerializeField] private float ignoreRange;
    [SerializeField] private bounds[] deadZones; 
    [System.Serializable]
    public struct bounds
    {
        public Vector2 minBounds;
        public Vector2 maxBounds;
    }
    //generates the array of trees
    public void GenArrays()
    {
        RaycastHit hit;
        Vector3 offset;
        List<Matrix4x4> matrixTempList = new List<Matrix4x4>();

        for (int i = 0; i < sampleDensity * range; i++)
        {
            offset = new Vector3(Random.Range(-1.0f, 1.0f) * range, checkHeight, Random.Range(-1.0f, 1.0f) * range);

            if (Physics.Raycast(transform.position + offset, Vector3.down, out hit, yThreshhold + checkHeight, lm))
            {

                if (hit.normal == Vector3.up)
                {
                    if (CanPlaceTree(hit.point, Random.Range(0.2f, 0.8f)))
                    {
                        Matrix4x4 matrixTemp =
                            Matrix4x4.identity *
                            Matrix4x4.Translate(hit.point) *
                            Matrix4x4.Rotate(Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up)) *
                            Matrix4x4.Scale(Vector3.one *  Random.Range(sizeRange.x, sizeRange.y));


                        if (ClosestRelative(matrixTempList.ToArray(), matrixTemp) > 1 / treeDensity)
                        {
                            matrixTempList.Add(matrixTemp);
                        }
                    }
                }
            }
        }
        matricies = matrixTempList.ToArray();
        UpdateIgnoreList();
        UpdateFinalMatricies();
    }
    void Update() { DrawMesh(); }

    //adds a structure to the structure list
    public void AddStructure(GameObject _obj)
    {
        structures.Add(_obj);
        UpdateIgnoreList();
        UpdateFinalMatricies();
    }

    //removes a structure from the structure list
    public void RemoveStructure(GameObject _obj)
    {
        if (structures.Contains(_obj))
        {
            structures.Remove(_obj);
            UpdateIgnoreList();
            UpdateFinalMatricies();
        }
    }

    //updates the list of indicies we should ignore when drawing the trees
    public void UpdateIgnoreList()
    {
        ignoreList = new List<int>();
        for (int i = 0; i < structures.Count; i++)
        {
            ignoreList.AddRange(TreesInRange(structures[i].transform.position, ignoreRange));
        }
        for (int i = 0; i < deadZones.Length; i++)
        {
            ignoreList.AddRange(TreesInBounds(deadZones[i].minBounds, deadZones[i].maxBounds));
        }
    }

    //filters the list of total matricies and only includes ones we arent ignoring
    public void UpdateFinalMatricies()
    {
        List<Matrix4x4> finalMatrix = new List<Matrix4x4>();
        for (int i = 0; i < matricies.Length; i++)
        {
            if (!ignoreList.Contains(i))
            {
                finalMatrix.Add(matricies[i]);
            }
        }
        filteredMatricies = finalMatrix.ToArray();
    }
    //Gets the indicies of trees in a certain range to the origin provided
    public int[] TreesInRange(Vector3 _origin, float _range)
    {
        List<int> res = new List<int>();
        Vector3 tempPos;
        for (int i = 0; i < matricies.Length; i++)
        {
            tempPos = new Vector3(matricies[i].m03, matricies[i].m13, matricies[i].m23);
            if (Vector3.Distance(_origin, tempPos) < _range)
            {
                res.Add(i);
            }
        }
        return res.ToArray();
    }
    
    public int[] TreesInBounds(Vector2 minBounds, Vector2 maxBounds)
    {
        List<int> res = new List<int>();
        Vector3 tempPos;
        for (int i = 0; i < matricies.Length; i++)
        {
            tempPos = new Vector3(matricies[i].m03, matricies[i].m13, matricies[i].m23);
            if (tempPos.x > minBounds.x && tempPos.x < maxBounds.x)
            {
                if (tempPos.z > minBounds.y && tempPos.z < maxBounds.y)
                {
                    res.Add(i);
                }
            }
        }
        return res.ToArray();
        
    }
    //draws trees
    private void DrawMesh()
    {
        if (mat.enableInstancing)
        {
            if (filteredMatricies != null && filteredMatricies.Length > 0 && m != null)
                Graphics.DrawMeshInstanced(m, 0, mat, filteredMatricies);
        }
    }
    //helper functions
    public float SampleNoise(Vector3 _pos)
    {
        return Mathf.PerlinNoise(_pos.x / noiseScale, _pos.z / noiseScale) * noiseImpact;
    }
    public bool CanPlaceTree(Vector3 _pos, float _seed)
    {
        return _seed > SampleNoise(_pos) && Mathf.Abs(_pos.y - transform.position.y) < yThreshhold;
    }
    private float ClosestRelative(Matrix4x4[] _total, Matrix4x4 _current)
    {
        return ClosestRelative(_total, new Vector3(_current.m03, _current.m13, _current.m23));
    }
    private float ClosestRelative(Matrix4x4[] _total, Vector3 _current)
    {
        float dist = Mathf.Infinity;
        Vector3 currPos;
        for (int i = 0; i < _total.Length; i++)
        {
            currPos = new Vector3(_total[i].m03, _total[i].m13, _total[i].m23);
            float currDist = Vector3.Distance(currPos, _current);
            if (currDist < dist) dist = currDist;
        }
        return dist;
    }
    private void OnDrawGizmosSelected()
    {
        
        
        
        for (int i = 0; i < deadZones.Length; i++)
        {
            
            Vector3 pm1 = new Vector3(deadZones[i].minBounds.x, 0, deadZones[i].minBounds.y);
            Vector3 pm2 = new Vector3(deadZones[i].maxBounds.x, 0, deadZones[i].minBounds.y);
            Vector3 pm3 = new Vector3(deadZones[i].maxBounds.x, 0, deadZones[i].maxBounds.y);
            Vector3 pm4 = new Vector3(deadZones[i].minBounds.x, 0, deadZones[i].maxBounds.y);
            Gizmos.color = Color.red;
            
            Gizmos.DrawLine(pm1 + Vector3.up * yThreshhold, pm2 + Vector3.up * yThreshhold);
            Gizmos.DrawLine(pm2 + Vector3.up * yThreshhold, pm3 + Vector3.up * yThreshhold);
            Gizmos.DrawLine(pm3 + Vector3.up * yThreshhold, pm4 + Vector3.up * yThreshhold);
            Gizmos.DrawLine(pm4 + Vector3.up * yThreshhold, pm1 + Vector3.up * yThreshhold);
            
        }
        for (int i = 0; i < structures.Count; i++)
        {
            Gizmos.DrawWireSphere(structures[i].transform.position, ignoreRange);
        }
        Gizmos.color = Color.white;
        Vector3 p1 = new Vector3(-range, 0, -range) + transform.position;
        Vector3 p2 = new Vector3(range, 0, -range) + transform.position;
        Vector3 p3 = new Vector3(range, 0, range) + transform.position;
        Vector3 p4 = new Vector3(-range, 0, range) + transform.position;

        Gizmos.DrawLine(p1 + Vector3.up * yThreshhold, p2 + Vector3.up * yThreshhold);
        Gizmos.DrawLine(p2 + Vector3.up * yThreshhold, p3 + Vector3.up * yThreshhold);
        Gizmos.DrawLine(p3 + Vector3.up * yThreshhold, p4 + Vector3.up * yThreshhold);
        Gizmos.DrawLine(p4 + Vector3.up * yThreshhold, p1 + Vector3.up * yThreshhold);

        Gizmos.DrawLine(p1 + Vector3.down * yThreshhold, p2 + Vector3.down * yThreshhold);
        Gizmos.DrawLine(p2 + Vector3.down * yThreshhold, p3 + Vector3.down * yThreshhold);
        Gizmos.DrawLine(p3 + Vector3.down * yThreshhold, p4 + Vector3.down * yThreshhold);
        Gizmos.DrawLine(p4 + Vector3.down * yThreshhold, p1 + Vector3.down * yThreshhold);

        Gizmos.DrawLine(p1 + Vector3.down * yThreshhold, p1 + Vector3.up * yThreshhold);
        Gizmos.DrawLine(p2 + Vector3.down * yThreshhold, p2 + Vector3.up * yThreshhold);
        Gizmos.DrawLine(p3 + Vector3.down * yThreshhold, p3 + Vector3.up * yThreshhold);
        Gizmos.DrawLine(p4 + Vector3.down * yThreshhold, p4 + Vector3.up * yThreshhold);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(p1 + Vector3.up * checkHeight, p2 + Vector3.up * checkHeight);
        Gizmos.DrawLine(p2 + Vector3.up * checkHeight, p3 + Vector3.up * checkHeight);
        Gizmos.DrawLine(p3 + Vector3.up * checkHeight, p4 + Vector3.up * checkHeight);
        Gizmos.DrawLine(p4 + Vector3.up * checkHeight, p1 + Vector3.up * checkHeight);

        Gizmos.DrawLine(p1 + Vector3.down * yThreshhold, p1 + Vector3.up * checkHeight);
        Gizmos.DrawLine(p2 + Vector3.down * yThreshhold, p2 + Vector3.up * checkHeight);
        Gizmos.DrawLine(p3 + Vector3.down * yThreshhold, p3 + Vector3.up * checkHeight);
        Gizmos.DrawLine(p4 + Vector3.down * yThreshhold, p4 + Vector3.up * checkHeight);

    }
}
