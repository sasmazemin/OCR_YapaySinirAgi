using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_YapaySinirAgi
{
    class OrnekVeri
    {
        public static double[][] Girdiler =
   {
        // A
        new double[]
        {
            0,1,1,1,0,
            1,0,0,0,1,
            1,0,0,0,1,
            1,1,1,1,1,
            1,0,0,0,1,
            1,0,0,0,1,
            1,0,0,0,1
        },
        // B
        new double[]
        {
            1,1,1,1,0,
            1,0,0,0,1,
            1,0,0,0,1,
            1,1,1,1,0,
            1,0,0,0,1,
            1,0,0,0,1,
            1,1,1,1,0
        },
        // C
        new double[]
        {
            0,1,1,1,1,
            1,0,0,0,0,
            1,0,0,0,0,
            1,0,0,0,0,
            1,0,0,0,0,
            1,0,0,0,0,
            0,1,1,1,1
        },
        // D
        new double[]
        {
            1,1,1,0,0,
            1,0,0,1,0,
            1,0,0,0,1,
            1,0,0,0,1,
            1,0,0,0,1,
            1,0,0,1,0,
            1,1,1,0,0
        },
        // E
        new double[]
        {
            1,1,1,1,1,
            1,0,0,0,0,
            1,0,0,0,0,
            1,1,1,1,0,
            1,0,0,0,0,
            1,0,0,0,0,
            1,1,1,1,1
        }
    };

        public static double[][] Ciktilar =
        {
        new double[] {1,0,0,0,0}, // A
        new double[] {0,1,0,0,0}, // B
        new double[] {0,0,1,0,0}, // C
        new double[] {0,0,0,1,0}, // D
        new double[] {0,0,0,0,1}  // E
    };
    }
}
