namespace SagaDB.DEMIC
{
    using SagaLib;
    using SagaLib.VirtualFileSystem;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="ModelFactory" />.
    /// </summary>
    public class ModelFactory : Singleton<ModelFactory>
    {
        /// <summary>
        /// Defines the models.
        /// </summary>
        private Dictionary<uint, Model> models = new Dictionary<uint, Model>();

        /// <summary>
        /// Gets the Models.
        /// </summary>
        public Dictionary<uint, Model> Models
        {
            get
            {
                return this.models;
            }
        }

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="encoding">The encoding<see cref="Encoding"/>.</param>
        public void Init(string path, Encoding encoding)
        {
            StreamReader streamReader = new StreamReader(Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path), encoding);
            int num1 = 0;
            string label = "Loading Chip model database";
            Logger.ProgressBarShow(0U, (uint)streamReader.BaseStream.Length, label);
            DateTime now = DateTime.Now;
            bool flag = false;
            uint index1 = 0;
            int num2 = 0;
            while (!streamReader.EndOfStream)
            {
                string str = streamReader.ReadLine();
                try
                {
                    if (!(str == ""))
                    {
                        if (!(str.Substring(0, 1) == "#"))
                        {
                            string[] strArray = str.Split(',');
                            for (int index2 = 0; index2 < strArray.Length; ++index2)
                            {
                                if (strArray[index2] == "" || strArray[index2].ToLower() == "null")
                                    strArray[index2] = "0";
                            }
                            if (strArray[0] == "model")
                            {
                                flag = true;
                                Model model = new Model();
                                model.ID = uint.Parse(strArray[1]);
                                index1 = model.ID;
                                this.models.Add(model.ID, model);
                                num2 = 0;
                                ++num1;
                            }
                            else if (flag)
                            {
                                Model model = this.models[index1];
                                for (int index2 = 0; index2 < strArray.Length; ++index2)
                                {
                                    if (strArray[index2] != "0")
                                        model.Cells.Add(new byte[2]
                                        {
                      (byte) index2,
                      (byte) num2
                                        });
                                    if (byte.Parse(strArray[index2]) > (byte)100)
                                    {
                                        model.CenterX = (byte)index2;
                                        model.CenterY = (byte)num2;
                                    }
                                }
                                ++num2;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Error on parsing mob db!\r\nat line:" + str);
                    Logger.ShowError(ex);
                }
            }
            Logger.ProgressBarHide(num1.ToString() + " models loaded.");
            streamReader.Close();
        }
    }
}
