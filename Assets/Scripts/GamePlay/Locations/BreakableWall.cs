using UnityEngine;

public class BreakableWall : MonoBehaviour, IHitable
{
    [SerializeField] int wallNumber;
    [SerializeField] int hp;
    [SerializeField] Element requiredElement;
    Wall wall;
    bool wallWasDestroyed;

    [SerializeField] GameObject destroyAnim;

    [SerializeField] AudioClip destroySound, hitSound;


    void Start(){
        EventManager.Save.AddListener(Save);
        EventManager.Load.AddListener(Load);
        var psmain = GetComponent<ParticleSystem>().main;
        psmain.startColor = requiredElement.particleColor;
        Load();
    }

    void Load(){
        Debug.Log("Loaded wall");
        if(!Serializer.CheckFileExistance("wall" + wallNumber))
        {
            return;
        }
        Wall wall = Serializer.Load<Wall>("wall" + wallNumber);

        wallWasDestroyed = wall.wallWasDestroyed;
        
        if(wallWasDestroyed){
            gameObject.SetActive(false);
        }
    }

    void Save(){
        Wall wall = new Wall();
        wall.wallNumber = wallNumber;
        wall.wallWasDestroyed = wallWasDestroyed;
        Debug.Log(wallWasDestroyed + " " + wallNumber);
        Debug.Log(wall.wallWasDestroyed + " " + wall.wallNumber);
        Serializer.Save("wall" + wallNumber, wall);
    }

    public void TakeDamage(float value, Element attackingElement){
        if(attackingElement == requiredElement || requiredElement == null)
        {
            hp--;
            EventManager.PlaySound.Invoke(hitSound);
            if(hp == 0)
            {
                wallWasDestroyed = true;
                Save();
                GameObject anim = Instantiate(destroyAnim, transform.position, Quaternion.identity);
                Destroy(anim, 1f);
                EventManager.PlaySound.Invoke(destroySound);
                gameObject.SetActive(false);
            }
            return;
        }
    }

    [System.Serializable]
    class Wall{
        internal int wallNumber;
        internal bool wallWasDestroyed;
    }
}