using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Utils
{
    public static class MyMonoBehaviourUtils
    {
        public static T GetComponentNotNull<T>(this GameObject @this) where T : class
        {
            var component = @this.GetComponent<T>();
            Assert.IsNotNull(component, $"There is no component of type {typeof(T)} in object {@this.name}");

            return component;
        }

        public static T GetComponentNotNull<T>(this MonoBehaviour @this) where T: class
        {
            return @this.gameObject.GetComponentNotNull<T>();
        }

        public static T GetComponentInChildrenNotNull<T>(this GameObject @this) where T : class
        {
            var component = @this.GetComponentInChildren<T>();
            Assert.IsNotNull(component, $"There is no component of type {typeof(T)} in object {@this.name}");

            return component;
        }

        public static T GetComponentInChildrenNotNull<T>(this MonoBehaviour @this) where T: class
        {
            return @this.gameObject.GetComponentNotNull<T>();
        }

    }
}
