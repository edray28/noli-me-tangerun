using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public bool showcollider = true;
    public static LevelManager Instance { set; get; }
   //spawnerlvl
    private const float Distancebeforespawn = 100.0f;
    private const int initialsegments = 5;
    private const int initialtransitionsegments = 2;
    private const int maxsegmentsonscreen = 15;
    private Transform cameraContainer;
    private int amountofactivesegments;
    private int continioussegments;
    private int currentspawnz;
    private int currentlevel;
    private int y1, y2, y3;



    //listsofpiece
    public List<Piece> ramps = new List<Piece>();
    public List<Piece> longblocks = new List<Piece>();
    public List<Piece> jumps = new List<Piece>();
    public List<Piece> slides = new List<Piece>();
    public List<Piece> questions = new List<Piece>();

    [HideInInspector]
    public List<Piece> pieces = new List<Piece>(); //the pieces in world
    public List<Segment> availableSegments = new List<Segment>();
    public List<Segment> availableTransitions = new List<Segment>();
    [HideInInspector]
    public List<Segment> segments = new List<Segment>();

    //game
    private bool isMoving = false;
    private void Awake()
    {
        Instance = this;
        cameraContainer = Camera.main.transform;
        currentspawnz = 0;
        currentlevel = 0;
    }
            
    private void Start()
    {
        for (int i = 0; i < initialsegments; i++)
        if (i < initialtransitionsegments)
                spawntransition();
            else
                GenerateSegment();
               
    }
    private void Update()
    {
        if(currentspawnz-cameraContainer.position.z <Distancebeforespawn)
        {
            GenerateSegment();
            if(amountofactivesegments >= maxsegmentsonscreen)
            {
                segments[amountofactivesegments - 1].DeSpawn();
                amountofactivesegments--;
            }

        }
    }
    private void GenerateSegment()
    {
        spawnsegment();
        if(Random.Range(0f,1f) <(continioussegments *0.25f))
        {
            //spawntrans
            continioussegments = 0;
            spawntransition();
        }
        else
        {

        }
        continioussegments++;
    }
    private void spawnsegment()
    {
        List<Segment> possibleSeg = availableSegments.FindAll(x =>x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleSeg.Count);
        Segment s = GetSegment(id, false);
        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;
        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentspawnz;
        currentspawnz += s.lenght;
        amountofactivesegments++;
        s.Spawn();
       


    }
    private void spawntransition()
    {
        List<Segment> possibleTransition = availableTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleTransition.Count);
        Segment s = GetSegment(id, true);
        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;
        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentspawnz;
        currentspawnz += s.lenght;
        amountofactivesegments++;
        s.Spawn();

    }
    public Segment GetSegment(int id,bool transition)
    {
        Segment s = null;
        s = segments.Find(x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);
        if(s ==null)
        {
            GameObject go = Instantiate((transition) ? availableSegments[id].gameObject : availableSegments[id].gameObject) as GameObject;
        s=go.GetComponent<Segment>();
            s.SegId = id;
            s.transition = transition;
            segments.Insert(0, s);
        }
        else
        {
            segments.Remove(s);
            segments.Insert(0,s);
        }
        return s;
    }
    public Piece GetPiece(PieceType pt, int visualIndex)
    {
        Piece p = pieces.Find(x => x.type == pt && x.visualIndex == visualIndex && !x.gameObject.activeSelf);
        if(p ==null)
        {
            GameObject go =null;
            if (pt == PieceType.ramp)
                go = ramps[visualIndex].gameObject;
            else if (pt == PieceType.longblock)
                go = longblocks[visualIndex].gameObject;
            else if (pt == PieceType.jump)
                go = jumps[visualIndex].gameObject;
            else if (pt == PieceType.slide)
                go = slides[visualIndex].gameObject;
        

            go = Instantiate(go);
            p = GetComponent <Piece>();
            pieces.Add(p);
        }
        return p;
    }
}
