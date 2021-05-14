using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CalculatorWPF
{
    public static class ExpressionsHelpers
    {

        /// <summary>
        /// Expression helper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lamba"></param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> lamba)
        {
            return lamba.Compile().Invoke();
        }
        public static void SetPropertyValue<T>(this Expression<Func<T>> lamba, T value)
        {
            var expression = lamba.Body as MemberExpression;
            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();
            propertyInfo.SetValue(target, value);
        }
    }
}
