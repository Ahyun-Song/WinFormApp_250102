using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApp_250102
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TestArrayMethods();
        }

        // ref를 사용하여 배열 초기화
        private void InitializeArrayWithRef(ref int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }
        }
        
        // out을 사용하여 배열 생성 및 초기화
        private void InitializeArrayWithOut(out int[] array, int size)
        {
            array = new int[size]; // 새 배열 생성
            for (int i = 0; i < size; i++)
            {
                array[i] = i + 1;
            }
        }
        

        // 배열 메서드 테스트
        private void TestArrayMethods()
        {
            // ref 테스트
            int[] existingArray = new int[5];
            InitializeArrayWithRef(ref existingArray);
            MessageBox.Show("Ref Initialized Array: " + string.Join(", ", existingArray));

            // out 테스트
            int[] newArray;
            InitializeArrayWithOut(out newArray, 8);
            MessageBox.Show("Out Initialized Array: " + string.Join(", ", newArray));
        }
    }
}
