using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Jump : MonoBehaviour
{
    NavMeshAgent NavMeshAgent;
    Rigidbody Rigidbody;
    GameObject Target;
    //public float ReachedStartPointDistance = 0.5f;
    //public Transform DummyAgent;
    //public Vector3 EndJumpPosition;
    //public float MaxJumpableDistance = 80f;
    public float JumpTime = 0.6f;
    public float AddToJumpHeight;

    //public Transform[] FinSalto;

    //Transform _dummyAgent;
    Vector3 JumpStartPoint;
    //Vector3 JumpMidPoint;
    Vector3 JumpEndPoint;
    bool checkForStartPointReached;
    //Transform _transform;
    List<Vector3> Path = new List<Vector3>();
    float JumpDistance;
    Vector3[] _jumpPath;
    bool previousRigidBodyState;

    Animator animatorEnemigo;

    bool Point3 = false;

    // Use this for initialization
    void Start ()
    {
        if (gameObject.name != "E_Normal(Clone)")
        {
            print(gameObject.name);
            animatorEnemigo = gameObject.transform.GetChild(0).GetComponent<Animator>();
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(checkForStartPointReached)
        {

        }
	}

    public void Salto(string Col)
    {
        print("salto");
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Rigidbody = GetComponent<Rigidbody>();

        JumpStartPoint = transform.position;

        //PREPARAMOS EL SALTO
        Target = GameObject.FindGameObjectWithTag("Jugador");
        print(Col);
        switch (Col)
        {  
            case "pos 1":
                JumpEndPoint = GameObject.Find("FinSalto 1").transform.position;
                break;
            case "pos 2":
                JumpEndPoint = GameObject.Find("FinSalto 2").transform.position;
                break;
            case "pos 3":
                JumpEndPoint = GameObject.Find("FinSalto 3").transform.position;
                AddToJumpHeight = 10;
                Point3 = true;
                JumpTime = 2;
                break;
            case "pos 4":
                JumpEndPoint = GameObject.Find("FinSalto 4").transform.position;
                break;
            case "pos 5":
                JumpEndPoint = GameObject.Find("FinSalto 5").transform.position;
                break;
        }

        //RUTA DE SALTO
        Path.Add(JumpStartPoint);

        var tempMid = Vector3.Lerp(JumpStartPoint, JumpEndPoint, 0.5f);
        tempMid.y = tempMid.y + NavMeshAgent.height + AddToJumpHeight;

        Path.Add(tempMid);

        Path.Add(JumpEndPoint);

        JumpDistance = Vector3.Distance(JumpStartPoint, JumpEndPoint);

        //SALTAMOS
        previousRigidBodyState = Rigidbody.isKinematic;
        NavMeshAgent.enabled = false;
        Rigidbody.isKinematic = true;

        _jumpPath = Path.ToArray();
        Rigidbody.DOLocalPath(_jumpPath, JumpTime, PathType.CatmullRom).OnComplete(JumpFinished);

        if (gameObject.name != "E_Normal(Clone)" && !Point3)
        {
            animatorEnemigo.SetBool("enElAire", true);
        }

    }

    void JumpFinished()
    {
        NavMeshAgent.enabled = true;
        Rigidbody.isKinematic = previousRigidBodyState;
        animatorEnemigo.SetBool("enElAire", false);
    }
}
