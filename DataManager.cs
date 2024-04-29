
using Newtonsoft.Json;
using System.Runtime.CompilerServices;


namespace Sparta_Dungeon
{
    public class DataManager
    {
        public void Save<T>(T data)
        {
            string json = JsonConvert.SerializeObject(data);

            // 파일명과 디렉터리
            string fileName = $"{DefaultPath()}\\{typeof(T)}.json";

            FileWrite(fileName, json);
        }

        public T? Load<T>()
        {
            string path = $"{DefaultPath()}\\{typeof(T)}.json";

            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Exists)
            {
                string json = FileRead(path);

                T data = JsonConvert.DeserializeObject<T>(json);

                return data;
            }

            return default(T);
        }

        private void FileWrite(string path, string data)
        {
            FileStream fileStream = new FileStream(
                path,
                FileMode.Create,
                FileAccess.Write);

            StreamWriter writer = new StreamWriter(fileStream, System.Text.Encoding.UTF8);

            writer.Write(data);

            writer.Close();
            fileStream.Close();
        }

        private string FileRead(string path)
        {
            FileInfo file = new FileInfo(path);
            string json = "";

            // 파일 존재 시
            if (file.Exists)
            {
                FileStream fileStream = new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.Read);

                StreamReader reader = new StreamReader(fileStream, System.Text.Encoding.UTF8);


                while(reader.Peek() > -1)
                {
                    json += reader.ReadLine();
                }
                reader.Close();
                fileStream.Close();
            }

            return json;

        }
        private string DefaultPath()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}
