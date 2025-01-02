using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApp_250102
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            // 폼 로드 시 파일을 읽음
            ReadFileOnLoad();
        }

        // 폼이 로드될 때 파일을 읽고 배열에 저장하는 메서드
        private void ReadFileOnLoad()
        {
            string filePath;
            string[] fileLines;

            // 파일을 읽고 배열에 저장
            if (ReadFileLines(out filePath, out fileLines))
            {
                MessageBox.Show("File successfully read!\nFile Path: " + filePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 파일 내용을 표시
                string fileContent = string.Join(Environment.NewLine, fileLines);
                MessageBox.Show("File Content:\n" + fileContent, "File Content", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to read the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 파일 경로와 내용을 배열로 반환하는 메서드
        // <param name="filePath">읽을 파일 경로 (out)</param>
        // <param name="fileLines">파일의 각 줄을 저장하는 배열 (out)</param>
        // <returns>파일 읽기 성공 여부</returns>
        private bool ReadFileLines(out string filePath, out string[] fileLines)
        {
            filePath = @"C:\Users\송철\source\repos\WinFormApp_250102\input.txt";  // 솔루션 폴더 내의 파일 경로
            fileLines = null;

            try
            {
                // 파일 경로가 유효한지 확인
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("The selected file does not exist.");
                }

                // 파일 내용 읽기
                string content = File.ReadAllText(filePath);
                fileLines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None); // 줄 단위로 분리

                return true; // 성공
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Error: Access to the file is denied.\n" + ex.Message, "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MessageBox.Show("File reading process completed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return false; // 실패
        }
    }
}
