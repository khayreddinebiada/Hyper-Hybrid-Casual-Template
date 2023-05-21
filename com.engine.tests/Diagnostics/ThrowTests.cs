using System;
using UnityEngine;
using System.Security;
using NUnit.Framework;
using Engine.Diagnostics;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Test.Diagnostics
{
    public class ThrowTests
    {
        [Test]
        public void ThrowArgumentOutOfRangeException()
        {
            CatchException(() => ThrowHelper.ThrowArgumentOutOfRangeException(), typeof(ArgumentOutOfRangeException));
            CatchException(() => ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.dictionary), typeof(ArgumentOutOfRangeException));
            CatchException(() => ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.dictionary, ExceptionResource.Serialization_NullKey), typeof(ArgumentOutOfRangeException));
        }

        [Test]
        public void ThrowKeyNotFoundException()
        {
            CatchException(() => ThrowHelper.ThrowKeyNotFoundException(), typeof(KeyNotFoundException));
        }
        
        [Test]
        public void ThrowArgumentException()
        {
            CatchException(() => ThrowHelper.ThrowArgumentException(ExceptionResource.ArgumentOutOfRange_Index), typeof(ArgumentException));
            CatchException(() => ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate, ExceptionArgument.dictionary), typeof(ArgumentException));
        }

        [Test]
        public void ThrowArgumentNullException()
        {
            CatchException(() => ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary), typeof(ArgumentNullException));
        }

        [Test]
        public void ThrowInvalidOperationException()
        {
            CatchException(() => ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EmptyStack), typeof(InvalidOperationException));
        }

        [Test]
        public void ThrowSerializationException()
        {
            CatchException(() => ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_NullKey), typeof(SerializationException));
        }

        [Test]
        public void ThrowSecurityException()
        {
            CatchException(() => ThrowHelper.ThrowSecurityException(ExceptionResource.Serialization_NullKey), typeof(SecurityException));
        }

        [Test]
        public void ThrowNotSupportedException()
        {
            CatchException(() => ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet), typeof(NotSupportedException));
        }

        [Test]
        public void ThrowUnauthorizedAccessException()
        {
            CatchException(() => ThrowHelper.ThrowUnauthorizedAccessException(ExceptionResource.UnauthorizedAccess_RegistryNoWrite), typeof(UnauthorizedAccessException));
        }

        [Test]
        public void ThrowObjectDisposedException()
        {
            CatchException(() => ThrowHelper.ThrowObjectDisposedException("Name is Disposed", ExceptionResource.ObjectDisposed_RegKeyClosed), typeof(ObjectDisposedException));
        }

        private void CatchException(Action actionExption, Type exception)
        {
            try
            {
                actionExption?.Invoke();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.GetType() == exception);
                Debug.Log(e.Message);
            }
        }
    }
}
