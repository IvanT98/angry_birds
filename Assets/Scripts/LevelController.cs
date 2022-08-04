using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Enemy[] _enemies;

    [SerializeField] private string nextLevelName = "";
    
    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (nextLevelName == "")
        {
            return;
        }
        
        if (_enemies.Any(enemy => enemy))
        {
            return;
        }

        SceneManager.LoadScene(nextLevelName);
    }
}
