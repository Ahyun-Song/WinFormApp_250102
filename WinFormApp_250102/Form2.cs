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
            int[] fileValues;

            // 파일을 읽고 배열에 저장 (변환)
            if (ReadFileLines(out filePath, out fileValues))
            {
                // 파일을 정상적으로 읽었을 때 출력
                MessageBox.Show("File successfully read!\nFile Path: " + filePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 파일 내용을 표시
                string fileContent = string.Join(Environment.NewLine, fileLines);
                MessageBox.Show("File Content:\n" + fileContent, "File Content", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to read or convert the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 파일 경로와 내용을 배열로 반환하는 메서드
        // <param name="filePath">읽을 파일 경로 (out)</param>
        // <param name="fileValues">파일의 각 줄을 저장하는 배열 (int로 변환된 값)</param>
        // <returns>파일 읽기 및 변환 성공 여부</returns>
        private bool ReadFileLines(out string filePath, out int[] fileValues)
        {
            filePath = @"C:\Users\송철\source\repos\WinFormApp_250102\input.txt";  // 솔루션 폴더 내의 파일 경로
            fileValues = null;

            try
            {
                // 파일 경로가 유효한지 확인
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("The selected file does not exist.");
                }

                // 파일 내용 읽기
                string content = File.ReadAllText(filePath);
                string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None); // 줄 단위로 분리

                // 각 줄을 int로 변환
                fileValues = new int[lines.Length];
                bool isAllConverted = true;  // 모든 값이 성공적으로 변환되었는지 체크

                // 변환 성공한 값과 실패한 값을 따로 처리
                for (int i = 0; i < lines.Length; i++)
                {
                    // 변환 실패 시 예외 발생
                    if (int.TryParse(lines[i], out fileValues[i]))
                    {
                        // 변환된 값 출력 (성공)
                        // MessageBox로 각 줄을 변환한 값을 표시
                        MessageBox.Show($"Line {i + 1}: {fileValues[i]}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // 변환 실패 시 오류 메시지 출력
                        MessageBox.Show($"Error: Line {i + 1} could not be converted to an integer: \"{lines[i]}\".", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isAllConverted = false;
                    }
                }

                // 성공적으로 변환된 값만 출력
                if (isAllConverted)
                {
                    MessageBox.Show("All lines were successfully converted to integers.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return isAllConverted; // 변환 여부 반환
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
