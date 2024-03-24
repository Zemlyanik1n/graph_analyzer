using Accessibility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using static System.Windows.Forms.AxHost;

namespace Analyzer
{
    static class Analyzer
    {
        public static List<string> Ids = new List<string>(); // списки для вывода
        public static List<string> IdsType = new List<string>();
        public static List<string> Cnts = new List<string>();
        public static List<string> CntsType = new List<string>();
        public static List<string> CntsView = new List<string>();
        public static Dictionary<string, string> Idens = new Dictionary<string, string>(); // словари для проверки (можно заменить на листы)
        public static Dictionary<string, string> IdensMassive = new Dictionary<string, string>();
        public static Dictionary<string, string> IdensIndex = new Dictionary<string, string>();
        public static Dictionary<string, string> IdensVar = new Dictionary<string, string>();
        private enum States {S, A1, A2, A3, B0, B, C0, C1, C2, D0, D1, D2, D3, E, F}
        public static bool Check(string str, out string message, out int i) // проверка принадлежност строки
        {
            Idens.Clear(); // очистка статик списоков при обновлении строки
            Ids.Clear();
            IdsType.Clear();
            Cnts.Clear();
            CntsType.Clear();
            CntsView.Clear();   
            IdensMassive.Clear();
            IdensIndex.Clear();
            IdensVar.Clear();
        message = "Строка принадлежит языку";
            int position = 0;
            States curState = States.S;
            for (i = position; i < str.Length && curState != States.E && curState != States.F; i++)
            {
                var curChar = str[i];
                switch (curState)
                {
                    case States.S: // проверка with
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.S;
                                break;
                            case 'W':
                                curState = States.A1;
                                break;
                            default:
                                curState = States.E;
                                message = "ОШИБКА! ОЖИДАЕТСЯ ПРОБЕЛ ИЛИ W";
                                break;
                        }
                        break;
                    case States.A1:
                        switch (curChar)
                        {
                            case 'I':
                                curState = States.A2;
                                break;
                            default:
                                curState = States.E;
                                message = "ОШИБКА! ОЖИДАЕТСЯ I";
                                break;
                        }
                        break;
                    case States.A2:
                        switch (curChar)
                        {
                            case 'T':
                                curState = States.A3;
                                break;
                            default:
                                curState = States.E;
                                message = "ОШИБКА! ОЖИДАЕТСЯ T";
                                break;
                        }
                        break;
                    case States.A3:
                        switch (curChar)
                        {
                            case 'H':
                                curState = States.B0;
                                break;
                            default:
                                curState = States.E;
                                message = "ОШИБКА! ОЖИДАЕТСЯ H";
                                break;
                        }
                        break;
                    case States.B0:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.B;
                                break;
                            default:
                                curState = States.E;
                                message = "ОШИБКА! ОЖИДАЕТСЯ ПРОБЕЛ";
                                break;
                        }
                        break;
                    case States.B:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.B;
                                break;
                            default:

                                if (Variable.Check(str, i, str.Length, out message, out i, ref Idens, ref Ids))
                                {
                                    curState = States.C0;
                                }
                                else { curState = States.E; }
                                break;
                        }
                        break;
                    case States.C0:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.C1;
                                break;
                            default:
                                curState = States.E;
                                message = "ОШИБКА! Ожидается пробел";
                                break;
                        }
                        break;
                    case States.C1:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.C1;
                                break;
                            case 'D': 
                                curState = States.C2;
                                break;
                            default:
                                curState = States.E;
                                message = "ОШИБКА! Ожидается пробел или D";
                                break;
                        }
                        break;
                    case States.C2:
                        switch (curChar)
                        {
                            case 'O':
                                curState= States.D0;
                                break;
                            default:
                                curState= States.E;
                                message = "ОШИБКА! Ожидается O";
                                break;
                        }
                        break;
                    case States.D0:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.D1;
                                break;
                            default:
                                curState = States.E;
                                message = "ОШИБКА! Ожидается пробел";
                                break;
                        }
                        break;
                    case States.D1:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.D1;
                                break;
                            default:
                                if (AssignmentOperator.Check(str, i, str.Length, out message, out i)) // проверка оператора присваивания
                                {
                                    curState = States.D2;
                                }
                                else
                                {
                                    curState = States.E;
                                }
                                break;
                        }
                        break;
                    case States.D2:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.D2;
                                break;
                            case ';':
                                curState = States.D3;
                                break;
                            default:
                                curState = States.E;
                                message = "ОШИБКА! Ожидается пробел или ;";
                                break;
                        }
                        break;
                    case States.D3:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.D3;
                                break;
                            case '\r': // проверка конечного символа - перевода строки
                                curState = States.F;
                                i++;
                                break;
                            default:
                                message = "ОШИБКА! Ожидается пробел или enter";
                                curState = States.E;
                                break;
                        }
                        break;
                }
            }
            i--;
            if (curState == States.F && str.Length - 1 != i)
            {
                i++;
                message = "ОШИБКА! Уберите лишние символы после ;";
            }
            if (curState != States.F && curState != States.E)
            {
                i++;
                message = "Ошибка! Ожидалось продолжение!";
            }
            return curState == States.F;
        }

    }
}
