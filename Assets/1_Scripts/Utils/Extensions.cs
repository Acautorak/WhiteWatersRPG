using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;
using Random = System.Random;

public static class Extensions
{
    // UI SetListener extension that removes previous and then adds new listener
    // (this version is for onClick etc.)
    public static void SetListener(this UnityEvent uEvent, UnityAction call)
    {
        uEvent.RemoveAllListeners();
        uEvent.AddListener(call);
    }

    public static void SetListener(this UnityEvent<Vector2> scrollEvent, UnityAction<Vector2> call)
    {
        scrollEvent.RemoveAllListeners();
        scrollEvent.AddListener(call);
    }

    // UI SetListener extension that removes previous and then adds new listener
    // (this version is for onEndEdit, onValueChanged etc.)
    public static void SetListener<T>(this UnityEvent<T> uEvent, UnityAction<T> call)
    {
        uEvent.RemoveAllListeners();
        uEvent.AddListener(call);
    }

    public static float RoundToDec(this float number, int decimals)
    {
        float result = Mathf.Round(number * Mathf.Pow(10, decimals)) / Mathf.Pow(10, decimals);

        return result;
    }

    public static string SecondsToTimeFormat(this float seconds)
    {
        const int secondsInTenMinutes = 10 * 60;
        return seconds switch
        {
            >= secondsInTenMinutes => new DateTime(TimeSpan.FromSeconds(seconds).Ticks).ToString("mm:ss"),
            >= 0 => new DateTime(TimeSpan.FromSeconds(seconds).Ticks).ToString("m:ss"),
            _ => "0:00"
        };
    }

    public static void ParentAndReset(this Transform trans, Transform parent, bool resetScale = true)
    {
        trans.parent = parent;
        trans.Reset(resetScale);
    }

    public static void Reset(this Transform trans, bool resetScale = true)
    {
        trans.localPosition = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        if (resetScale)
            trans.localScale = Vector3.zero;
    }

    public static bool TryGetComponent<T>(this GameObject go, out T result)
    {
        result = go.GetComponent<T>();
        return result != null;
    }

    public static bool TryGetComponentInChildren<T>(this GameObject go, out T result)
    {
        result = go.GetComponentInChildren<T>(true);
        return result != null;
    }

    public static bool TryGetComponents<T>(this GameObject go, out T[] result)
    {
        result = go.GetComponents<T>();
        return result != null;
    }

    public static bool TryGetComponentsInChildren<T>(this GameObject go, out T[] result)
    {
        result = go.GetComponentsInChildren<T>();
        return result != null;
    }

    public static List<T> GetComponentsInChildrenRecursively<T>(this Transform transform, List<T> componentList)
    {
        foreach (Transform t in transform)
        {
            T[] components = t.GetComponents<T>();

            foreach (T component in components)
            {
                if (component != null)
                {
                    componentList.Add(component);
                }
            }

            GetComponentsInChildrenRecursively(t, componentList);
        }

        return componentList;
    }

    public static bool TryGetElementAtIndex<T>(this object[] par, int index, out T obj)
    {
        if (par != null && par.Length > index && par[index] is T temp)
        {
            obj = temp;
            return true;
        }
        else
        {
            obj = default;
            return false;
        }
    }

    public static T GetRandomElement<T>(this T[] arr)
    {
        if (arr == null || arr.Length == 0) return default;
        var r = new Random();
        return arr[r.Next(0, arr.Length)];
    }

    public static string Repeat(this char baseString, int length)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            sb.Append(baseString);
        }

        return sb.ToString();
    }

    public static T DeepCopy<T>(this T self)
    {
        var serialized = JsonConvert.SerializeObject(self);
        return JsonConvert.DeserializeObject<T>(serialized);
    }

    public static void Shuffle<T>(this IList<T> list, string seed)
    {
        int? seedInt = null;
        if (seed != null)
        {
            var md5Hasher = MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(seed));
            seedInt = BitConverter.ToInt32(hashed, 0);
        }

        list.Shuffle(seedInt);
    }

    public static void Shuffle<T>(this IList<T> list, int? seedInt = null)
    {
        var rng = seedInt.HasValue ? new Random(seedInt.Value) : new Random();
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    public static void ClearTransform(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }

    public static bool IsNullOrWhiteSpace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static DateTime Clamp(this DateTime input, DateTime min, DateTime max)
    {
        return input < min ? min : input > max ? max : input;
    }

    /// <summary></summary>
    /// <param name="rectTransform"></param>
    /// <param name="worldPosition"></param>
    /// <param name="worldCamera"> The camera used to render the scene. </param>
    /// <param name="canvasCamera"> The camera used to render the canvas. If the canvas render mode is set to Screen Space - Overlay, this parameter can be null. </param>
    /// <returns></returns>
    public static Vector2 WorldToCanvasPoint(
        this RectTransform rectTransform,
        Vector3 worldPosition,
        [NotNull] Camera worldCamera,
        Camera canvasCamera)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            worldCamera.WorldToScreenPoint(worldPosition),
            canvasCamera,
            out var canvasPoint);

        return canvasPoint;
    }
}