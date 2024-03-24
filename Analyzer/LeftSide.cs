using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    static class LeftSide
    {
        private enum States { S, ID, SK, ID2, KS, F, E };
        public static bool Check(string str, int start, int end, out string message, out int i,
            ref Dictionary<string, string> Idens, ref List<string> Ids, ref Dictionary<string, string> IdensIndex,
            ref List<string> Const, ref Dictionary<string, string> IdensMassive) // проверка левой стороны
        {
            message = "Строка принадлежит языку";
            States curState = States.S;
            int id1 = start, id2 = start;
            var curIden = new StringBuilder();
            for (i = start; i < end && curState != States.F && curState != States.E; i++)
            {
                var curChar = str[i];
                switch (curState)
                {
                    case States.S:
                        if (Identifier.Check(str, i, str.Length, out message, out i)) // проверка идентификатора
                        {
                            curState = States.ID;
                            id2 = i + 1;
                        }
                        else 
                        {
                            curState = States.E;
                        }
                        break;
                    case States.ID:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.ID;
                                break;
                            case '[': // проверка если является массивом
                                curIden.Append(str, id1, id2 - id1);
                                Ids.Add(curIden.ToString());
                                Analyzer.IdsType.Add("массив");
                                IdensMassive[curIden.ToString()] = "массив";
                                curIden.Clear();
                                curState = States.SK;
                                break;
                            default:
                                curIden.Append(str, id1, id2 - id1);
                                Ids.Add(curIden.ToString());
                                Analyzer.IdsType.Add("переменная");
                                Idens[curIden.ToString()] = "переменная";
                                curIden.Clear();
                                curState = States.F;
                                i--;
                                break;
                        }
                        break;

                    case States.SK:
                        if (curChar == ' ') 
                        {
                            curState = States.SK;
                            break;
                        }
                        if ("+-_".Contains(curChar) || char.IsLetterOrDigit(curChar))
                        {
                            id1 = i;
                            if ((curChar == '_' || char.IsLetter(curChar))
                            && Identifier.Check(str, i, str.Length, out message, out i))
                            {
                                curState = States.ID2;
                                id2 = i + 1;
                                curIden.Append(str, id1, id2 - id1);
                                Ids.Add(curIden.ToString());
                                Analyzer.IdsType.Add("индекс"); // добавим в список если индекс
                                if (IdensMassive.ContainsKey(curIden.ToString()))
                                {
                                    i = id1;
                                    message = "Ошибка! Использование указателя на массив в качестве индекса";
                                    curState = States.E;
                                    break;
                                }
                                IdensIndex[curIden.ToString()] = "индекс"; 
                                curIden.Clear();
                            }
                            else if ((char.IsDigit(curChar) || curChar == '+' || curChar == '-')
                                && ConstInt.Check(str, i, str.Length, out message, out i))
                            {
                                id2 = i + 1;
                                curIden.Append(str, id1, id2 - id1);
                                Const.Add(curIden.ToString());
                                Analyzer.CntsView.Add("индекс");
                                Analyzer.CntsType.Add("целая константа");
                                curIden.Clear();
                                curState = States.ID2;
                            }
                            else
                            {
                                curState = States.E;
                            }
                        }
                        else
                        {
                            message = "ОШИБКА! Ожидается + или _ или - или буква или цифра";
                            curState = States.E;
                        }
                        break;
                    case States.ID2:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.ID2;
                                break;
                            case ']':
                                curState = States.KS;
                                break;
                            default:
                                curState = States.E;
                                message = "ОШИБКА! Ожидается пробел или ]";
                                break;
                        }
                        break;
                    case States.KS:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.KS;
                                break;
                            default:
                                curState = States.F;
                                i--;
                                break;
                        }
                        break;
                }
            }
            i--;
            if (curState != States.F && curState != States.E)
            {
                i++;
                message = "Ошибка! Ожидалось продолжение!";
            }
            return curState == States.F;
        }
    }
}
