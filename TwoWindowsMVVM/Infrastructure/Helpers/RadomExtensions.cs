using System;

namespace TwoWindowsMVVM.Infrastructure.Helpers
{
    internal static class RadomExtensions
    {
        public static T NextItem<T>(this Random rnd, params T[] items) => items[rnd.Next(items.Length)];
    }
}