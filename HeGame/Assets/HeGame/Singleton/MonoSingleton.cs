using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HeGame
{
    /// <summary>
    /// Be aware this will not prevent a non singleton constructor
    ///   such as `T myT = new T();`
    /// To prevent that, add `protected T () {}` to your singleton class.
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /** the m_hierarchyXXX is used to set the path and name, 
         * not using the attributes and reflection just for the performance reason */
        protected static string m_hierarchyPath = "SingletonPath";

        protected static string m_hierarchyName = "SingletonName";

        private static T m_instance;

        private static object m_syncRoot = new object();

        private static bool m_applicationIsQuitting = false;

        public static T Instance
        {
            get
            {
                if (m_applicationIsQuitting)
                {
                    Log.Warning("[Singleton] Instance '" + typeof(T) +
                        "' already destroyed on application quit." +
                        " Won't create again - returning null.");
                    return null;
                }

                lock (m_syncRoot)
                {
                    if (m_instance == null)
                    {
                        m_instance = (T)FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {Log.Error("[Singleton] Something went really wrong " +
                                " - there should never be more than 1 singleton!" +
                                " Reopening the scene might fix it.");
                            return m_instance;
                        }

                        if (m_instance == null)
                        {
                            MemberInfo memberInfo = typeof(T);

                            foreach (Attribute attr in memberInfo.GetCustomAttributes(true))
                            {
                                if (attr is HierarchyAttribute)
                                {
                                    m_hierarchyPath = ((HierarchyAttribute)attr).Path + "/" + m_hierarchyPath;
                                    m_hierarchyName = ((HierarchyAttribute)attr).Name;
                                    break;
                                }
                            }

                            m_instance = CreateGetGameobjectAddComponent(m_hierarchyPath);

                            m_instance.name = m_hierarchyName;

                            //Log.Info("[Singleton] An instance of " + typeof(T) +
                            //    " is needed in the scene, so '" + singleton +
                            //    "' was created with DontDestroyOnLoad.");
                        }
                        else
                        {
                            Log.Debug("[Singleton] Using instance already created: " +
                                m_instance.gameObject.name);
                        }
                    }
                    return m_instance;
                }
            }
        }

        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed, 
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        public void OnDestroy()
        {
            m_applicationIsQuitting = true;
        }

        protected static T CreateGetGameobjectAddComponent(string path)
        {
            if (path == null || path.Length == 0)
            {
                return null;
            }

            string[] subPath = path.Split('/');
            if (subPath == null || subPath.Length == 0)
            {
                return null;
            }

            GameObject singleton = GetGameObject(null, subPath);

            if (singleton == null)
            {
                singleton = new GameObject();
                singleton.name = "(singleton) " + typeof(T).ToString();
                UnityEngine.Object.DontDestroyOnLoad(singleton);
            }

            return singleton.AddComponent<T>();
        }

        protected static GameObject GetGameObject(GameObject root, string[] subPath)
        {
            GameObject subRoot = null;

            if(root == null)
            {
                subRoot = GameObject.Find(subPath[0]);
            }
            else
            {
                Transform subChild = root.transform.Find(subPath[0]);
                if(subChild)
                {
                    subRoot = subChild.gameObject;
                }
            }

            if(subRoot == null)
            {
                subRoot = new GameObject(subPath[0]);
                if(root != null)
                {
                    subRoot.transform.parent = root.transform;
                }
                else
                {
                    GameObject.DontDestroyOnLoad(subRoot);
                }
            }

            if(subPath.Length == 1)
            {
                return subRoot;
            }
            else
            {
                subPath = subPath.Skip(1).ToArray();
                return GetGameObject(subRoot, subPath);
            }
        }
    }
}
