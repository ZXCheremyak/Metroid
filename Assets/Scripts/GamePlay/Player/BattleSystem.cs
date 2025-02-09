using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] Transform attackPoint;

    [SerializeField] float attackCooldown;

    [SerializeField] float attackRange;

    [SerializeField]Element[] elements;

    public List<Element> availableElements = new List<Element>();

    [SerializeField]Element currentElement;

    int currentElementNumber = 0;

    PlayerParameters parametersComponent;

    [SerializeField] bool canAttack;

    Animator animator;

    AnimationPlayer animPlayer;

    [SerializeField]LayerMask enemyLayer;

    [SerializeField] AudioClip elementGain, attackSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        EventManager.BossDied.AddListener(AddElement);
        EventManager.Save.AddListener(Save);
        EventManager.Load.AddListener(Load);
        parametersComponent = GetComponent<PlayerParameters>();
        
        currentElement = availableElements[0];
        parametersComponent.element = currentElement;
        
        animPlayer = GetComponent<AnimationPlayer>();
        SetParticlesColor();
    }

    void Update()
    {
        if(!parametersComponent.actionsAllowed || !canAttack) return;

        SwapElements();
        StartCoroutine("Attack");
    }

    void SwapElements()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            currentElementNumber = (currentElementNumber + 1) % availableElements.Count;
            currentElement = availableElements[currentElementNumber];
            parametersComponent.element = currentElement;
            EventManager.PlaySound.Invoke(currentElement.swapSound);
            SetParticlesColor();
        }
    }

    void SetParticlesColor(){
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        var psmain = ps.main;
        psmain.startColor = currentElement.particleColor;
    }

    public void AddElement(Parameters parameters){
        foreach(Element element in availableElements)
        {
            if(element == parameters.element) return;
        }
        availableElements.Add(parameters.element);
        EventManager.PlaySound.Invoke(elementGain);
        Save();
    }

    IEnumerator Attack()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            canAttack = false;
            Debug.Log("Attack");

            animPlayer.PlayAnimation("Attack");
            
            EventManager.PlaySound.Invoke(attackSound);

            animPlayer.allowOtherAnimations = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);


            foreach(Collider2D coll in colliders)
            {
                if(coll.gameObject.TryGetComponent<IHitable>(out IHitable target) && !coll.gameObject.CompareTag("Player"))
                {
                    target.TakeDamage(parametersComponent.damage * (parametersComponent.damageMultiplier + 1), currentElement);
                    GameObject particles = Instantiate(currentElement.particles, coll.transform.position, Quaternion.identity);
                    Destroy(particles, 1);
                    Debug.Log("AttackLanded");
                }
            }

            yield return new WaitForSeconds(0.2f);
            
            animPlayer.allowOtherAnimations = true;
            
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }

        

    }

    void Save(){
        ElementsContainer elementsContainer = new ElementsContainer();
        foreach(Element element in availableElements)
        {
            if(element.elementType == ElementType.Water)
            {
                elementsContainer.waterSaved = true;
            }
            if(element.elementType == ElementType.Earth)
            {
                elementsContainer.earthSaved = true;
            }
            if(element.elementType == ElementType.Fire)
            {
                elementsContainer.fireSaved = true;
            }
            if(element.elementType == ElementType.Air)
            {
                elementsContainer.airSaved = true;
            }
            Debug.Log(element.elementType + " saved");
        }
        Serializer.Save("elementsContainer", elementsContainer);
    }

    void Load(){
        if(!Serializer.CheckFileExistance("elementsContainer"))
        {
            availableElements.Add(elements[1]);
            return;
        }

        Debug.Log("Elements loaded");

        ElementsContainer elementsContainer = new ElementsContainer();
        elementsContainer = Serializer.Load<ElementsContainer>("elementsContainer");

        availableElements.Clear();

        if(elementsContainer.waterSaved)
        {
            availableElements.Add(elements[0]);
            Debug.Log("Water added");
        }
        if(elementsContainer.airSaved)
        {
            availableElements.Add(elements[1]);
            Debug.Log("air added");
        }
        if(elementsContainer.earthSaved)
        {
            availableElements.Add(elements[2]);
            Debug.Log("earth added");
        }
        if(elementsContainer.fireSaved)
        {
            availableElements.Add(elements[3]);
            Debug.Log("Fire added");
        }
    }

    [System.Serializable]
    class ElementsContainer{
        internal bool waterSaved, earthSaved, fireSaved, airSaved;
    }
}