using System;
namespace Util
{
    public static class TextUtils
    {
        public static bool isAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }
    }
}

