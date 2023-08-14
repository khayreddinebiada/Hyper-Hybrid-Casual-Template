using System.Collections;
using System.Collections.Generic;

namespace HCEngine.DI
{
    public class InjectorDependencies : IEnumerable
    {
        private List<IDependency> _dependencies;

        public IDependency this[int index] => _dependencies[index];

        public int Count => _dependencies.Count;

        public InjectorDependencies()
        {
            _dependencies = new List<IDependency>();
        }

        public InjectorDependencies(IEnumerable<IDependency> dependencies)
        {
            if (dependencies == null) throw new System.ArgumentNullException("The dependencies has a null value!.");

            _dependencies = new List<IDependency>(dependencies);
        }

        public InjectorDependencies(IEnumerable<UnityEngine.Object> dependencies)
        {
            if (dependencies == null) throw new System.ArgumentNullException("The dependencies has a null value!.");

            _dependencies = new List<IDependency>();
            foreach (UnityEngine.Object obj in dependencies)
            {
                IDependency dependency = obj as IDependency;
                if (dependency != null) _dependencies.Add(dependency);
            }
        }

        public void InjectAll()
        {
            foreach (IDependency dependency in _dependencies)
            {
                if (dependency != null && !dependency.Equals(null)) dependency.Inject();
            }
        }

        public void AddDependency(IDependency dependency)
        {
            _dependencies.Add(dependency);
        }

        public void AddDependencies(IEnumerable<IDependency> dependency)
        {
            _dependencies.AddRange(dependency);
        }

        public IEnumerator GetEnumerator()
        {
            return _dependencies.GetEnumerator();
        }
    }
}