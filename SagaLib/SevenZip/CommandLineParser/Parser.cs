namespace SevenZip.CommandLineParser
{
    using System;
    using System.Collections;

    /// <summary>
    /// Defines the <see cref="Parser" />.
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// Defines the NonSwitchStrings.
        /// </summary>
        public ArrayList NonSwitchStrings = new ArrayList();

        /// <summary>
        /// Defines the kSwitchID1.
        /// </summary>
        private const char kSwitchID1 = '-';

        /// <summary>
        /// Defines the kSwitchID2.
        /// </summary>
        private const char kSwitchID2 = '/';

        /// <summary>
        /// Defines the kSwitchMinus.
        /// </summary>
        private const char kSwitchMinus = '-';

        /// <summary>
        /// Defines the kStopSwitchParsing.
        /// </summary>
        private const string kStopSwitchParsing = "--";

        /// <summary>
        /// Defines the _switches.
        /// </summary>
        private SwitchResult[] _switches;

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class.
        /// </summary>
        /// <param name="numSwitches">The numSwitches<see cref="int"/>.</param>
        public Parser(int numSwitches)
        {
            this._switches = new SwitchResult[numSwitches];
            for (int index = 0; index < numSwitches; ++index)
                this._switches[index] = new SwitchResult();
        }

        /// <summary>
        /// The ParseString.
        /// </summary>
        /// <param name="srcString">The srcString<see cref="string"/>.</param>
        /// <param name="switchForms">The switchForms<see cref="SwitchForm[]"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool ParseString(string srcString, SwitchForm[] switchForms)
        {
            int length1 = srcString.Length;
            if (length1 == 0)
                return false;
            int index1 = 0;
            if (!Parser.IsItSwitchChar(srcString[index1]))
                return false;
            while (index1 < length1)
            {
                if (Parser.IsItSwitchChar(srcString[index1]))
                    ++index1;
                int index2 = 0;
                int num1 = -1;
                for (int index3 = 0; index3 < this._switches.Length; ++index3)
                {
                    int length2 = switchForms[index3].IDString.Length;
                    if (length2 > num1 && index1 + length2 <= length1 && string.Compare(switchForms[index3].IDString, 0, srcString, index1, length2, true) == 0)
                    {
                        index2 = index3;
                        num1 = length2;
                    }
                }
                if (num1 == -1)
                    throw new Exception("maxLen == kNoLen");
                SwitchResult switchResult = this._switches[index2];
                SwitchForm switchForm = switchForms[index2];
                if (!switchForm.Multi && switchResult.ThereIs)
                    throw new Exception("switch must be single");
                switchResult.ThereIs = true;
                index1 += num1;
                int num2 = length1 - index1;
                SwitchType type = switchForm.Type;
                switch (type)
                {
                    case SwitchType.PostMinus:
                        if (num2 == 0)
                        {
                            switchResult.WithMinus = false;
                            continue;
                        }
                        switchResult.WithMinus = srcString[index1] == '-';
                        if (switchResult.WithMinus)
                        {
                            ++index1;
                            continue;
                        }
                        continue;
                    case SwitchType.LimitedPostString:
                    case SwitchType.UnLimitedPostString:
                        int minLen = switchForm.MinLen;
                        if (num2 < minLen)
                            throw new Exception("switch is not full");
                        if (type == SwitchType.UnLimitedPostString)
                        {
                            switchResult.PostStrings.Add((object)srcString.Substring(index1));
                            return true;
                        }
                        string str = srcString.Substring(index1, minLen);
                        index1 += minLen;
                        for (int index3 = minLen; index3 < switchForm.MaxLen && index1 < length1; ++index1)
                        {
                            char c = srcString[index1];
                            if (!Parser.IsItSwitchChar(c))
                            {
                                str += (string)(object)c;
                                ++index3;
                            }
                            else
                                break;
                        }
                        switchResult.PostStrings.Add((object)str);
                        continue;
                    case SwitchType.PostChar:
                        if (num2 < switchForm.MinLen)
                            throw new Exception("switch is not full");
                        string postCharSet = switchForm.PostCharSet;
                        if (num2 == 0)
                        {
                            switchResult.PostCharIndex = -1;
                            continue;
                        }
                        int num3 = postCharSet.IndexOf(srcString[index1]);
                        if (num3 < 0)
                        {
                            switchResult.PostCharIndex = -1;
                            continue;
                        }
                        switchResult.PostCharIndex = num3;
                        ++index1;
                        continue;
                    default:
                        continue;
                }
            }
            return true;
        }

        /// <summary>
        /// The ParseStrings.
        /// </summary>
        /// <param name="switchForms">The switchForms<see cref="SwitchForm[]"/>.</param>
        /// <param name="commandStrings">The commandStrings<see cref="string[]"/>.</param>
        public void ParseStrings(SwitchForm[] switchForms, string[] commandStrings)
        {
            int length = commandStrings.Length;
            bool flag = false;
            for (int index = 0; index < length; ++index)
            {
                string commandString = commandStrings[index];
                if (flag)
                    this.NonSwitchStrings.Add((object)commandString);
                else if (commandString == "--")
                    flag = true;
                else if (!this.ParseString(commandString, switchForms))
                    this.NonSwitchStrings.Add((object)commandString);
            }
        }


        public SwitchResult this[int index]
        {
            get
            {
                return this._switches[index];
            }
        }
        /// <summary>
        /// The ParseCommand.
        /// </summary>
        /// <param name="commandForms">The commandForms<see cref="CommandForm[]"/>.</param>
        /// <param name="commandString">The commandString<see cref="string"/>.</param>
        /// <param name="postString">The postString<see cref="string"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int ParseCommand(CommandForm[] commandForms, string commandString, out string postString)
        {
            for (int index = 0; index < commandForms.Length; ++index)
            {
                string idString = commandForms[index].IDString;
                if (commandForms[index].PostStringMode)
                {
                    if (commandString.IndexOf(idString) == 0)
                    {
                        postString = commandString.Substring(idString.Length);
                        return index;
                    }
                }
                else if (commandString == idString)
                {
                    postString = "";
                    return index;
                }
            }
            postString = "";
            return -1;
        }

        /// <summary>
        /// The ParseSubCharsCommand.
        /// </summary>
        /// <param name="numForms">The numForms<see cref="int"/>.</param>
        /// <param name="forms">The forms<see cref="CommandSubCharsSet[]"/>.</param>
        /// <param name="commandString">The commandString<see cref="string"/>.</param>
        /// <param name="indices">The indices<see cref="ArrayList"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool ParseSubCharsCommand(int numForms, CommandSubCharsSet[] forms, string commandString, ArrayList indices)
        {
            indices.Clear();
            int num1 = 0;
            for (int index1 = 0; index1 < numForms; ++index1)
            {
                CommandSubCharsSet form = forms[index1];
                int num2 = -1;
                int length = form.Chars.Length;
                for (int index2 = 0; index2 < length; ++index2)
                {
                    char ch = form.Chars[index2];
                    int num3 = commandString.IndexOf(ch);
                    if (num3 >= 0)
                    {
                        if (num2 >= 0 || commandString.IndexOf(ch, num3 + 1) >= 0)
                            return false;
                        num2 = index2;
                        ++num1;
                    }
                }
                if (num2 == -1 && !form.EmptyAllowed)
                    return false;
                indices.Add((object)num2);
            }
            return num1 == commandString.Length;
        }

        /// <summary>
        /// The IsItSwitchChar.
        /// </summary>
        /// <param name="c">The c<see cref="char"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool IsItSwitchChar(char c)
        {
            if (c != '-')
                return c == '/';
            return true;
        }
    }
}
