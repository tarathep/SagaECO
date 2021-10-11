namespace SagaMap.Manager
{
    using Microsoft.CSharp;
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Scripting;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="ScriptManager" />.
    /// </summary>
    public class ScriptManager : Singleton<ScriptManager>
    {
        /// <summary>
        /// Defines the timers.
        /// </summary>
        private Dictionary<string, MultiRunTask> timers = new Dictionary<string, MultiRunTask>();

        /// <summary>
        /// Defines the events.
        /// </summary>
        private Dictionary<uint, Event> events;

        /// <summary>
        /// Defines the path.
        /// </summary>
        private string path;

        /// <summary>
        /// Defines the variableHolder.
        /// </summary>
        private ActorPC variableHolder;

        /// <summary>
        /// Gets the Events.
        /// </summary>
        public Dictionary<uint, Event> Events
        {
            get
            {
                return this.events;
            }
        }

        /// <summary>
        /// Gets or sets the VariableHolder.
        /// </summary>
        public ActorPC VariableHolder
        {
            get
            {
                return this.variableHolder;
            }
            set
            {
                this.variableHolder = value;
            }
        }

        /// <summary>
        /// Gets the Timers.
        /// </summary>
        public Dictionary<string, MultiRunTask> Timers
        {
            get
            {
                return this.timers;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptManager"/> class.
        /// </summary>
        public ScriptManager()
        {
            this.events = new Dictionary<uint, Event>();
        }

        /// <summary>
        /// The ReloadScript.
        /// </summary>
        public void ReloadScript()
        {
            ClientManager.noCheckDeadLock = true;
            try
            {
                this.events.Clear();
                this.LoadScript(this.path);
            }
            catch
            {
            }
            ClientManager.noCheckDeadLock = false;
        }

        /// <summary>
        /// The LoadScript.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        public void LoadScript(string path)
        {
            Logger.ShowInfo("Loading uncompiled scripts");
            CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider((IDictionary<string, string>)new Dictionary<string, string>()
      {
        {
          "CompilerVersion",
          "v3.5"
        }
      });
            int num1 = 0;
            this.path = path;
            try
            {
                string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
                if (files.Length > 0)
                {
                    Assembly newAssembly = this.CompileScript(files, (CodeDomProvider)csharpCodeProvider);
                    if (newAssembly != null)
                    {
                        int num2 = this.LoadAssembly(newAssembly);
                        Logger.ShowInfo(string.Format("Containing {0} Events", (object)num2));
                        num1 += num2;
                    }
                }
                Logger.ShowInfo("Loading compiled scripts....");
                foreach (string file in Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories))
                {
                    Assembly newAssembly = Assembly.LoadFile(Path.GetFullPath(file));
                    if (newAssembly != null)
                    {
                        int num2 = this.LoadAssembly(newAssembly);
                        Logger.ShowInfo(string.Format("Loading {1}, Containing {0} Events", (object)num2, (object)file));
                        num1 += num2;
                    }
                }
                if (!this.events.ContainsKey(12001501U))
                    this.events.Add(12001501U, (Event)new DungeonNorth());
                if (!this.events.ContainsKey(12001502U))
                    this.events.Add(12001502U, (Event)new DungeonEast());
                if (!this.events.ContainsKey(12001503U))
                    this.events.Add(12001503U, (Event)new DungeonSouth());
                if (!this.events.ContainsKey(12001504U))
                    this.events.Add(12001504U, (Event)new DungeonWest());
                if (!this.events.ContainsKey(12001505U))
                    this.events.Add(12001505U, (Event)new DungeonExit());
                if (!this.events.ContainsKey(4043309056U))
                    this.events.Add(4043309056U, (Event)new WestFortGate());
                if (!this.events.ContainsKey(4043309057U))
                    this.events.Add(4043309057U, (Event)new WestFortField());
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            Logger.ShowInfo(string.Format("Totally {0} Events Added", (object)num1));
        }

        /// <summary>
        /// The CompileScript.
        /// </summary>
        /// <param name="Source">The Source<see cref="string[]"/>.</param>
        /// <param name="Provider">The Provider<see cref="CodeDomProvider"/>.</param>
        /// <returns>The <see cref="Assembly"/>.</returns>
        private Assembly CompileScript(string[] Source, CodeDomProvider Provider)
        {
            CompilerParameters options = new CompilerParameters();
            options.CompilerOptions = "/target:library /optimize";
            options.GenerateExecutable = false;
            options.GenerateInMemory = true;
            options.IncludeDebugInformation = true;
            options.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Reference Assemblies\\Microsoft\\Framework\\v3.5\\System.Data.DataSetExtensions.dll");
            options.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Reference Assemblies\\Microsoft\\Framework\\v3.5\\System.Core.dll");
            options.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Reference Assemblies\\Microsoft\\Framework\\v3.5\\System.Xml.Linq.dll");
            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.Add("SagaLib.dll");
            options.ReferencedAssemblies.Add("SagaDB.dll");
            options.ReferencedAssemblies.Add("SagaMap.exe");
            foreach (string str in Singleton<Configuration>.Instance.ScriptReference)
                options.ReferencedAssemblies.Add(str);
            CompilerResults compilerResults = Provider.CompileAssemblyFromFile(options, Source);
            if (compilerResults.Errors.Count <= 0)
                return compilerResults.CompiledAssembly;
            foreach (CompilerError error in (CollectionBase)compilerResults.Errors)
            {
                Logger.ShowError("Compile Error:" + error.ErrorText, (Logger)null);
                Logger.ShowError("File:" + error.FileName + ":" + (object)error.Line, (Logger)null);
            }
            return (Assembly)null;
        }

        /// <summary>
        /// The LoadAssembly.
        /// </summary>
        /// <param name="newAssembly">The newAssembly<see cref="Assembly"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int LoadAssembly(Assembly newAssembly)
        {
            Module[] modules = newAssembly.GetModules();
            int num = 0;
            foreach (Module module in modules)
            {
                foreach (Type type in module.GetTypes())
                {
                    try
                    {
                        if (!type.IsAbstract)
                        {
                            if (type.GetCustomAttributes(false).Length <= 0)
                            {
                                Event instance;
                                try
                                {
                                    instance = (Event)Activator.CreateInstance(type);
                                }
                                catch (Exception ex)
                                {
                                    continue;
                                }
                                if (!this.events.ContainsKey(instance.EventID) && instance.EventID != 0U)
                                    this.events.Add(instance.EventID, instance);
                                else if (instance.EventID != 0U)
                                    Logger.ShowWarning(string.Format("EventID:{0} already exists, Class:{1} droped", (object)instance.EventID, (object)type.FullName));
                            }
                            else
                                continue;
                        }
                        else
                            continue;
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                    ++num;
                }
            }
            return num;
        }

        /// <summary>
        /// The GetFreeIDSince.
        /// </summary>
        /// <param name="since">The since<see cref="uint"/>.</param>
        /// <param name="max">The max<see cref="int"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetFreeIDSince(uint since, int max)
        {
            for (uint key = since; (long)key < (long)since + (long)max; ++key)
            {
                if (!this.events.ContainsKey(key))
                    return key;
            }
            return uint.MaxValue;
        }
    }
}
