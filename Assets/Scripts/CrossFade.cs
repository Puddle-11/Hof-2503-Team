using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class CrossFade : MonoBehaviour
{
    public static CrossFade instance;
    public float crossFadeTime;
    public Image crossFadeObj;
    public bool crossFading;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void StartCrossFade(Action _middle, Action _end, float hangTime)
    {
        
        StartCoroutine(CFade(_middle, _end, hangTime));
        
    }
    
    public IEnumerator CFade(Action _middle, Action _end, float hangTime)
    {
        while (crossFading)
        {
            yield return null;
        }
        crossFading = true;
        float opacity = 0;
        while (opacity < 1)
        {
            opacity += Time.deltaTime * crossFadeTime;
            UpdateOpacity(opacity);
            yield return null;
        }
        opacity = 1;
        UpdateOpacity(opacity);
        _middle?.Invoke();
        yield return new WaitForSeconds(hangTime);
        while (opacity > 0)
        {
            opacity -= Time.deltaTime * crossFadeTime;
            UpdateOpacity(opacity);
            yield return null;
            
        }
        opacity = 0;
        UpdateOpacity(opacity);
        _end?.Invoke();
        crossFading = false;
    }
    
    void UpdateOpacity(float _val)
    {
        crossFadeObj.color = new Color(crossFadeObj.color.r, crossFadeObj.color.g, crossFadeObj.color.b, _val);

    }
}
