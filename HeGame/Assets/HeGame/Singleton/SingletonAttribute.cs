using System;

namespace HeGame
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HierarchyAttribute : Attribute
    {
        #region  Attributes and Properties
        public string Path { get; private set; }

        public string Name { get; private set; }
        #endregion

        #region Public Methods
        public HierarchyAttribute(string path, string name)
        {
            this.Path = path;
            this.Name = name;
        }
        #endregion
    }
}

