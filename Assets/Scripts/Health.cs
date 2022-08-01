using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHp = 1;
    public GameObject enemyPoint, indicator;
    [HideInInspector] public int curHp;
    public Image hpBar;
    public Animator animator;

    private void Start()
    {
        curHp = maxHp;
        indicator.SetActive(false);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(curHp != maxHp)
        {
            indicator.SetActive(true);
        }

        hpBar.fillAmount = 1f / maxHp * curHp;

        if(curHp <= 0)
        {
            animator.enabled = false;
            indicator.SetActive(false);
            Destroy(enemyPoint);
        }
    }
}
