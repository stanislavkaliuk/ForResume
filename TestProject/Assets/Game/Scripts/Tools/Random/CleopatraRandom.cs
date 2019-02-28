using System;
using Random = UnityEngine.Random;

namespace Tools
{

    public static class CleopatraRandom
    {
        private static bool _initialized;

        /// <summary>
        /// Get value [0,1]
        /// </summary>
        public static float Value
        {
            get
            {
                if (!_initialized)
                {
                    Init();
                }

                return Random.value;
            }
        }

        public static int ArraySize { private get; set; }

        /// <summary>
        /// Get array with item [0,1]
        /// </summary>
        public static float[] ArrayValue
        {
            get
            {
                if (!_initialized)
                {
                    Init();
                }

                if (ArraySize == 0)
                    ArraySize = 10;

                var array = new float[ArraySize];
                for (int i = 0; i < ArraySize; i++)
                {
                    array[i] = Random.value;
                }

                return array;
            }
        }

        private static void Init()
        {
            DateTime baseDateTime = new DateTime(1970, 1, 1);
            TimeSpan diff = DateTime.Now - baseDateTime;
            Random.InitState((int)diff.TotalMilliseconds);
            for (int i = 0; i < 10; i++)
            {
                float val = Random.value;
            }
            _initialized = true;

        }
    }
}
