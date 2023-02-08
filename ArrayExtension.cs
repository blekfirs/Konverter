using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSharpProb
{
    //Класс для метода раширения
    public static class ArrayExtension
    {
        //Извлечение подмассива из массива
        public static T[] SubArray<T>(this T[] array, int offset, int length) =>
            new ArraySegment<T>(array, offset, length)
                .ToArray();
    }
}
