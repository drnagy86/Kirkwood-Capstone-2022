using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace LogicLayerTests
{
    public static class ReflectionHelper
    {
        public static void AssertReflectiveEquals<T>(T expected, T actual)
        {
            if (expected == null && actual == null)
            {
                // Assert a pass just in case this is the first layer of recursion
                Assert.IsTrue(true);
                return;
            }
            else if (expected == null || actual == null)
            {
                Assert.Fail("One of the two objects is null");
            }
            else
            {
                Type tExpected = expected.GetType();
                Type tActual = actual.GetType();
                if (!tExpected.FullName.Equals(tActual.FullName))
                {
                    Assert.Fail("The types of the two objects do not match");
                }
                else if (tExpected.IsValueType || tExpected.IsPrimitive || tExpected.Equals(typeof(string)))
                {
                    Assert.AreEqual(expected, actual);
                }
                else
                {
                    foreach (PropertyInfo prop in tExpected.GetProperties(System.Reflection.BindingFlags.Public))
                    {
                        if (prop.GetIndexParameters().Length > 0)
                        {
                            DoReflectiveAssert(expected, actual, prop, new Object[] { 0 });
                        }
                        else
                        {
                            DoReflectiveAssert(expected, actual, prop, null);
                        }

                    }
                    foreach (FieldInfo prop in tExpected.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
                    {
                        DoReflectiveAssert(expected, actual, prop, null);
                    }
                }
            }
        }

        public static void AssertReflectiveEqualsEnumerable<T>(IEnumerable<T> expectedList, IEnumerable<T> actualList)
        {
            if (expectedList == null && actualList == null)
            {
                // Assert a pass just in case this is the first layer of recursion
                Assert.IsTrue(true);
                return;
            }
            else if (expectedList == null || actualList == null)
            {
                Assert.Fail("One of the two objects is null");
            }
            if (expectedList.Count() != actualList.Count())
            {
                Assert.Fail("Enumerables are different sizes");
            }
            else
            {
                for (int i = 0; i < expectedList.Count(); i++)
                {

                    T expected = expectedList.ElementAt(i);
                    T actual = actualList.ElementAt(i);
                    AssertReflectiveEquals(expected, actual);
                }
            }
        }
        private static void DoReflectiveAssert<T>(T expected, T actual, MemberInfo prop, object[] indexes, bool retry = false)
        {
            try
            {
                if (prop is FieldInfo property)
                {
                    var expectedValue = property.GetValue(expected);
                    var actualValue = property.GetValue(actual);

                    if (expectedValue == null && actualValue == null)
                    {
                        return;
                    }
                    var type = expectedValue.GetType();
                    bool passed = false;
                    try
                    {
                        AssertReflectiveEqualsEnumerable((IEnumerable<object>)expectedValue, (IEnumerable<object>)actualValue);
                        passed = true;
                    }
                    catch (Exception ex)
                    {
                    }
                    if (passed)
                    {
                        return;
                    }
                    if (!type.IsPrimitive && !(type.Equals(typeof(String))))
                    {
                        AssertReflectiveEquals(expectedValue, actualValue);
                        return;
                    }
                    Assert.AreEqual(expectedValue, actualValue);
                }
                else
                {
                    var PropInfo = (PropertyInfo)prop;
                    var expectedValue = PropInfo.GetValue(expected, indexes);

                    var actualValue = PropInfo.GetValue(actual, indexes);
                    if (expectedValue == null && actualValue == null)
                    {
                        return;
                    }
                    var type = expectedValue.GetType();
                    bool passed = false;
                    try
                    {
                        AssertReflectiveEqualsEnumerable((IEnumerable<object>)expectedValue, (IEnumerable<object>)actualValue);
                        passed = true;
                    }
                    catch (Exception ex)
                    {
                    }
                    if (passed)
                    {
                        return;
                    }
                    if (!type.IsPrimitive && !(type.Equals(typeof(String))))
                    {
                        AssertReflectiveEquals(expectedValue, actualValue);
                        return;
                    }
                    Assert.AreEqual(expectedValue, actualValue);
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                // Necessary for empty strings.
                if (!retry)
                {
                    if (indexes != null)
                    {
                        DoReflectiveAssert(expected, actual, prop, null, true);
                    }
                    else
                    {
                        DoReflectiveAssert(expected, actual, prop, new Object[] { 0 }, true);
                    }

                }
            }

        }


    }
}
