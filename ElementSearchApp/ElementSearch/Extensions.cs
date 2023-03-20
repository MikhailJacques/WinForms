//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ElementSearch
//{
//    public static class Extensions
//    {
//        public static TResult Let<TSource, TResult>(this TSource source, Func<TSource, TResult> func) where TSource : class
//        {
//            return source != null ? func(source) : default(TResult);
//        }
//    }
//}